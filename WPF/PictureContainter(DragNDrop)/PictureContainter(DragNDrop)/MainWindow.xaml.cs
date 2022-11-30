using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
//using static System.Net.Mime.MediaTypeNames;

namespace PictureContainter_DragNDrop_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Хранение изображений полученных из Drop
        List<string> images = new List<string>();
        int cntImagesRows = 1, cntImagesCols = 1;

        //размеры изображений
        const int SIZEW = 112;
        const int SIZEH = 70;

        // Ссылка на передвигаемый объект
        Image movingElementLMB = null;
        Image movingElementRMB = null;

        // Координаты нажатия в передвигаемом объекте
        Point elementCoords;

        //для Rotate
        double angle = 0;

        int currentRowGrid;
        int currentColGrid;
        public MainWindow()
        {
            InitializeComponent();
            InitGrid();
            Help();
        }

        private void InitGrid()
        {
            for (int i = 0; i < this.Width; i++)
            {
                ColumnDefinition colDef1 = new ColumnDefinition();
                colDef1.Width = new GridLength(SIZEW);
                if (SIZEW * i < Width)
                {
                    tabPageGrid.ColumnDefinitions.Add(colDef1);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < this.Height; i++)
            {
                RowDefinition rowDef1 = new RowDefinition();
                rowDef1.Height = new GridLength(SIZEH);
                if (SIZEH * i < Height)
                {
                    tabPageGrid.RowDefinitions.Add(rowDef1);
                }
                else
                {
                    break;
                }
            }
            currentRowGrid = Grid.GetRow(tabPageGrid);
            currentColGrid = Grid.GetColumn(tabPageGrid);
        }

        private void Help()
        {
            myLabel.Content = "To scale an image, click and hold LMB on it and rotate the wheel\n" +
                               "To rotate an image, press and hold RMB on it and rotate the wheel";
        }

        private void mainCanvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
              (e.AllowedEffects & DragDropEffects.Copy) != 0 && 
              !e.Data.GetDataPresent("MyFormat"))
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void mainCanvas_Drop(object sender, DragEventArgs e)
        {
            if (myLabel.Content != null)
                myLabel.Content = null;
            string[] fls = ((string[])e.Data.GetData(DataFormats.FileDrop));

            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
              (e.AllowedEffects & DragDropEffects.Copy) != 0 &&
              !e.Data.GetDataPresent("MyFormat"))
            {
                

                foreach (var item in fls)
                {
                    CheckFile(item);
                }
                AddImageInGrid();
                AddImageInCanvas();
            }
        }

        private void AddImageInCanvas()
        {
            foreach (var fileName in images)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = (new ImageSourceConverter()).ConvertFromString(fileName) as ImageSource;
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                image.Width = SIZEW;
                image.Height = SIZEH;

                if (cntImagesCols * SIZEW+5 < Width)
                {
                    if(cntImagesRows == 1)
                    {
                        if(cntImagesCols == 1)
                        {
                            Canvas.SetLeft(image, cntImagesCols++);
                            Canvas.SetTop(image, cntImagesRows);
                        }
                        else
                        {
                            Canvas.SetLeft(image, (cntImagesCols - 1) * SIZEW+5);
                            Canvas.SetTop(image, cntImagesRows);
                            cntImagesCols++;
                        }
                        
                    }
                    else
                    {
                        if (cntImagesCols == 1)
                        {
                            Canvas.SetLeft(image, cntImagesCols++);
                            Canvas.SetTop(image, (cntImagesRows-1) * SIZEH+5);
                        }
                        else
                        {
                            Canvas.SetLeft(image, (cntImagesCols - 1) * SIZEW+5);
                            Canvas.SetTop(image, (cntImagesRows - 1) * SIZEH+5);
                            cntImagesCols++;
                        }
                    }
                }
                else
                {
                    //переход на следующую строку
                    cntImagesCols = 1;
                    cntImagesRows++;
                    Canvas.SetLeft(image, cntImagesCols++);
                    Canvas.SetTop(image, (cntImagesRows - 1) * SIZEH+5);
                }

                image.MouseDown += Image_MouseDown;
                image.MouseRightButtonDown += Image_MouseRightButtonDown;
                image.MouseRightButtonUp += Image_MouseRightButtonUp;
                image.MouseWheel += Image_MouseWheel;

                // Поместить объект на самый нижний Z-уровень
                Canvas.SetZIndex(image, 0);

                mainCanvas.Children.Add(image);
            }

            //очищаем буффер изображений
            images.Clear();
        }

        private void AddImageInGrid()
        {
            
            foreach (var fileName in images)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = (new ImageSourceConverter()).ConvertFromString(fileName) as ImageSource;

                image.Width = SIZEW;
                image.Height = SIZEH;
                if (SIZEW * currentColGrid < Width)
                {
                    Grid.SetRow(image, currentRowGrid);
                    Grid.SetColumn(image, currentColGrid++);
                }
                else
                {
                    currentColGrid = 0;
                    Grid.SetRow(image, currentRowGrid++);
                    Grid.SetColumn(image, currentColGrid);
                }
                image.MouseDown += grid_MouseDown;
                tabPageGrid.Children.Add(image);
            }
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            GridShowImage show = new GridShowImage(1024,768);
            show.imageShow.Source = img.Source;
            show.ShowDialog();
        }

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (movingElementLMB != null)
            {
                double scale = e.Delta >= 0 ? 1.1 : 0.9;
                TransformGroup gr;
                if (movingElementLMB.Tag != null)
                {
                    gr = movingElementLMB.Tag as TransformGroup;
                }
                else
                    gr = new TransformGroup();

                ScaleTransform st = new ScaleTransform(scale, scale);
                gr.Children.Add(st);
                movingElementLMB.RenderTransform = gr;
                movingElementLMB.Tag = gr;

                

            }
            if(movingElementRMB != null)
            {
                angle = e.Delta > 0 ? angle + 5 : angle - 5;
                // 1 - угол в градусах 2, 3 - координаты точки, относительно которой происходит поворот
                TransformGroup gr;
                if (movingElementRMB.Tag != null)
                {
                    gr = movingElementRMB.Tag as TransformGroup;
                }
                else
                    gr = new TransformGroup();

                RotateTransform rt = new RotateTransform(angle, 0, 0);
                gr.Children.Add(rt);
                movingElementRMB.RenderTransform = gr;
                movingElementRMB.Tag = gr;
            }
        }

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            movingElementRMB = (Image)sender;

            //коорды мыши внутри объекта
            elementCoords = e.GetPosition(movingElementRMB);

            //Поместить объект на верхний уровень по Z
            Canvas.SetZIndex(movingElementRMB, 10);
            e.Handled = true;
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (movingElementRMB != null)
            {
                //нижний уровень по Z
                Canvas.SetZIndex(movingElementRMB, 0);
                movingElementRMB = null;
                angle = 0;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            movingElementLMB = (Image)sender;

            //коорды мыши внутри объекта
            elementCoords = e.GetPosition(movingElementLMB);

            //Поместить объект на верхний уровень по Z
            Canvas.SetZIndex(movingElementLMB, 10);

            movingElementLMB.Effect = new DropShadowEffect
            {
                Color = new Color { A = 0, R = 0, G = 0, B = 0 },

                //Угол тени
                Direction = 300,
                //радиус(величина тени)
                BlurRadius = 150,

                //качество
                RenderingBias = RenderingBias.Quality,

                //Дистанция от объекта
                ShadowDepth = 10,

                //прозрачность 
                Opacity = 1
            };
            //Отметить как обработанное
            e.Handled = true;
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingElementLMB != null)
            {
                // Текущие координаты мыши на холсте
                Point coords = e.GetPosition(mainCanvas);

                // Перемещение элемента по новым координатам мыши, с учётом места нажатия на элементе
                Canvas.SetLeft(movingElementLMB, coords.X - elementCoords.X);
                Canvas.SetTop(movingElementLMB, coords.Y - elementCoords.Y);
            }

        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(movingElementLMB != null)
            {
                //Отменить эффект тени
                movingElementLMB.ClearValue(EffectProperty);

                //нижний уровень по Z
                Canvas.SetZIndex(movingElementLMB, 0);
                movingElementLMB = null;
            }
        }

        private void CheckFile(string fileName)
        {
            FileAttributes attr = File.GetAttributes(fileName);
            if((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                CheckDir(fileName);
            }
            else
            {
                if (fileName.EndsWith(".jpg") || fileName.EndsWith(".bmp") || fileName.EndsWith("png"))
                {
                    images.Add(fileName);
                }
                    
            }
        }

        private void mainCanvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (movingElementRMB != null)
                {
                    mainCanvas.Children.Remove(movingElementRMB);
                }
            }
        }

        private void ellipse3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void captionEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // организация перемещения окна
            DragMove();
        }

        private void CheckDir(string dirName)
        {
            foreach (string s1 in Directory.GetFiles(dirName))
            {
                if (s1.EndsWith(".jpg") || s1.EndsWith(".bmp") || s1.EndsWith("png"))
                    images.Add(s1);
            }
            foreach (string s2 in Directory.GetDirectories(dirName))
            {
                CheckDir(s2);
            }
        }
    }
}
