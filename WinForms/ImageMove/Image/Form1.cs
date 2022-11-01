using System.Runtime.ConstrainedExecution;

namespace Image
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