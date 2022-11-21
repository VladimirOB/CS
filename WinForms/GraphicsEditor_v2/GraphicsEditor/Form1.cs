using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GraphicsEditor
{
    /*2. –азработать простой графический редактор, который имеет следующие функции:
        - рисование примитивов: линии, карандаш, пр€моугольник, эллипс, треугольник
        - возможность рисовать закрашенные фигуры
        - сохранение / загрузка рисунков в своЄм формате
        - сохранение рисунков в форматах: PNG, JPG, BMP
        - перерисовка картинки (Paint)
        - осветление / затемнение картинки (быстрый способ)
        - поворот картинки на 90, 180, 270 (быстрый способ)*/

    public partial class Form1 : Form
    {
        private Point mousePos;

        //дл€ рисовани€
        private Point tempPos;

        Color baseColor = Color.Blue;

        Image image;
        
        //запоминает последнюю нарисованную фигуру.
        (int, int, int, int) backspace;
        
        bool isCurve = false;


        // дл€ TrackBar'a
        int widthCurve = 0;
        int prevBrightnessValue = 51;

        public Form1()
        {
            InitializeComponent();
            panelCurrentColor.BackColor = baseColor;
            image = new Bitmap(1920, 1080);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = e.X;
            mousePos.Y = e.Y;

            if(checkCurve.Checked && isCurve)
            {
                Graphics imgr = Graphics.FromImage(image);
                Graphics gr = pictureBox1.CreateGraphics();
                imgr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // —оздать карандаш
                Pen pen = new Pen(baseColor, widthCurve);
                // –исование линии
                imgr.DrawLine(pen, e.X, e.Y, tempPos.X, tempPos.Y);
                tempPos.X = e.X;
                tempPos.Y = e.Y;

                gr.DrawImage(image, 0, 0);
                backspace = (e.X, e.Y, tempPos.X, tempPos.Y);
                imgr.Dispose();
                gr.Dispose();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            tempPos = mousePos;

            if (checkCurve.Checked)
            {
                isCurve = true;
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (checkCurve.Checked)
            {
                isCurve = false;
            }

            if (checkLine.Checked)
            {
                Graphics imgr = Graphics.FromImage(image);
                Graphics gr = pictureBox1.CreateGraphics();
                // —оздать карандаш
                Pen pen = new Pen(baseColor, widthCurve);
                // –исование линии
                imgr.DrawLine(pen, e.X, e.Y, tempPos.X, tempPos.Y);

                gr.DrawImage(image, 0, 0);
                backspace = (e.X, e.Y, tempPos.X, tempPos.Y);
                imgr.Dispose();
                gr.Dispose();
            }
        }

        private void checkEllipse_Click(object sender, EventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                    if (checkAll.Checked == true && checkAll != checkEllipse && checkAll != checkFill)
                    checkAll.Checked = false;
            }
        }

        private void checkRect_Click(object sender, EventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if(checkAll != null)
                if (checkAll.Checked == true && checkAll != checkRect && checkAll != checkFill)
                    checkAll.Checked = false;
            }
        }

        private void checkTriangle_Click(object sender, EventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                    if (checkAll.Checked == true && checkAll != checkTriangle && checkAll != checkFill)
                    checkAll.Checked = false;
            }
        }
        private void checkLine_Click(object sender, EventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                    if (checkAll.Checked == true && checkAll != checkLine && checkAll != checkFill)
                    checkAll.Checked = false;
            }
        }

        private void checkCurve_Click(object sender, EventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                    if (checkAll.Checked == true && checkAll != checkFill && checkAll != checkCurve)
                        checkAll.Checked = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                if (checkAll.Checked == true && checkAll != checkLine && checkAll != checkCurve)
                {
                    if (checkAll.Tag != null)
                    {
                        Graphics imgr = Graphics.FromImage(image);
                        Graphics gr = pictureBox1.CreateGraphics();
                        switch (checkAll.Tag.ToString())
                        {
                            case "2":
                                CreateRectange(gr, imgr);
                                break;
                            case "3":
                                CreateEllipse(gr, imgr);
                                break;
                            case "4":
                                CreateTriangle(gr, imgr);
                                break;
                        }
                        imgr.Dispose();
                        gr.Dispose();
                        backspace = (mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                        break;
                    }
                }
            }
        }

        private void CreateRectange(Graphics gr, Graphics imgr)
        {
            // создание сплошной одноцветной кисти (принимает цвет)
            Brush brush1 = new SolidBrush(baseColor);
            // закрашенный пр€моугольник
            if (checkFill.Checked)
            {
                imgr.FillRectangle(brush1, mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                gr.DrawImage(image, 0, 0);
            }
            else
            {
                imgr.DrawRectangle(new Pen(brush1), mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                gr.DrawImage(image, 0, 0);
            }
        }

        private void CreateEllipse(Graphics gr, Graphics imgr)
        {
            // создание сплошной одноцветной кисти (принимает цвет)
            Brush brush1 = new SolidBrush(baseColor);
            // закрашенный
            if (checkFill.Checked)
            {
                imgr.FillEllipse(brush1, mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                gr.DrawImage(image, 0, 0);
            }
            else
            {
                imgr.DrawEllipse(new Pen(brush1), mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                gr.DrawImage(image, 0, 0);
            }
        }

        private void CreateTriangle(Graphics gr, Graphics imgr)
        {
            // создание сплошной одноцветной кисти (принимает цвет)
            Brush brush1 = new SolidBrush(baseColor);

            // ћассив точек треугольника.
            Point[] points = new Point[3];
            points[0].X = mousePos.X; 
            points[0].Y = mousePos.Y;

            points[1].X = mousePos.X + 100; 
            points[1].Y = mousePos.Y;

            points[2].X = mousePos.X + Convert.ToInt32(TBSizeWidth.Text); 
            points[2].Y = mousePos.Y + Convert.ToInt32(TBSizeHeight.Text);

            // закрашенный 
            if (checkFill.Checked)
            {
                imgr.FillPolygon(brush1, points);
                gr.DrawImage(image, 0, 0);
            }
            else
            {
                imgr.DrawPolygon(new Pen(brush1), points);
                gr.DrawImage(image, 0, 0);
            }
        }

        

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawImage(image, 0, 0);
           
        }

        private void TBSizeWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TBSizeHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.FullOpen = true;
            dlg.ShowDialog();
            baseColor = dlg.Color;
            panelCurrentColor.BackColor = baseColor;
        }

        private void newImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image = new Bitmap(1920, 1080);
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            gr.Dispose();
        }

        private void buttonBackSpace_Click(object sender, EventArgs e)
        {
            Graphics imgr = Graphics.FromImage(image);
            Graphics gr = pictureBox1.CreateGraphics();

            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                {
                    if (backspace.Item3 != 0 && backspace.Item4 != 0)
                    {
                        if(checkFill.Checked)
                            imgr.FillRectangle(new SolidBrush(Color.White), backspace.Item1, backspace.Item2, backspace.Item3, backspace.Item4);
                        else
                            imgr.FillRectangle(new SolidBrush(Color.White), backspace.Item1, backspace.Item2, backspace.Item3+1, backspace.Item4+1);
                        gr.DrawImage(image, 0, 0);
                    }
                }
            }
            imgr.Dispose();
            gr.Dispose();
            //pictureBox1.Invalidate();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save image";
            saveFileDialog1.Filter = "My Image format|*.bvg|Image format|*.jpg|Image format|*.bmp|Image format|*.png";
            
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image.Save(saveFileDialog1.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //очистка
            newImageToolStripMenuItem_Click(null, null);

            openFileDialog1.Title = "Load Image";
            openFileDialog1.Filter = "My Image format|*.bvg|Image format|*.jpg|Image format|*.bmp|Image format|*.png|All files|*.*";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                Bitmap bmp = (Bitmap)Bitmap.FromStream(stream);
                stream.Close();
                image = bmp;
                pictureBox1.Invalidate();
            }
        }

        private void btnColorBlack_Click(object sender, EventArgs e)
        {
            panelCurrentColor.BackColor = Color.Black;
            baseColor = Color.Black;
        }

        private void btnColorWhite_Click(object sender, EventArgs e)
        {
            panelCurrentColor.BackColor = Color.White;
            baseColor = Color.White;
        }

        private void btnColorGray_Click(object sender, EventArgs e)
        {
            panelCurrentColor.BackColor = Color.Gray;
            baseColor = Color.Gray;
        }

        private void btnColorYellow_Click(object sender, EventArgs e)
        {
            panelCurrentColor.BackColor = Color.Yellow;
            baseColor = Color.Yellow;
        }

        private void btnColorGreen_Click(object sender, EventArgs e)
        {
            panelCurrentColor.BackColor = Color.Green;
            baseColor = Color.Green;
        }

        private void btnColorBlue_Click(object sender, EventArgs e)
        {
            panelCurrentColor.BackColor = Color.Blue;
            baseColor = Color.Blue;
        }

        private void btnColorRed_Click(object sender, EventArgs e)
        {
            panelCurrentColor.BackColor = Color.Red;
            baseColor = Color.Red;
        }

        private void btnColorPurple_Click(object sender, EventArgs e)
        {
            panelCurrentColor.BackColor = Color.Purple;
            baseColor = Color.Purple;
        }

        private void trackBarWidthCurve_Scroll(object sender, EventArgs e)
        {
            labelSizeCurve.Text = "Width: " + widthCurve;
            widthCurve = trackBarWidthCurve.Value;
        }


        private void trackBarBrightness_Scroll(object sender, EventArgs e)
        {
            Graphics imgr = Graphics.FromImage(image);
            Graphics gr = pictureBox1.CreateGraphics();

            Bitmap bmp = new Bitmap(image);

            //ѕолучаем размеры изображени€
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            //ѕолучаем "сырые данные" изображени€
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            //указатель на начальный байт
            IntPtr ptr = bmpData.Scan0;

            //размер в байтах
            int size = bmpData.Stride * bmp.Height;

            //выделить пам€ть дл€ массива байт изображени€
            byte[] rgbValues = new byte[size];

            // скопировать пам€ть изображени€ в массив байт
            Marshal.Copy(ptr, rgbValues, 0, size);
            int len = rgbValues.Length;
            int value = trackBarBrightness.Value;
            if(prevBrightnessValue < trackBarBrightness.Value)
            {
                for (int counter = 0; counter < len; counter++)
                {
                    if (rgbValues[counter] + 20 <= 255)
                        rgbValues[counter] += 20;
                }
            }
            else
            {
                for (int counter = 0; counter < len; counter++)
                {
                    if (rgbValues[counter] - 20 >= 0)
                        rgbValues[counter] -= 20;
                }
            }

            prevBrightnessValue = trackBarBrightness.Value;
            if(trackBarBrightness.Value < 5 || trackBarBrightness.Value > 95)
            trackBarBrightness.Value = 50;

            Marshal.Copy(rgbValues, 0, ptr, size);
            bmp.UnlockBits(bmpData);
            imgr.DrawImage(bmp, 0, 0);
            gr.DrawImage(image, 0, 0);
            gr.Dispose();
        }

        private void btnRotate90Left_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(image);
            Graphics imgr = Graphics.FromImage(image);
            Graphics gr = pictureBox1.CreateGraphics();

            imgr.Clear(Color.White);
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            imgr.DrawImage(bmp, 0, 0);

            gr.Clear(Color.White);
            gr.DrawImage(image, 0, 0);
            gr.Dispose();
            imgr.Dispose();


            //Graphics imgr = Graphics.FromImage(image);
            //Graphics gr = pictureBox1.CreateGraphics();

            //Bitmap bmp = new Bitmap(image);

            ////ѕолучаем размеры изображени€
            //Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            ////ѕолучаем "сырые данные" изображени€
            //BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            ////указатель на начальный байт
            //IntPtr ptr = bmpData.Scan0;

            ////размер в байтах
            //int size = bmpData.Stride * bmp.Height;

            ////выделить пам€ть дл€ массива байт изображени€
            //byte[] rgbValues = new byte[size];
            //byte[,] rgbValues2D = new byte[bmpData.Stride, bmp.Height];

            //// скопировать пам€ть изображени€ в массив байт
            //Marshal.Copy(ptr, rgbValues, 0, size);

            //int cnt = 0;
            //for (int i = 0; i < rgbValues2D.GetLength(0); i++)
            //{
            //    for (int j = 0; j < rgbValues2D.GetLength(1); j++)
            //    {
            //        rgbValues2D[i, j] = rgbValues[cnt++];
            //    }
            //}
            //cnt = 0;
            //int r = rgbValues2D.GetLength(0);
            //int c = rgbValues2D.GetLength(1);

            //for (int i = 0; i < r; i++)
            //{
            //    for (int j = 0; j < c; j++)
            //    {
            //        rgbValues2D[i, c - j - 1] = rgbValues[cnt++];
            //    }
            //}

            //cnt = 0;

            //for (int i = 0; i < r; i++)
            //{
            //    for (int j = 0; j < c; j++)
            //    {
            //        rgbValues[cnt++] = rgbValues2D[i, j];
            //    }
            //}

            //Marshal.Copy(rgbValues, 0, ptr, size);
            //bmp.UnlockBits(bmpData);
            //imgr.DrawImage(bmp, 0, 0);
            //gr.DrawImage(image, 0, 0);
            //gr.Dispose();

        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(image);
            Graphics imgr = Graphics.FromImage(image);
            Graphics gr = pictureBox1.CreateGraphics();

            imgr.Clear(Color.White);
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            imgr.DrawImage(bmp, 0, 0);

            gr.Clear(Color.White);
            gr.DrawImage(image, 0, 0);
            gr.Dispose();
            imgr.Dispose();
        }
    }
}