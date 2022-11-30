using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicsEditor
{
    /*3. Разработать векторный графический редактор, который имеет следующие функции:
        - добавление примитивов: линия, прямоугольник, эллипс, полигон, кривая Безье
        - перемещение примитивов
        - раскраска примитивов
        - удаление примитивов
        - поворот / масштабирование примитивов
        - сохр / загрузка документа в своём формате*/

    enum currentFigure
    {
        Rectange = 1,
        Ellipse = 2,
        Line = 3,
        Polygon = 4,
        Path = 5
    }

    public partial class MainWindow : Window
    {
        //чтоб канвас понял, что рисовать.
        currentFigure currFigure;

        //базовый цвет
        Color baseColor = Colors.DarkBlue;

        // Ссылка на передвигаемый объект
        FrameworkElement movingElement = null;

        // Ссылка на выбранный объект
        FrameworkElement selectElement = null;

        // Ссылка на рисуемый объект
        FrameworkElement drawingElement = null;

        // Ссылка на предыдущий объект ( для выледения)
        FrameworkElement prevElement = null;

        // Координаты нажатия в передвигаемом объекте
        Point elementCoords;

        //Текущие координаты мыши на Canvas
        Point mousePos;

        //для рисования
        private Point tempPos;
        private Point startPosFigure;
        private bool drawing;

        //для ушек (Чтоб масштабировать примитивы мышью)
        Rectangle rectTop = null;
        Rectangle rectBot = null;
        bool earsTop = false;
        bool earsBot = false;
        Point earsPos;

        public MainWindow()
        {
            InitializeComponent();
            ClrPicker.SelectedColor = baseColor; 
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.DefaultExt = ".PNG";
            save.Filter = "Image Format png|*.png|My Image Format BVG|*.bvg";
            if (save.ShowDialog() == true)
            {
                SaveImage(save.FileName);
            }
        }

        private void SaveImage(string filename)
        {
            //canvas.LayoutTransform = null;  //обнуляем маштабировние если было


            //качество изображения
            double dpi = 300;
            double scale = dpi / 96;

            //получаем размер Canvas
            Size size = mainCanvas.RenderSize;

            RenderTargetBitmap image = new RenderTargetBitmap((int)(size.Width * scale), (int)(size.Height * scale), dpi, dpi, PixelFormats.Pbgra32);
            mainCanvas.Measure(size);
            mainCanvas.Arrange(new Rect(size)); // This is important
            image.Render(mainCanvas);
            PngBitmapEncoder encoder = new PngBitmapEncoder();  //конвертируем в png формат
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (jpg)|*.jpg|PNG Files (png)|*.png|BMP Files (bmp)|*.bmp|My Files (bvg)|*.bvg";

            bool? result = dlg.ShowDialog();

            if(result == true)
            {
                LoadImage(dlg.FileName);
            }
        }

        private void LoadImage(string fileName)
        {
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri(fileName));
            mainCanvas.Children.Clear();
            selectElement = null;
            prevElement = null;
            mainCanvas.Background = img;
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tempPos = mousePos;
            startPosFigure = mousePos;
            drawing = true;
        }

        private void CheckCurrentPos()
        {
            //если координаты конечной точки меньше чем начальной
            if (mousePos.X < startPosFigure.X)
            {
                tempPos.X = startPosFigure.X;
                startPosFigure.X = mousePos.X;
            }
            else
            {
                tempPos.X = mousePos.X;
            }
            if (mousePos.Y < startPosFigure.Y)
            {
                tempPos.Y = startPosFigure.Y;
                startPosFigure.Y = mousePos.Y;
            }
            else
            {
                tempPos.Y = mousePos.Y;
            }
        }


        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement currentElement = null;

            if(earsBot)
                earsBot = false;

            if (earsTop)
                earsTop = false;

            if (movingElement != null)
            {
                // Отменить эффект тени
                movingElement.ClearValue(EffectProperty);

                // Поместить отпущенный объект на самый нижний Z-уровень
                Canvas.SetZIndex(movingElement, 0);
                movingElement = null;
            }

            if(drawing)
            {
                switch (currFigure)
                {
                    case currentFigure.Rectange:
                        CheckCurrentPos();
                        currentElement = new Rectangle();
                        ((Rectangle)currentElement).Stroke = Brushes.Black;
                        ((Rectangle)currentElement).Fill = new SolidColorBrush(baseColor);
                        Canvas.SetLeft(currentElement, startPosFigure.X);
                        Canvas.SetTop(currentElement, startPosFigure.Y);
                        currentElement.Width = tempPos.X - startPosFigure.X;
                        currentElement.Height = tempPos.Y - startPosFigure.Y;
                        break;

                    case currentFigure.Ellipse:
                        CheckCurrentPos();
                        currentElement = new Ellipse();
                        ((Ellipse)currentElement).Stroke = Brushes.Black;
                        ((Ellipse)currentElement).Fill = new SolidColorBrush(baseColor);
                        Canvas.SetLeft(currentElement, startPosFigure.X);
                        Canvas.SetTop(currentElement, startPosFigure.Y);
                        currentElement.Width = tempPos.X - startPosFigure.X;
                        currentElement.Height = tempPos.Y - startPosFigure.Y;
                        break;

                    case currentFigure.Line:
                        currentElement = new Line();
                        ((Line)currentElement).Stroke = new SolidColorBrush(baseColor);
                        ((Line)currentElement).StrokeThickness = 3;
                        ((Line)currentElement).X1 = startPosFigure.X;
                        ((Line)currentElement).X2 = mousePos.X;
                        ((Line)currentElement).Y1 = startPosFigure.Y;
                        ((Line)currentElement).Y2 = mousePos.Y;
                        break;

                    case currentFigure.Polygon:
                        currentElement = new Polygon();
                        ((Polygon)currentElement).Stroke = Brushes.Black;
                        ((Polygon)currentElement).Fill = new SolidColorBrush(baseColor);
                        PointCollection myPointCollection = new PointCollection();
                        myPointCollection.Add(new Point(0, 0));
                        myPointCollection.Add(new Point(mousePos.X, 0));
                        myPointCollection.Add(new Point(0, mousePos.Y));
                        ((Polygon)currentElement).Points = myPointCollection;
                        Canvas.SetLeft(currentElement, startPosFigure.X);
                        Canvas.SetTop(currentElement, startPosFigure.Y);
                        break;

                    case currentFigure.Path:
                        currentElement = new System.Windows.Shapes.Path();
                        ((System.Windows.Shapes.Path)currentElement).Stroke = Brushes.Black;
                        ((System.Windows.Shapes.Path)currentElement).Fill = new SolidColorBrush(baseColor);
                        ((System.Windows.Shapes.Path)currentElement).StrokeThickness = 1;
                        ((System.Windows.Shapes.Path)currentElement).StrokeStartLineCap = PenLineCap.Round;
                        ((System.Windows.Shapes.Path)currentElement).StrokeEndLineCap = PenLineCap.Round;
                        ((System.Windows.Shapes.Path)currentElement).StrokeDashCap = PenLineCap.Round;
                        ((System.Windows.Shapes.Path)currentElement).Data = Geometry.Parse($"M 0,0 C 50,200, 100,-100, 150,150 Q 50,250 0,0");

                        Canvas.SetLeft(currentElement, startPosFigure.X);
                        Canvas.SetTop(currentElement, startPosFigure.Y);
                        break;
                }
                mainCanvas.Children.Remove(drawingElement);
                drawingElement = null;
                drawing = false;
            }
            

            if(currentElement != null)
            {
                currentElement.RenderTransformOrigin = new Point(0.5, 0.5);
                // по Z на нижний уровень
                Canvas.SetZIndex(currentElement, 0);
                currentElement.MouseDown += Element_MouseDown;


                mainCanvas.Children.Add(currentElement);
            }
        }


        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Текущие координаты мыши на холсте
            mousePos = e.GetPosition(mainCanvas);
            txtPos.Content = mousePos.X + ", " + Convert.ToInt32(mousePos.Y);

            //if (drawing)
            //{
            //    if(drawingElement != null)
            //    mainCanvas.Children.Remove(drawingElement);
            //    drawingElement = new Rectangle();
            //    ((Rectangle)drawingElement).Stroke = Brushes.Black;
            //    ((Rectangle)drawingElement).Fill = new SolidColorBrush(baseColor);

            //    drawingElement.Width = mousePos.X - startPosFigure.X;
            //    drawingElement.Height = mousePos.Y - startPosFigure.Y;
            //    drawingElement.RenderTransformOrigin = new Point(0.5, 0.5);
            //    // по Z на нижний уровень
            //    Canvas.SetZIndex(drawingElement, 0);
            //    mainCanvas.Children.Add(drawingElement);
            //    Canvas.SetLeft(drawingElement, startPosFigure.X);
            //    Canvas.SetTop(drawingElement, startPosFigure.Y);

            //}

            if (movingElement != null)
            {
                // Перемещение элемента по новым координатам мыши, с учётом места нажатия на элементе
                Canvas.SetLeft(movingElement, mousePos.X - elementCoords.X);
                Canvas.SetTop(movingElement, mousePos.Y - elementCoords.Y);
                CurrentPosEars();
            }


            if (earsTop)
            {
                // коорд мыши - последние коорды ушей
                double x = e.GetPosition(mainCanvas).X - earsPos.X;
                double y = e.GetPosition(mainCanvas).Y - earsPos.Y;

                if(selectElement.Width - x > 0)
                selectElement.Width -= x;
                if(selectElement.Height - y > 0)
                selectElement.Height -= y;

                Canvas.SetLeft(selectElement, mousePos.X - e.GetPosition(selectElement).X + x);
                Canvas.SetTop(selectElement, mousePos.Y - e.GetPosition(selectElement).Y + y);

                CurrentPosEars();

                //смещаем координаты ушей
                earsPos.X += x;
                earsPos.Y += y;
               
            }
            if (earsBot)
            {
                
                double x = e.GetPosition(mainCanvas).X - earsPos.X;
                double y = e.GetPosition(mainCanvas).Y - earsPos.Y;

                if (selectElement.Width + x > 0)
                    selectElement.Width += x;
                if (selectElement.Height + y > 0)
                    selectElement.Height += y;

                CurrentPosEars();

                earsPos.X += x;
                earsPos.Y += y;
               
            }
            
        }

        private void SelectFigureColor(Color color, double thickness)
        {
            switch (selectElement)
            {
                case Rectangle:
                    ((Rectangle)selectElement).Stroke = new SolidColorBrush(color);
                    ((Rectangle)selectElement).StrokeThickness = thickness;
                    break;
                case Ellipse:
                    ((Ellipse)selectElement).Stroke = new SolidColorBrush(color);
                    ((Ellipse)selectElement).StrokeThickness = thickness;
                    break;
                case Line:
                    ((Line)selectElement).Stroke = new SolidColorBrush(color);
                    ((Line)selectElement).StrokeThickness = thickness;
                    break;
                case Polygon:
                    ((Polygon)selectElement).Stroke = new SolidColorBrush(color);
                    ((Polygon)selectElement).StrokeThickness = thickness;
                    break;
                case System.Windows.Shapes.Path:
                    ((System.Windows.Shapes.Path)selectElement).Stroke = new SolidColorBrush(color);
                    ((System.Windows.Shapes.Path)selectElement).StrokeThickness = thickness;
                    break;
            }
        }

        private void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            movingElement = (FrameworkElement)sender;
            //Изменение контура выделенного объекта
            prevElement = selectElement;
            SelectFigureColor(Colors.Black, 1);
            selectElement = (FrameworkElement)sender;
            SelectFigureColor(Colors.Red, 2);
            //коорды мыши внутри объекта
            elementCoords = e.GetPosition(movingElement);

            if(rectBot == null)
            {
                CreateEars(e);
            }
            rectBot.Visibility = Visibility.Visible;
            rectTop.Visibility = Visibility.Visible;
            CurrentPosEars();
            //Отметить как обработанное
            e.Handled = true;
        }

        private void CurrentPosEars()
        {
            Canvas.SetLeft(rectTop, Canvas.GetLeft(selectElement)- 10);
            Canvas.SetTop(rectTop,  Canvas.GetTop(selectElement) - 10);
            Canvas.SetLeft(rectBot, Canvas.GetLeft(selectElement) + selectElement.Width);
            Canvas.SetTop(rectBot,  Canvas.GetTop(selectElement) + selectElement.Height);
        }

        private void CreateEars(MouseButtonEventArgs e)
        {
            // изменить курсор
            rectTop = new Rectangle();
            rectTop.Stroke = Brushes.Black;
            rectTop.StrokeThickness = 1;
            rectTop.Fill = Brushes.White;
            rectTop.Height = 10;
            rectTop.Width = 10;
            rectTop.PreviewMouseLeftButtonDown += Rect_MouseDownTop;
            mainCanvas.Children.Add(rectTop);

            rectBot = new Rectangle();
            rectBot.Stroke = Brushes.Black;
            rectBot.StrokeThickness = 1;
            rectBot.Fill = Brushes.White;
            rectBot.Height = 10;
            rectBot.Width = 10;

            rectBot.PreviewMouseLeftButtonDown += Rect_MouseDownBot;
            mainCanvas.Children.Add(rectBot);
        }
        private void Rect_MouseDownTop(object sender, MouseButtonEventArgs e)
        {
            
            earsTop = true;
            earsPos = e.GetPosition(mainCanvas);
            startPosFigure = mousePos;
            //Отметить как обработанное
            e.Handled = true;
        }

        private void Rect_MouseDownBot(object sender, MouseButtonEventArgs e)
        {
            earsBot = true;
            earsPos = e.GetPosition(mainCanvas);
            startPosFigure = mousePos;
            //Отметить как обработанное
            e.Handled = true;
        }


        private void Rect_MouseUp(object sender, MouseButtonEventArgs e)
        {

            FrameworkElement currentElement = null;

            if (earsTop)
            {
                switch (currFigure)
                {
                    case currentFigure.Rectange:
                        Canvas.SetLeft(selectElement, mousePos.X);
                        Canvas.SetTop(selectElement, mousePos.Y);
                        selectElement.Width += startPosFigure.X - mousePos.X;
                        selectElement.Height += startPosFigure.Y - mousePos.Y;
                       
                        break;

                    case currentFigure.Ellipse:
                        Canvas.SetLeft(selectElement, mousePos.X);
                        Canvas.SetTop(selectElement, mousePos.Y);
                        selectElement.Width += startPosFigure.X - mousePos.X;
                        selectElement.Height += startPosFigure.Y - mousePos.Y;
                        break;

                    case currentFigure.Line:
                        currentElement = new Line();
                        ((Line)currentElement).Stroke = new SolidColorBrush(baseColor);
                        ((Line)currentElement).StrokeThickness = 3;
                        ((Line)currentElement).X1 = startPosFigure.X;
                        ((Line)currentElement).X2 = mousePos.X;
                        ((Line)currentElement).Y1 = startPosFigure.Y;
                        ((Line)currentElement).Y2 = mousePos.Y;
                        break;

                    case currentFigure.Polygon:
                        currentElement = new Polygon();
                        ((Polygon)currentElement).Stroke = Brushes.Black;
                        ((Polygon)currentElement).Fill = new SolidColorBrush(baseColor);
                        PointCollection myPointCollection = new PointCollection();
                        myPointCollection.Add(new Point(0, 0));
                        myPointCollection.Add(new Point(mousePos.X, 0));
                        myPointCollection.Add(new Point(0, mousePos.Y));
                        ((Polygon)currentElement).Points = myPointCollection;
                        Canvas.SetLeft(currentElement, startPosFigure.X);
                        Canvas.SetTop(currentElement, startPosFigure.Y);
                        break;

                    case currentFigure.Path:
                        //Data="M 0,0 C 100,400, 200,-200, 300,300 Q 100,500 0,0"
                        // Создание контура с указанной геометрией контура
                        currentElement = new System.Windows.Shapes.Path();
                        ((System.Windows.Shapes.Path)currentElement).Stroke = Brushes.Black;
                        ((System.Windows.Shapes.Path)currentElement).Fill = new SolidColorBrush(baseColor);
                        ((System.Windows.Shapes.Path)currentElement).StrokeThickness = 1;
                        ((System.Windows.Shapes.Path)currentElement).StrokeStartLineCap = PenLineCap.Round;
                        ((System.Windows.Shapes.Path)currentElement).StrokeEndLineCap = PenLineCap.Round;
                        ((System.Windows.Shapes.Path)currentElement).StrokeDashCap = PenLineCap.Round;
                        ((System.Windows.Shapes.Path)currentElement).Data = Geometry.Parse($"M 0,0 C 50,200, 100,-100, 150,150 Q 50,250 0,0");


                        Canvas.SetLeft(currentElement, startPosFigure.X);
                        Canvas.SetTop(currentElement, startPosFigure.Y);
                        break;

                }
                earsTop = false;
            }

            if (earsBot)
            {
                switch (currFigure)
                {
                    case currentFigure.Rectange:
                        selectElement.Width -= startPosFigure.X - mousePos.X;
                        selectElement.Height -= startPosFigure.Y - mousePos.Y;

                        break;

                    case currentFigure.Ellipse:
                        selectElement.Width -= startPosFigure.X - mousePos.X;
                        selectElement.Height -= startPosFigure.Y - mousePos.Y;
                        break;
                }
                earsBot = false;
            }

        }

        private void ColorPicker_ChangeColor(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (ClrPicker.SelectedColor != null)
                baseColor = ClrPicker.SelectedColor.Value;
        }

        private void Paint_click(object sender, RoutedEventArgs e)
        {
            if (selectElement != null)
            {
                switch(selectElement)
                {
                    case Rectangle:
                        ((Rectangle)selectElement).Fill = new SolidColorBrush(baseColor);
                        break;
                    case Ellipse:
                        ((Ellipse)selectElement).Fill = new SolidColorBrush(baseColor);
                        break;
                    case Line:
                        ((Line)selectElement).Stroke = new SolidColorBrush(baseColor);
                        break;
                    case Polygon:
                        ((Polygon)selectElement).Fill = new SolidColorBrush(baseColor);
                        break;
                    case System.Windows.Shapes.Path:
                        ((System.Windows.Shapes.Path)selectElement).Fill = new SolidColorBrush(baseColor);
                        break;
                }
            }
        }

        private void Delete_click(object sender, RoutedEventArgs e)
        {
            if (selectElement != null)
            {
                mainCanvas.Children.Remove(selectElement);
                rectBot.Visibility = Visibility.Collapsed;
                rectTop.Visibility = Visibility.Collapsed;
                selectElement = null;

            }
        }

        private void mainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (selectElement != null)
            {
                double scale = e.Delta >= 0 ? 1.1 : 0.9;
                TransformGroup gr;
                //если в свойсве Tag уже сохранён TG, берём оттуда.
                if (selectElement.Tag != null)
                {
                    gr = selectElement.Tag as TransformGroup;
                }
                else
                    gr = new TransformGroup();

                ScaleTransform st = new ScaleTransform(scale, scale);
                gr.Children.Add(st);
                selectElement.RenderTransform = gr;
                selectElement.Tag = gr;

                CurrentPosEars();
            }
        }

        private void scrollRotate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (selectElement != null)
            {
                double ang = 0;
                ang = scrollRotate.Value > 0 ? ang += 1 : ang -= 1;
                // 1 - угол в градусах 2, 3 - координаты точки, относительно которой происходит поворот
                TransformGroup gr;
                if (selectElement.Tag != null)
                {
                    gr = selectElement.Tag as TransformGroup;
                }
                else
                    gr = new TransformGroup();

                RotateTransform rt = new RotateTransform(ang, 0, 0);
                gr.Children.Add(rt);
                selectElement.RenderTransform = gr;
                selectElement.Tag = gr;
                if(scrollRotate.Value <= -100 || scrollRotate.Value >= 100)
                scrollRotate.Value = 0;
            }
        }

        private void Rectangle_Checked(object sender, RoutedEventArgs e)
        {
            currFigure = currentFigure.Rectange;
        }

        private void Ellipse_Checked(object sender, RoutedEventArgs e)
        {
            currFigure = currentFigure.Ellipse;
        }

        private void Line_Checked(object sender, RoutedEventArgs e)
        {
            currFigure = currentFigure.Line;
        }

        private void Polygon_Checked(object sender, RoutedEventArgs e)
        {
            currFigure = currentFigure.Polygon;
        }

        private void Path_Checked(object sender, RoutedEventArgs e)
        {
            currFigure = currentFigure.Path;
        }
    }
}
