using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.ConstrainedExecution;

namespace Image_Move
{
    public partial class Form1 : Form
    {

        /*«адача. ¬ окне есть небольша€ картинка, котора€ изначально находитс€ в центре окна. 
         * огда пользователь щЄлкает мышью к какое-то место в окне, картинка плавно туда перемещаетс€ по таймеру.*/

        int posX, posY;
        int imagePosX, imagePosY;
        int newImagePosX, newImagePosY;

        int deltaX, deltaY;
        int signX, signY;
        int error, error2;

        float rorationAngle = 0.01f;
        public Form1()
        {
            InitializeComponent();
            imagePosX = pictureBox1.Location.X;
            imagePosY = pictureBox1.Location.Y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //получение координат клика
            newImagePosX = posX-50;
            newImagePosY = posY-20;

            ////»зменени€ координат
            deltaX = Math.Abs(newImagePosX - imagePosX); // возвращает абсолютное значение( Math.Abs(-12.4); // 12.4 )
            deltaY = Math.Abs(newImagePosY - imagePosY);
            //Ќаправление
            signX = imagePosX < newImagePosX ? 1 : -1;
            signY = imagePosY < newImagePosY ? 1 : -1;
            error = deltaX - deltaY;
            timerImageMove.Start();
        }

        Bitmap RotateImage2(float angleDegrees, bool upsizeOk, bool clipOk, Color backgroundColor)
        {
            // Test for zero rotation and return a clone of the input image
            if (angleDegrees == 0f)
                return (Bitmap)pictureBox1.BackgroundImage.Clone();

            // Set up old and new image dimensions, assuming upsizing not wanted and clipping OK
            int oldWidth = pictureBox1.BackgroundImage.Width;
            int oldHeight = pictureBox1.BackgroundImage.Height;
            int newWidth = oldWidth;
            int newHeight = oldHeight;
            float scaleFactor = 1f;

            // If upsizing wanted or clipping not OK calculate the size of the resulting bitmap
            if (upsizeOk || !clipOk)
            {
                double angleRadians = angleDegrees * Math.PI / 180d;

                double cos = Math.Abs(Math.Cos(angleRadians));
                double sin = Math.Abs(Math.Sin(angleRadians));
                newWidth = (int)Math.Round(oldWidth * cos + oldHeight * sin);
                newHeight = (int)Math.Round(oldWidth * sin + oldHeight * cos);
            }

            // If upsizing not wanted and clipping not OK need a scaling factor
            if (!upsizeOk && !clipOk)
            {
                scaleFactor = Math.Min((float)oldWidth / newWidth, (float)oldHeight / newHeight);
                newWidth = oldWidth;
                newHeight = oldHeight;
            }

            // Create the new bitmap object. If background color is transparent it must be 32-bit, 
            //  otherwise 24-bit is good enough.
            Bitmap newBitmap = new Bitmap(newWidth, newHeight, backgroundColor == Color.Transparent ?
                                             PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
            newBitmap.SetResolution(pictureBox1.BackgroundImage.HorizontalResolution, pictureBox1.BackgroundImage.VerticalResolution);

            // Create the Graphics object that does the work
            using (Graphics graphicsObject = Graphics.FromImage(newBitmap))
            {
                graphicsObject.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsObject.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsObject.SmoothingMode = SmoothingMode.HighQuality;

                // Fill in the specified background color if necessary
                if (backgroundColor != Color.Transparent)
                    graphicsObject.Clear(backgroundColor);

                // Set up the built-in transformation matrix to do the rotation and maybe scaling
                graphicsObject.TranslateTransform(newWidth / 2f, newHeight / 2f);

                if (scaleFactor != 1f)
                    graphicsObject.ScaleTransform(scaleFactor, scaleFactor);

                graphicsObject.RotateTransform(angleDegrees);
                graphicsObject.TranslateTransform(-oldWidth / 2f, -oldHeight / 2f);

                // Draw the result 
                graphicsObject.DrawImage(pictureBox1.BackgroundImage, 0, 0);
            }

            return newBitmap;
        }

        private Bitmap RotateImage()
        {
            Bitmap bmp = new Bitmap(pictureBox1.BackgroundImage.Width, pictureBox1.BackgroundImage.Height);

            //превращаем Bitmap в объект Graphics
            Graphics gfx = Graphics.FromImage(bmp);
            //устанавливаем точку вращени€ в центр нашего изображени€
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            //поворачиваем изображение
            gfx.RotateTransform(rorationAngle+= 0.05f);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //установите дл€ InterpolationMode значение HighQualityBicubic, чтобы обеспечить высокое
            //качество изображени€ после преобразовани€ до заданного размера
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //“еперь рисуем наше новое изображение на графическом объекте.
            gfx.DrawImage(pictureBox1.BackgroundImage, new Point(0, 0));

            //избавитьс€ от объекта Graphics
            gfx.Dispose();

            return bmp;
        }

        
        private void timerImageMove_Tick(object sender, EventArgs e)
        {
            error2 = error * 2;
            if (error2 > -deltaY)
            {
                error -= deltaY;
                imagePosX += signX;
            }
            if (error2 < deltaX)
            {
                error += deltaX;
                imagePosY += signY;
            }

            //pictureBox1.BackgroundImage = RotateImage();

            pictureBox1.Location = new Point(imagePosX, imagePosY);

            if (imagePosX == newImagePosX || imagePosY == newImagePosY)
            {
                imagePosX = newImagePosX;
                imagePosY = newImagePosY;
                timerImageMove.Stop();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            posX = e.X;
            posY = e.Y;
        }
    }
}