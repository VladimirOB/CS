using System.CodeDom;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;

namespace GraphicsEditor_v3
{
    /*1. Разработать простой графический редактор, который имеет следующие функции:
        - рисование примитивов: линии, карандаш, прямоугольник, эллипс, треугольник
        - возможность рисовать закрашенные фигуры
        - сохранение / загрузка рисунков в своём формате
        - сохранение рисунков в форматах: PNG, JPG, BMP
        - перерисовка картинки (Paint)
        - осветление / затемнение картинки (быстрый способ)
        - поворот картинки на 90, 180, 270 (быстрый способ)
    
        - рисование происходит на слоях
        - можно добавлять, удалять и переключать слои
        - отображать список слоёв в listBox / listView
        - в своём формате сохранять информацию о слоях
        - перед отрисовкой и сохранением в .bmp, .jpg, .png объединять все слои
        - добавить выделение и копирование выделенной части на другой слой (по желанию)
     */

    public partial class Form1 : Form
    {
        //SetLastError - Exception
        [DllImport("gdi32", SetLastError = true, EntryPoint = "SetROP2")]
        static extern unsafe int SetROP2(IntPtr hWnd, int mode);

        [DllImport("gdi32", SetLastError = true, EntryPoint = "CreatePen")]
        static extern unsafe IntPtr CreatePen(int style,int width,int color);

        [DllImport("gdi32", SetLastError = true, EntryPoint = "SelectObject")]
        static extern unsafe IntPtr SelectObject(IntPtr hWnd, IntPtr obj);

        [DllImport("gdi32", SetLastError = true, EntryPoint = "DeleteObject")]
        static extern unsafe bool DeleteObject(IntPtr obj);

        [DllImport("gdi32", SetLastError = true, EntryPoint = "LineTo")]
        static extern unsafe bool LineTo(IntPtr hWnd,int x,int y);

        [DllImport("gdi32", SetLastError = true, EntryPoint = "MoveToEx")]
        static extern unsafe bool MoveToEx(IntPtr hWnd,int x,int y,IntPtr point);

        List<Point> selPoints = new List<Point>();
        Point start, old;
        bool flagHighlight = false;

        //для рисования
        private Point tempPos;
        private Point startPosFigure;
        private Point mousePos;

        BitmapLayers layers;
        ViewLayers viewLayers;

        bool isBrush = false;
        bool isLine = false;
        bool isRectangle = false;
        bool isEllipse = false;
        bool isTriangle = false;
        bool isGrater = false;
        bool isHighlight = false;

        int cntOfLayers = 1;
        int cntOfHighlight = 1;
        public Form1()
        {
            InitializeComponent();
            layers = new BitmapLayers(Width, Height, pictureBox1);
            CreateLayersList();
            layers.OnLayersChanged += Layers2_OnLayersChanged;
            layers.LayersRefresh();
        }

        private void Layers2_OnLayersChanged(BitmapLayers layers)
        {
            viewLayers.checkedListBox1.Items.Clear();

            foreach (Layer layer in layers.lst)
            {
                viewLayers.checkedListBox1.Items.Add(layer.Name);
                if (layer.Visible)
                    viewLayers.checkedListBox1.SetItemChecked(viewLayers.checkedListBox1.Items.Count - 1, true);
                else
                    viewLayers.checkedListBox1.SetItemChecked(viewLayers.checkedListBox1.Items.Count - 1, false);
            }
        }



        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layers.Add(1, $"Layer {cntOfLayers++}");
        }

        private void CreateLayersList()
        {
            viewLayers = new ViewLayers();
            viewLayers.Location = new Point(Width + 510, 240);
            viewLayers.checkedListBox1.Items.Add(layers.lst[0].Name);
            viewLayers.FormClosing += ViewLayers_FormClosing;
            viewLayers.checkedListBox1.ItemCheck += CheckedListBox1_ItemCheck;
            viewLayers.Show();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs ee)
        {
            // если точек выделения больше одной (т.е. было выделение)
            if (selPoints.Count > 1)
            {
                // выключить режим рисования
                flagHighlight = false;
                Bitmap bitmap = (Bitmap)pictureBox1.Image;
                
                Point e = selPoints[selPoints.Count - 1];
                startPosFigure = selPoints[0];
                tempPos = e;
                
                CheckCurrentPos(e.X, e.Y);
                Bitmap bitmap2 = bitmap.Clone(new Rectangle(startPosFigure.X, startPosFigure.Y, tempPos.X - startPosFigure.X, tempPos.Y - startPosFigure.Y), pictureBox1.Image.PixelFormat);
                layers.Add(1, $"Layer copy {cntOfHighlight++}");
                layers.lst[layers.lst.Count - 1].image = bitmap2;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = e.X;
            mousePos.Y = e.Y;
            statusLabel1.Text = $" {mousePos.X}, {mousePos.Y}px    Picture size: {pictureBox1.Width} x {pictureBox1.Height}px";

            if (isBrush)
            {
                if (viewLayers.checkedListBox1.SelectedIndex != -1)
                {
                    //передаём сам PictureBox, имя, и координаты
                    layers.DrawBrush(viewLayers.checkedListBox1.SelectedIndex, mousePos, tempPos);
                    layers.Invalidate();
                }
                tempPos.X = e.X;
                tempPos.Y = e.Y;
            }

            if (isGrater)
            {
                if (viewLayers.checkedListBox1.SelectedIndex != -1)
                {
                    //передаём сам PictureBox, имя, и координаты
                    layers.Grater(viewLayers.checkedListBox1.SelectedIndex, mousePos, tempPos);
                    layers.Invalidate();
                }
                tempPos.X = e.X;
                tempPos.Y = e.Y;
            }

            if(isHighlight && flagHighlight)
            {
                // соддать контекст для рисования в окне
                Graphics gr = pictureBox1.CreateGraphics();

                // получить низкоуровневый адрес контекста для рисования в окне
                IntPtr dc = gr.GetHdc();

                // занести во второй указатель 0
                IntPtr ptr2 = (IntPtr)0;

                // включить режим рисования XOR в окне PictureBox
                SetROP2(dc, 10);

                // создать карандаш для рисования
                IntPtr pen = CreatePen(1, 1, 0);

                // выбрать карандаш в контекст для рисования
                IntPtr oldobj = SelectObject(dc, pen);

                /* стирание старой линии*/
                // переместить световое перо в координаты начала линии
                MoveToEx(dc, start.X, start.Y, ptr2);
                // нарисовать линию от координат начала до старых координат
                LineTo(dc, old.X, old.Y);

                /*рисование новой линии*/
                // переместить координаты светового пера в начало линии, которую нужно нарисовать
                MoveToEx(dc, start.X, start.Y, ptr2);
                // нарисовать линию от начала до координат мыши
                LineTo(dc, e.X, e.Y);

                // вернуть в контекст старый карандаш
                SelectObject(dc, oldobj);
                // удалить наш карандаш за ненадобностью
                DeleteObject(pen);

                // текущие координаты мыши сделать старыми
                old = new Point(e.X, e.Y);

                // освободить низкоуровневый контекст устройства
                gr.ReleaseHdc();

                // освободить объект Graphics
                gr.Dispose();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            tempPos = mousePos;

            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (groupBox1.Controls[i] is RadioButton && ((RadioButton)groupBox1.Controls[i]).Checked)
                {

                    RadioButton currentButton = (RadioButton)groupBox1.Controls[i];

                    if (currentButton.Tag.Equals("Brush"))
                    {
                        isBrush = true;
                        return;
                    }
                    if (currentButton.Tag.Equals("Line"))
                    {
                        isLine = true;
                        return;
                    }
                    if (currentButton.Tag.Equals("Rectangle"))
                    {
                        startPosFigure = mousePos;
                        isRectangle = true;
                        return;
                    }
                    if (currentButton.Tag.Equals("Ellipse"))
                    {
                        startPosFigure = mousePos;
                        isEllipse = true;
                        return;
                    }
                    if (currentButton.Tag.Equals("Triangle"))
                    {
                        startPosFigure = mousePos;
                        isTriangle = true;
                        return;
                    }
                    if (currentButton.Tag.Equals("Grater"))
                    {
                        isGrater = true;
                        return;
                    }
                    if (currentButton.Tag.Equals("Highlight"))
                    {
                        isHighlight = true;

                        if (flagHighlight)
                        { 
                            // соддать контекст для рисования в окне
                            Graphics gr = pictureBox1.CreateGraphics();

                            // получить низкоуровневый адрес контекста для рисования в окне
                            IntPtr dc = gr.GetHdc();

                            //занести во 2й указатель 0
                            IntPtr ptr2 = (IntPtr)0;

                            //включить режим COPY_PEN (обычное рисование)
                            SetROP2(dc, 13);

                            IntPtr pen = CreatePen(1, 1, 0);

                            IntPtr oldobj = SelectObject(dc, pen);

                            //нарисовать линию в обычном режиме(навсегда)
                            MoveToEx(dc, start.X, start.Y, ptr2);
                            LineTo(dc, e.X, e.Y);

                            // вернуть старый карандаш и удалить новый
                            SelectObject(dc, oldobj);
                            DeleteObject(pen);

                            // занести координаты мыши в список точек выделения
                            selPoints.Add(new Point(e.X, e.Y));

                            // освободить контексты для рисования
                            gr.ReleaseHdc();
                            gr.Dispose();

                            // занести координаты мыши в стартовую точку
                            start = new Point(e.X, e.Y);

                        }
                        else
                        {
                            // очистить список точек выделения
                            selPoints.Clear();

                            // старые координаты содержат координаты мыши
                            old = new Point(e.X, e.Y);

                            // новые координаты (координаты начала линии) содержат координаты мыши
                            start = new Point(e.X, e.Y);

                            // координаты старта линии занести в список точек
                            selPoints.Add(new Point(e.X, e.Y));

                            // включить режим рисования
                            flagHighlight = true;
                        }

                        return;
                    }

                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isBrush)
                isBrush = false;
            if (isGrater)
                isGrater = false;

            if (isLine)
            {
                layers.DrawLine(viewLayers.checkedListBox1.SelectedIndex, tempPos.X, tempPos.Y, e.X, e.Y);
                layers.Invalidate();
                isLine = false;

            }

            if (isRectangle)
            {
                CheckCurrentPos(e.X, e.Y);
                layers.DrawRectangle(viewLayers.checkedListBox1.SelectedIndex, startPosFigure.X, startPosFigure.Y, tempPos.X - startPosFigure.X, tempPos.Y - startPosFigure.Y, checkBoxFill.Checked);
                layers.Invalidate();
                isRectangle = false;
            }

            if (isEllipse)
            {
                CheckCurrentPos(e.X, e.Y);
                layers.DrawEllipse(viewLayers.checkedListBox1.SelectedIndex, startPosFigure.X, startPosFigure.Y, tempPos.X - startPosFigure.X, tempPos.Y - startPosFigure.Y, checkBoxFill.Checked);
                layers.Invalidate();
                isEllipse = false;
            }
            if (isTriangle)
            {
                layers.DrawTriangle(viewLayers.checkedListBox1.SelectedIndex, startPosFigure.X, startPosFigure.Y, e.X, e.Y, checkBoxFill.Checked);
                layers.Invalidate();
                isTriangle = false;
            }
        }

        private void CheckCurrentPos(int x, int y)
        {
            //если координаты конечной точки меньше чем начальной
            if (x < startPosFigure.X)
            {
                tempPos.X = startPosFigure.X;
                startPosFigure.X = x;
            }
            else
            {
                tempPos.X = x;
            }
            if (y < startPosFigure.Y)
            {
                tempPos.Y = startPosFigure.Y;
                startPosFigure.Y = y;
            }
            else
            {
                tempPos.Y = y;
            }
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            layers.Rotate(viewLayers.checkedListBox1.SelectedIndex, true);
            layers.Invalidate();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            layers.Rotate(viewLayers.checkedListBox1.SelectedIndex, false);
            layers.Invalidate();
        }

        private void trackBarBrush_Scroll(object sender, EventArgs e)
        {
            layers.widthBrush = trackBarBrush.Value;
        }

        private void ChangeColor()
        {
            ColorDialog dlg = new ColorDialog();
            dlg.FullOpen = true;
            dlg.ShowDialog();
            layers.baseColor = dlg.Color;
            panelCurrentColor.BackColor = dlg.Color;
        }

       

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Load Image";
            openFileDialog1.Filter = "My Image format|*.bvg|Image format|*.jpg|Image format|*.bmp|Image format|*.png|All files|*.*";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.FilterIndex = 5;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                layers.OpenFile(viewLayers.checkedListBox1.SelectedIndex, openFileDialog1.FileName);
                tBarBrightness.Value = 0;
                layers.Invalidate();
            }
        }

        private void saveImageFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileImage.Title = "Save image";
            saveFileImage.Filter = "Image format|*.jpg|Image format|*.bmp|Image format|*.png";

            if (saveFileImage.ShowDialog() == DialogResult.OK)
            {
                layers.SaveImage(saveFileImage.FileName);
            }
            
        }
        private void saveBVGFormatLayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileBVG.Title = "Save bvg";
            saveFileBVG.Filter = "bvg format|*.bvg";
            if(saveFileBVG.ShowDialog() == DialogResult.OK)
            {
                layers.SaveBVG(saveFileBVG.FileName);
            }
        }

        private void tBarBrightness_ValueChanged(object sender, EventArgs e)
        {
            layers.Brightness(viewLayers.checkedListBox1.SelectedIndex, tBarBrightness.Value);
            layers.Invalidate();
        }

        private void deleteLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int pos = viewLayers.checkedListBox1.SelectedIndex;
            if (pos > 0)
            {
                layers.DeleteLayer(pos);
                viewLayers.checkedListBox1.Items.RemoveAt(pos);
                layers.Invalidate();
            }
            else
            {
                MessageBox.Show("Сan't remove base layer", "Warning!");
            }
        }

        private void CheckedListBox1_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            if(e.Index < layers.lst.Count)
            {
                if(e.NewValue == CheckState.Checked)
                    layers.lst[e.Index].Visible = true;
                else
                    layers.lst[e.Index].Visible = false;

                layers.Invalidate();
            }
        }

        private void ViewLayers_FormClosing(object? sender, FormClosingEventArgs e)
        {
            viewLayers.Visible = false;
            e.Cancel = true;
        }

        private void openLayersListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewLayers.Visible = true;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeColor();
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            ChangeColor();
        }

        private void btnColor1_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Black;
            panelCurrentColor.BackColor = Color.Black;
        }

        private void btnColor2_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Gray;
            panelCurrentColor.BackColor = Color.Gray;
        }

        private void btnColor11_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.White;
            panelCurrentColor.BackColor = Color.White;
        }

        private void btnColor3_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Brown;
            panelCurrentColor.BackColor = Color.Brown;
        }

        private void btnColor4_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Red;
            panelCurrentColor.BackColor = Color.Red;
        }

        private void btnColor5_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.OrangeRed;
            panelCurrentColor.BackColor = Color.OrangeRed;
        }

        private void btnColor6_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Yellow;
            panelCurrentColor.BackColor = Color.Yellow;
        }

        private void btnColor7_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Green;
            panelCurrentColor.BackColor = Color.Green;
        }

        private void btnColor8_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.DarkTurquoise;
            panelCurrentColor.BackColor = Color.DarkTurquoise;
        }

        private void btnColor9_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.RoyalBlue;
            panelCurrentColor.BackColor = Color.RoyalBlue;
        }

        private void btnColor10_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.BlueViolet;
            panelCurrentColor.BackColor = Color.BlueViolet;
        }

        private void btnColor12_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Silver;
            panelCurrentColor.BackColor = Color.Silver;
        }

        private void btnColor13_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Peru;
            panelCurrentColor.BackColor = Color.Peru;
        }

        private void btnColor14_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Pink;
            panelCurrentColor.BackColor = Color.Pink;
        }

        private void btnColor15_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.Gold;
            panelCurrentColor.BackColor = Color.Gold;
        }

        private void btnColor16_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.LemonChiffon;
            panelCurrentColor.BackColor = Color.LemonChiffon;
        }

        private void btnColor17_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.FromArgb(192, 255, 192);
            panelCurrentColor.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void btnColor18_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.FromArgb(192, 255, 255);
            panelCurrentColor.BackColor = Color.FromArgb(192, 255, 255);
        }

        private void btnColor19_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.FromArgb(192, 192, 255);
            panelCurrentColor.BackColor = Color.FromArgb(192, 192, 255);
        }
        private void btnColor20_Click(object sender, EventArgs e)
        {
            layers.baseColor = Color.MediumPurple;
            panelCurrentColor.BackColor = Color.MediumPurple;
        }
    }
}