using System.Drawing;
using System.Text;

namespace GraphicsEditor
{
    /*2. Разработать простой графический редактор, который имеет следующие функции:
        - рисование примитивов: линии, карандаш, прямоугольник, эллипс, треугольник
        - возможность рисовать закрашенные фигуры
        - сохранение / загрузка рисунков в своём формате
        - сохранение рисунков в форматах: PNG, JPG, BMP
        - перерисовка картинки (Paint)
        - осветление / затемнение картинки (быстрый способ)
        - поворот картинки на 90, 180, 270 (быстрый способ)*/

    public partial class Form1 : Form
    {
        private Point mousePos;

        //для рисования линий
        private Point linePos;

        Color baseColor = Color.Blue;
        Image mainImage;

        //для перерисовки
        List<(string, Color, int, int, int, int, int)> redrawingLine = new List<(string, Color, int, int, int, int, int)>();
        List<(string, Color, int, int, int, int)> redrawingRectangle = new List<(string, Color, int, int, int, int)>();
        List<(string, Color, int, int, int, int)> redrawingEllipse = new List<(string, Color, int, int, int, int)>();
        List<(string, Color, Point[])> redrawingTriangle = new List<(string, Color, Point[])>();

        public Form1()
        {
            InitializeComponent();
            panelCurrentColor.BackColor = baseColor;
            mainImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = e.X;
            mousePos.Y = e.Y;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                if (checkAll.Checked == true && checkAll != checkLine)
                {
                    if (checkAll.Tag != null)
                    {
                        Graphics gr = pictureBox1.CreateGraphics();
                        switch (checkAll.Tag.ToString())
                        {
                            case "2":
                                    CreateRectange(gr);
                                break;
                            case "3":
                                    CreateEllipse(gr);
                                break;
                            case "4":
                                    CreateTriangle(gr);
                                break;
                        }
                        gr.Dispose();
                        break;
                    }
                }
            }
        }

        private void CreateRectange(Graphics gr)
        {
            // создание сплошной одноцветной кисти (принимает цвет)
            Brush brush1 = new SolidBrush(baseColor);
            // закрашенный прямоугольник
            if (checkFill.Checked)
            {
                gr.FillRectangle(brush1, mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                redrawingRectangle.Add(("RectangleF ", baseColor, mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text)));
            }
            else
            {
                gr.DrawRectangle(new Pen(brush1), mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                redrawingRectangle.Add(("RectangleD ", baseColor, mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text)));
            }
        }

        private void CreateEllipse(Graphics gr)
        {
            // создание сплошной одноцветной кисти (принимает цвет)
            Brush brush1 = new SolidBrush(baseColor);
            // закрашенный
            if (checkFill.Checked)
            {
                gr.FillEllipse(brush1, mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                redrawingEllipse.Add(("EllipseF ", baseColor, mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text)));
            }
            else
            {
                gr.DrawEllipse(new Pen(brush1), mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text));
                redrawingEllipse.Add(("EllipseD ", baseColor, mousePos.X, mousePos.Y, Convert.ToInt32(TBSizeWidth.Text), Convert.ToInt32(TBSizeHeight.Text)));
            }
        }

        private void CreateTriangle(Graphics gr)
        {
            // создание сплошной одноцветной кисти (принимает цвет)
            Brush brush1 = new SolidBrush(baseColor);

            // Массив точек треугольника.
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
                gr.FillPolygon(brush1, points);
                redrawingTriangle.Add(("TriangleF ", baseColor, points));
            }
            else
            {
                gr.DrawPolygon(new Pen(brush1), points);
                redrawingTriangle.Add(("TriangleD ", baseColor, points));
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                if (checkAll.Checked == true && checkAll == checkLine)
                {
                    linePos = mousePos;
                    break;
                }
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                if (checkAll.Checked == true && checkAll == checkLine)
                {
                    // Создание объекта для рисования в окне
                    Graphics gr = pictureBox1.CreateGraphics();

                    // Создать карандаш
                    Pen pen = new Pen(baseColor, Convert.ToInt32(comboBoxLineSize.SelectedItem));

                    // Рисование линии
                    gr.DrawLine(pen, e.X, e.Y, linePos.X, linePos.Y);

                    // Сохранение линии в списке
                    redrawingLine.Add(("Line ", baseColor, Convert.ToInt32(comboBoxLineSize.SelectedItem), e.X, e.Y, linePos.X, linePos.Y));
                    gr.Dispose();
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            // Перебрать все линии и нарисовать
            foreach (var pr in redrawingLine)
            {
                gr.DrawLine(new Pen(pr.Item2, pr.Item3), pr.Item4, pr.Item5, pr.Item6, pr.Item7);
            }

            //Перебрать все Rectangle
            foreach (var rect in redrawingRectangle)
            {
                if(rect.Item1.Equals("RectangleF "))
                {
                    gr.FillRectangle(new SolidBrush(rect.Item2), rect.Item3, rect.Item4, rect.Item5, rect.Item6);
                }
                if (rect.Item1.Equals("RectangleD "))
                {
                    gr.DrawRectangle(new Pen(rect.Item2), rect.Item3, rect.Item4, rect.Item5, rect.Item6);
                }
            }

            //Перебрать все Ellipse
            foreach (var ellipse in redrawingEllipse)
            {
                if (ellipse.Item1.Equals("EllipseF "))
                {
                    gr.FillEllipse(new SolidBrush(ellipse.Item2), ellipse.Item3, ellipse.Item4, ellipse.Item5, ellipse.Item6);
                }
                if (ellipse.Item1.Equals("EllipseD "))
                {
                    gr.DrawEllipse(new Pen(ellipse.Item2), ellipse.Item3, ellipse.Item4, ellipse.Item5, ellipse.Item6);
                }
            }

            //Перебрать все Triangle
            foreach (var ellipse in redrawingTriangle)
            {
                if (ellipse.Item1.Equals("TriangleF "))
                {
                    gr.FillPolygon(new SolidBrush(ellipse.Item2), ellipse.Item3);
                }
                if (ellipse.Item1.Equals("TriangleD "))
                {
                    gr.DrawPolygon(new Pen(ellipse.Item2), ellipse.Item3);
                }
            }
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
            redrawingLine.Clear();
            redrawingRectangle.Clear();
            redrawingEllipse.Clear();
            redrawingTriangle.Clear();
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            gr.Dispose();
        }

        private void buttonBackSpace_Click(object sender, EventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                CheckBox checkAll = item as CheckBox;
                if (checkAll != null)
                    if (checkAll.Checked == true)
                    {
                        switch((string)checkAll.Tag)
                        {
                            case "1":
                                if (redrawingLine.Count > 0)
                                    redrawingLine.Remove(redrawingLine[redrawingLine.Count - 1]);
                                    break;
                            case "2":
                                if (redrawingRectangle.Count > 0)
                                    redrawingRectangle.Remove(redrawingRectangle[redrawingRectangle.Count - 1]);
                                break;
                            case "3":
                                if (redrawingEllipse.Count > 0)
                                    redrawingEllipse.Remove(redrawingEllipse[redrawingEllipse.Count - 1]);
                                break;
                            case "4":
                                if (redrawingTriangle.Count > 0)
                                    redrawingTriangle.Remove(redrawingTriangle[redrawingTriangle.Count - 1]);
                                break;
                        }
                    }
            }
            pictureBox1.Invalidate();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save image";
            saveFileDialog1.Filter = "Image files|*.bvg";
            
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                // Перебрать все линии и нарисовать
                foreach (var pr in redrawingLine)
                {
                    sw.WriteLine(pr.Item1 + "[" + pr.Item2.R +"." + pr.Item2.G + "." + pr.Item2.B + ".]" + pr.Item3 + "." + pr.Item4 + "." + pr.Item5 + "." + pr.Item6 + "." + pr.Item7 + ".");
                }
                //Перебрать все Rectangle
                foreach (var rect in redrawingRectangle)
                {
                    sw.WriteLine(rect.Item1 + "[" + rect.Item2.R + "." + rect.Item2.G + "." + rect.Item2.B + ".]" + rect.Item3 + "." + rect.Item4 + "." + rect.Item5 + "." + rect.Item6 + ".");
                }

                //Перебрать все Ellipse
                foreach (var ellipse in redrawingEllipse)
                {
                    sw.WriteLine(ellipse.Item1 + "[" + ellipse.Item2.R + "." + ellipse.Item2.G + "." + ellipse.Item2.B + ".]" + ellipse.Item3 + "." + ellipse.Item4 + "." + ellipse.Item5 + "." + ellipse.Item6 + ".");
                }

                //Перебрать все Triangle
                foreach (var triangle in redrawingTriangle)
                {
                    sw.Write(triangle.Item1 + "[" + triangle.Item2.R + "." + triangle.Item2.G + "." + triangle.Item2.B + ".]");
                    foreach (var point in triangle.Item3)
                    {
                        sw.Write(point.X + "." + point.Y + ".");
                    }
                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        //Скорей всего можно переделать и сохранять 0-255 3шт.
        private Color CheckColor(string line)
        {
            Color currentColor = Color.Black;
            int[] bufferColor = new int[3];

            int start = line.IndexOf('[') + 1;
            
            StringBuilder color = new StringBuilder();
            for (int i = start, j = 0; line[i] != ']'; i++)
            {
                if (line[i] != '.')
                {
                    color.Append(line[i]);
                }
                else
                {
                    bufferColor[j++] = Convert.ToInt32(color.ToString());
                    color.Clear();
                }
               
            }
            currentColor = Color.FromArgb(bufferColor[0], bufferColor[1], bufferColor[2]);

            return currentColor;
        }

        private void LoadLine(string line, Graphics gr, Brush brush)
        {
            //следующий символ после цвета
            int startCoord = line.IndexOf("]") + 1;
            StringBuilder sb = new StringBuilder();
            int[] buffer = new int[5];

            for (int i = startCoord, j = 0; i < line.Length; i++)
            {
                if (line[i] != '.')
                {
                    sb.Append(line[i]);
                }
                else
                {
                    buffer[j++] = Convert.ToInt32(sb.ToString());
                    sb.Clear();
                }
            }

            Pen pen = new Pen(brush, buffer[0]);
            gr.DrawLine(pen, buffer[1], buffer[2], buffer[3], buffer[4]);
            // Сохранение линии в списке
            redrawingLine.Add(("Line ", baseColor, buffer[0], buffer[1], buffer[2], buffer[3], buffer[4]));
        }

        private void LoadRectangleFill(string line, Graphics gr, Brush brush)
        {
            //следующий символ после цвета
            int startCoord = line.IndexOf("]") + 1;

            StringBuilder sb = new StringBuilder();
            int[] buffer = new int[4];
            for (int i = startCoord, j = 0; i < line.Length; i++)
            {
                if (line[i] != '.')
                {
                    sb.Append(line[i]);
                }
                else
                {
                    buffer[j++] = Convert.ToInt32(sb.ToString());
                    sb.Clear();
                }
            }
            gr.FillRectangle(brush, buffer[0], buffer[1], buffer[2], buffer[3]);
            redrawingRectangle.Add(("RectangleF ", baseColor, buffer[0], buffer[1], buffer[2], buffer[3]));
        }

        private void LoadRectangleDraw(string line, Graphics gr, Brush brush)
        {
            //следующий символ после цвета
            int startCoord = line.IndexOf("]") + 1;

            StringBuilder sb = new StringBuilder();
            int[] buffer = new int[4];
            for (int i = startCoord, j = 0; i < line.Length; i++)
            {
                if (line[i] != '.')
                {
                    sb.Append(line[i]);
                }
                else
                {
                    buffer[j++] = Convert.ToInt32(sb.ToString());
                    sb.Clear();
                }
            }

            gr.DrawRectangle(new Pen(brush), buffer[0], buffer[1], buffer[2], buffer[3]);
            redrawingRectangle.Add(("RectangleD ", baseColor, buffer[0], buffer[1], buffer[2], buffer[3]));
        }

        private void LoadEllipseFill(string line, Graphics gr, Brush brush)
        {
            //следующий символ после цвета
            int startCoord = line.IndexOf("]") + 1;

            StringBuilder sb = new StringBuilder();
            int[] buffer = new int[4];
            for (int i = startCoord, j = 0; i < line.Length; i++)
            {
                if (line[i] != '.')
                {
                    sb.Append(line[i]);
                }
                else
                {
                    buffer[j++] = Convert.ToInt32(sb.ToString());
                    sb.Clear();
                }
            }

            gr.FillEllipse(brush, buffer[0], buffer[1], buffer[2], buffer[3]);
            redrawingEllipse.Add(("EllipseF ", baseColor, buffer[0], buffer[1], buffer[2], buffer[3]));
        }

        private void LoadEllipseDraw(string line, Graphics gr, Brush brush)
        {
            //следующий символ после цвета
            int startCoord = line.IndexOf("]") + 1;

            StringBuilder sb = new StringBuilder();
            int[] buffer = new int[4];
            for (int i = startCoord, j = 0; i < line.Length; i++)
            {
                if (line[i] != '.')
                {
                    sb.Append(line[i]);
                }
                else
                {
                    buffer[j++] = Convert.ToInt32(sb.ToString());
                    sb.Clear();
                }
            }

            gr.DrawEllipse(new Pen(brush), buffer[0], buffer[1], buffer[2], buffer[3]);
            redrawingEllipse.Add(("EllipseD ", baseColor, buffer[0], buffer[1], buffer[2], buffer[3]));
        }

        private void LoadTriangeFill(string line, Graphics gr, Brush brush)
        {
            //следующий символ после цвета
            int startCoord = line.IndexOf("]") + 1;

            StringBuilder sb = new StringBuilder();
            int[] buffer = new int[6];
            // Массив точек треугольника.
            Point[] points = new Point[3];
            
            for (int i = startCoord, j = 0; i < line.Length; i++)
            {
                if (line[i] != '.')
                {
                    sb.Append(line[i]);
                }
                else
                {
                    buffer[j++] = Convert.ToInt32(sb.ToString());
                    sb.Clear();
                }
            }
            points[0].X = buffer[0]; points[0].Y = buffer[1];
            points[1].X = buffer[2]; points[1].Y = buffer[3];
            points[2].X = buffer[4]; points[2].Y = buffer[5];
            gr.FillPolygon(brush, points);
            redrawingTriangle.Add(("TriangleF ", baseColor, points));
        }

        private void LoadTriangeDraw(string line, Graphics gr, Brush brush)
        {
            //следующий символ после цвета
            int startCoord = line.IndexOf("]") + 1;

            StringBuilder sb = new StringBuilder();
            int[] buffer = new int[6];
            // Массив точек треугольника.
            Point[] points = new Point[3];

            for (int i = startCoord, j = 0; i < line.Length; i++)
            {
                if (line[i] != '.')
                {
                    sb.Append(line[i]);
                }
                else
                {
                    buffer[j++] = Convert.ToInt32(sb.ToString());
                    sb.Clear();
                }
            }
            points[0].X = buffer[0]; points[0].Y = buffer[1];
            points[1].X = buffer[2]; points[1].Y = buffer[3];
            points[2].X = buffer[4]; points[2].Y = buffer[5];
            gr.DrawPolygon(new Pen(brush), points);
            redrawingTriangle.Add(("TriangleD ", baseColor, points));
        }


        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //очистка
            newImageToolStripMenuItem_Click(null, null);

            openFileDialog1.Title = "Load Image";
            openFileDialog1.Filter = "Image files|*.bvg";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                Graphics gr = pictureBox1.CreateGraphics();
                Brush brush;

                //сначала присваиваем переменной line результат функции reader.ReadLineAsync(),
                //а затем проверяем, не равна ли она null.
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    baseColor = CheckColor(line);
                    brush = new SolidBrush(baseColor);
                    if (line.StartsWith("Line"))
                    {
                        LoadLine(line, gr, brush);
                    }


                    if (line.StartsWith("RectangleF"))
                    {
                        LoadRectangleFill(line, gr, brush);
                    }

                    if (line.StartsWith("RectangleD"))
                    {
                        LoadRectangleDraw(line, gr, brush);
                    }


                    if (line.StartsWith("EllipseF"))
                    {
                        LoadEllipseFill(line, gr, brush);
                    }

                    if (line.StartsWith("EllipseD"))
                    {
                        LoadEllipseDraw(line, gr, brush);
                    }

                    if(line.StartsWith("TriangleF"))
                    {
                        LoadTriangeFill(line, gr, brush);
                    }
                    if (line.StartsWith("TriangleD"))
                    {
                        LoadTriangeDraw(line, gr, brush);
                    }
                }

                gr.Dispose();
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
    }
}


//switch (color.ToString())
//{
//    case "Red":
//        currentColor = Color.Red;
//        break;
//    case "Green":
//        currentColor = Color.Green;
//        break;
//    case "Blue":
//        currentColor = Color.Blue;
//        break;
//    case "Black":
//        currentColor = Color.Black;
//        break;
//    case "Yellow":
//        currentColor = Color.Yellow;
//        break;
//    case "Cyan":
//        currentColor = Color.Cyan;
//        break;
//    case "Gray":
//        currentColor = Color.Gray;
//        break;
//    case "Purple":
//        currentColor = Color.Purple;
//        break;
//    case "Aqua":
//        currentColor = Color.Aqua;
//        break;
//    case "White":
//        currentColor = Color.White;
//        break;
//}