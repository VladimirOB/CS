using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GraphicsEditor_v3
{
    class Layer
    {
        public Bitmap image;
        private float transparency; // прозрачность

        public bool Visible { get; set; }
        
        public string Name { get; set; }

        public float Transparency
        {
            get { return transparency; }
            set { transparency = value; }
        }

        public Layer(int x, int y, float transp, string name)
        {
            transparency = transp;
            image = new Bitmap(x, y);
            Visible = true;
            Name = name;
        }
    }

    delegate void LayersChanged(BitmapLayers layers);

    // класс всех слоёв
    class BitmapLayers
    {
        int width, height; //размеры слоёв

        public int widthBrush = 1;

        public Color baseColor = Color.Black;

        byte[] ScrPic;
        byte[] ResPic;
        byte[] tempPic;

        public event LayersChanged OnLayersChanged;

        PictureBox container = null;

        Bitmap res;           // создание результирующей картинки
        Graphics resgr;       // graphics для результирующей картинки
        ImageAttributes attr; // создание атрибутов изображения
        ColorMatrix myColorMatrix;

        public Color BaseColor
        {
            get { return baseColor; }
            set { baseColor = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public List<Layer> lst = new List<Layer>();

        public BitmapLayers(int x, int y, PictureBox pictureBox)
        {
            width = x;
            height = y;
            res = new Bitmap(width, height);         // создание результирующей картинки
            resgr = Graphics.FromImage(res);       // graphics для результирующей картинки
            attr = new ImageAttributes();   // создание атрибутов изображения
            myColorMatrix = new ColorMatrix();

            lst.Add(new Layer(width, height, 1, "Layer 0(main)"));
            container = pictureBox;
        }
        
        ~BitmapLayers()
        {
            resgr.Dispose();
        }

        public void LayersRefresh()
        {
            OnLayersChanged?.Invoke(this);
        }

        public void Add(float tranparency, string name = "")
        {
            Layer layer = new Layer(Width, Height, tranparency, name.Length == 0 ? $"Layer {lst.Count}" : name);
            lst.Add(layer);
            OnLayersChanged?.Invoke(this);
        }

        public void Remove(int n)
        {
            if (n < lst.Count)
                lst.RemoveAt(n);

            OnLayersChanged?.Invoke(this);
        }

        public void Invalidate()
        {
            Bitmap bitmap = GetResultImage();

            // выбор результирующей картинки для показа в picturebox
            container.Image = bitmap;
        }

        // объединение слоёв
        Bitmap GetResultImage()
        {
            resgr.Clear(Color.White);
            // обеспечение прозрачности слоёв за счёт замены белого цвета на прозрачный
            attr.SetColorKey(Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255)); // белый цвет делаем прозрачным

            //int w, h;
            //if (lst[0].Visible)
            //{
            //    // каждый слой хранить в массиве байт и если не прозрачный байт, закидывать в результирующий массив байт
            //    // массивы байт должны быть для каждого слоя и результирующий массив байт
            //    Bitmap bmp = lst[0].image;

            //    w = bmp.Width;
            //    h = bmp.Height;

            //    Rectangle rect = new Rectangle(0, 0, w, h);
            //    BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            //    IntPtr ptr = bmpData.Scan0;
            //    int bytes = bmpData.Stride * h;
            //    tempPic = new byte[bytes];
            //    Marshal.Copy(ptr, tempPic, 0, bytes);

            //    bmp.UnlockBits(bmpData);

            //    lst[0].image = bmp;
            //}
               

            // рисование фона на картинке
            if (lst[0].Visible)
                resgr.DrawImage(lst[0].image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);


            // отображение всех слоёв на результирующей картинке с учётом прозрачности
            for (int k = 1; k < lst.Count; k++)
            {
                if (lst[k].Visible)
                {
                    // матрица цветов задаёт прозрачноть для каждого слоя
                    myColorMatrix.Matrix00 = 1.00f;
                    myColorMatrix.Matrix11 = 1.00f;
                    myColorMatrix.Matrix22 = 1.00f;
                    myColorMatrix.Matrix33 = lst[k].Transparency;

                    attr.SetColorMatrix(myColorMatrix); // применение матрицы

                    // отображение слоя
                    resgr.DrawImage(lst[k].image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attr);
                }
            }
            return res;
        }

        public void DrawBrush(int layerIndex, Point x1, Point x2)
        {
            if(layerIndex >= 0 && layerIndex < lst.Count)
            {
                Image layer = lst[layerIndex].image;
                Graphics imgr = Graphics.FromImage(layer);
                imgr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                Pen pen = new Pen(baseColor, widthBrush);
                imgr.DrawLine(pen, x1.X, x1.Y, x2.X, x2.Y);
                imgr.Dispose();
            }
        }

        public void Grater(int layerIndex, Point x1, Point x2)
        {
            if (layerIndex >= 0 && layerIndex < lst.Count)
            {
                Graphics imgr = Graphics.FromImage(lst[layerIndex].image);
                Pen pen = new Pen(Color.White, widthBrush);
                imgr.DrawLine(pen, x1.X, x1.Y, x2.X, x2.Y);
                imgr.DrawLine(new Pen(Color.Transparent), x1.X, x1.Y, x2.X, x2.Y);
                imgr.Dispose();
            }
        }

        public void DrawLine(int layerIndex, int x1, int y1, int x2, int y2)
        {
            if (layerIndex >= 0 && layerIndex < lst.Count)
            {
                Graphics imgr = Graphics.FromImage(lst[layerIndex].image);
                Pen pen = new Pen(baseColor, widthBrush);
                imgr.DrawLine(pen, x1, y1, x2, y2);
                imgr.Dispose();
            }
        }
        public void DrawRectangle(int layerIndex, int x1, int y1, int x2, int y2, bool checkFill)
        {
            if (layerIndex >= 0 && layerIndex < lst.Count)
            {
                Graphics imgr = Graphics.FromImage(lst[layerIndex].image);
                Brush brush1 = new SolidBrush(baseColor);

                if (checkFill)
                {
                    imgr.FillRectangle(brush1, x1, y1, x2, y2);
                }
                else
                {
                    imgr.DrawRectangle(new Pen(brush1), x1, y1, x2, y2);
                }
                imgr.Dispose();
            }
        }

        public void DrawEllipse(int layerIndex, int x1, int y1, int x2, int y2, bool checkFill)
        {
            if (layerIndex >= 0 && layerIndex < lst.Count)
            {
                Graphics imgr = Graphics.FromImage(lst[layerIndex].image);
                Brush brush1 = new SolidBrush(baseColor);

                if (checkFill)
                {
                    imgr.FillEllipse(brush1, x1, y1, x2, y2);
                }
                else
                {
                    imgr.DrawEllipse(new Pen(brush1), x1, y1, x2, y2);
                }
                imgr.Dispose();
            }
        }

        public void DrawTriangle(int layerIndex, int x1, int y1, int x2, int y2, bool checkFill)
        {
            if (layerIndex >= 0 && layerIndex < lst.Count)
            {
                Graphics imgr = Graphics.FromImage(lst[layerIndex].image);
                Brush brush1 = new SolidBrush(baseColor);

                // Массив точек треугольника.
                Point[] points = new Point[3];
                points[0].X = x1;
                points[0].Y = y1;

                points[1].X = x1 + 100;
                points[1].Y = y1;

                points[2].X = x2;
                points[2].Y = y2;

                if (checkFill)
                {
                    imgr.FillPolygon(brush1, points);
                }
                else
                {
                    imgr.DrawPolygon(new Pen(brush1), points);
                }
                imgr.Dispose();
            }
        }

        public void Rotate(int layerIndex, bool flag)
        {
            if (layerIndex >= 0 && layerIndex < lst.Count)
            {
                Graphics imgr = Graphics.FromImage(lst[layerIndex].image);
                if (flag)
                    lst[layerIndex].image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                else
                    lst[layerIndex].image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                imgr.Dispose();
            }
        }

        public void OpenFile(int layerIndex, string fileName)
        {
            if (layerIndex >= 0 && layerIndex < lst.Count)
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                Bitmap bmp = (Bitmap)Bitmap.FromStream(stream);
                stream.Close();
                lst[layerIndex].image = bmp;
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = bmpData.Stride * bmp.Height;

                ScrPic = new byte[bytes];
                ResPic = new byte[bytes];

                Marshal.Copy(ptr, ScrPic, 0, bytes);
                Marshal.Copy(ptr, ResPic, 0, bytes);
                bmp.UnlockBits(bmpData);
            }
        }

        public void SaveImage(string fileName)
        {
            // создание результирующей картинки и графики для неё
            Bitmap res = GetResultImage();
            
            res.Save(fileName);
        }

        public void SaveBVG(string fileName)
        {
            string dir = Path.GetDirectoryName(fileName);
            fileName = fileName.Replace(dir, "").Replace(".bvg", "");
            DirectoryInfo dinfo = new DirectoryInfo(dir + fileName);
            if (!dinfo.Exists)
            {
                dinfo.Create();
            }

            // создание результирующей картинки и графики для неё
            Bitmap res = new Bitmap(Width, Height);
            Graphics resgr = Graphics.FromImage(res);
            //Атрибуты изображения
            ImageAttributes attr = new ImageAttributes();

            byte layersCnt = 1;

            // обеспечение прозрачности слоёв за счёт замены белого цвета на прозрачный
            attr.SetColorKey(Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255));
            //рисование фона (1й слой)
            resgr.DrawImage(lst[0].image, new Rectangle(0, 0, Width, Height), 0, 0, Width, Height, GraphicsUnit.Pixel);

            res.Save($"{dinfo.FullName}\\Layer{layersCnt++}.bvg");

            //отображение остальных слоёв на результирующей картинке
            for (int i = 1; i < lst.Count; i++)
            {
                resgr.Clear(Color.Transparent);
                // матрица цветов задаёт прозрачноть для каждого слоя
                ColorMatrix myColorMatrix = new ColorMatrix();
                myColorMatrix.Matrix00 = 1f;
                myColorMatrix.Matrix11 = 1f;
                myColorMatrix.Matrix22 = 1f;
                myColorMatrix.Matrix33 = lst[i].Transparency;

                // применение матрицы
                attr.SetColorMatrix(myColorMatrix);

                //рисуем слой. (attr в конце)
                resgr.DrawImage(lst[i].image, new Rectangle(0, 0, Width, Height), 0, 0, Width, Height, GraphicsUnit.Pixel, attr);
                res.Save($"{dinfo.FullName}\\Layer{layersCnt++}.bvg");
            }
            resgr.Dispose();
        }

        public void Brightness(int layerIndex, int value)
        {
            if(ScrPic != null)
            {
                Graphics imgr = Graphics.FromImage(lst[layerIndex].image);
                int len = ScrPic.Length;

                // Осветлить изображение
                // Цикл по изображению (модификация изображения)
                for (int counter = 0; counter < len; counter++)
                {
                    int newValue = ScrPic[counter] + value;
                    if (newValue > 255)
                        newValue = 255;
                    if (newValue < 0)
                        newValue = 0;

                    ResPic[counter] = (byte)newValue;
                }

                Rectangle rect = new Rectangle(0, 0, lst[layerIndex].image.Width, lst[layerIndex].image.Height);

                BitmapData bmpData = lst[layerIndex].image.LockBits(rect, ImageLockMode.WriteOnly, lst[layerIndex].image.PixelFormat);

                IntPtr ptr = bmpData.Scan0;

                Marshal.Copy(ResPic, 0, ptr, ResPic.Length);

                lst[layerIndex].image.UnlockBits(bmpData);
                imgr.Dispose();
            }
        }

        public void DeleteLayer(int pos)
        {
            lst.RemoveAt(pos);
        }

        // показ слоёв в picturebox
        public void Show(PictureBox pic)
        {
            // создание результирующей картинки и графики для неё
            Bitmap res = new Bitmap(Width, Height);
            Graphics resgr = Graphics.FromImage(res);

            //Атрибуты изображения
            ImageAttributes attr = new ImageAttributes();

            // обеспечение прозрачности слоёв за счёт замены белого цвета на прозрачный
            attr.SetColorKey(Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255));

            //рисование фона (1й слой)
            resgr.DrawImage(lst[0].image, new Rectangle(0, 0, Width, Height), 0, 0, Width, Height, GraphicsUnit.Pixel);

            //отображение остальных слоёв на результирующей картинке
            for (int i = 1; i < lst.Count; i++)
            {
                // матрица цветов задаёт прозрачноть для каждого слоя
                ColorMatrix myColorMatrix = new ColorMatrix();
                myColorMatrix.Matrix00 = 1f;
                myColorMatrix.Matrix11 = 1f;
                myColorMatrix.Matrix22 = 1f;
                myColorMatrix.Matrix33 = lst[i].Transparency;

                // применение матрицы
                attr.SetColorMatrix(myColorMatrix);

                //рисуем слой. (attr в конце)
                resgr.DrawImage(lst[i].image, new Rectangle(0, 0, Width, Height), 0, 0, Width, Height, GraphicsUnit.Pixel, attr);
            }

            // Добавляем результирующую картинку в PB
            pic.Image = res;
            resgr.Dispose();
        }
    }
}
