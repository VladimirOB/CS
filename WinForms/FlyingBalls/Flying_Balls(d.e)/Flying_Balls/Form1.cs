using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Flying_Balls
{
    public partial class Form1 : Form
    {
        // Загрузить картинку из файла с изображением шарика
        Bitmap ballImage = new Bitmap(@"ball.png");

        // Загрузить картинку из файла с изображением стены
        Bitmap wallImage = new Bitmap(@"wall.bmp");

        // Список элементов управления, отображающих картинки на форме
        List<PictureBox> balls = new List<PictureBox>();

        // Список элементов управления, отображающих стены на форме
        List<PictureBox> walls = new List<PictureBox>();

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();

            // Старт таймера, для движения шариков
            timer1.Start();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Если нажата левая кнопка мыши
            if (e.Button == MouseButtons.Left)
            {
                // Создание элемента управления, содержащего картинку шарика
                PictureBox ballPictureBox = new PictureBox();

                // Указать, чтобы элемента управления показывал шарик 
                ballPictureBox.Image = ballImage;

                // Задать координаты (берутся из координат шелчка мышью) и размеры шарика (берутся из картинки)
                ballPictureBox.Left = e.X;
                ballPictureBox.Top = e.Y;
                ballPictureBox.Width = ballImage.Width;
                ballPictureBox.Height = ballImage.Height;

                // Задать для видимого шарика обработчик нажатия мышью
                ballPictureBox.MouseClick += BallPictureBox_MouseClick;

                // Добавить шарик на форму
                ballPictureBox.Parent = this;

                // Сопоставить шарику объект, хранящий его точные координаты и пошаговые смещения
                ballPictureBox.Tag = new Ball(e.X, e.Y, (rand.Next(-100, 100) / (float)100), (rand.Next(-100, 100) / (float)100));

                // Добавить шарик в список движущихся шариков
                balls.Add(ballPictureBox);
            }
            else    // Нажата НЕ левая кнопка мыши
            {
                // Создание элемента управления, содержащего картинку шарика
                PictureBox wallPictureBox = new PictureBox();

                // Указать, чтобы элемента управления показывал шарик 
                wallPictureBox.Image = wallImage;

                // Задать координаты (берутся из координат шелчка мышью) и размеры шарика (берутся из картинки)
                wallPictureBox.Left = e.X;
                wallPictureBox.Top = e.Y;
                wallPictureBox.Width = wallImage.Width;
                wallPictureBox.Height = wallImage.Height;

                // Задать для видимой стены обработчик нажатия мышью
                wallPictureBox.MouseClick += WallPictureBox_MouseClick;

                // добавление в список стен
                walls.Add(wallPictureBox);

                // Добавить стену на форму
                wallPictureBox.Parent = this;
            }
        }

        private void WallPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            // Удаление стены из списка стен
            walls.Remove((PictureBox)sender);

            // Удаление видимой стены из окна
            this.Controls.Remove((PictureBox)sender);
        }

        // Обработчик нажатия по шарику
        private void BallPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            // Удаление шарика из списка шариков
            balls.Remove((PictureBox)sender);

            // Удаление видимого шарика из окна
            this.Controls.Remove((PictureBox)sender);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Цикл по всем шарикам
            for (int i=0; i<balls.Count; i++)
            {
                PictureBox currentBall = balls[i];

                // Для текущего шарика получить объект содержащий информацию о точном местоположении и пошаговых смещениях
                Ball ballInfo = (Ball)currentBall.Tag;

                // Если шарик вышел за границы окна по X
                if(ballInfo.X <= 0 || ballInfo.X + currentBall.Width >= this.ClientRectangle.Width)
                {
                    // Изменить направление движения шарика по X на противоположное
                    ballInfo.DeltaX *= -1;
                }

                // Если шарик вышел за границы окна по Y
                if (ballInfo.Y <= 0 || ballInfo.Y + currentBall.Height >= this.ClientRectangle.Height)
                {
                    // Изменить направление движения шарика по Y на противоположное
                    ballInfo.DeltaY *= -1;
                }

                // Прибавить пошаговые смещения точным координатам шарика
                ballInfo.X += ballInfo.DeltaX;
                ballInfo.Y += ballInfo.DeltaY;

                // проработка столкновений со стенами
                // Получить оконтуривающий прямоугольник для текущего шарика
                RectangleF ballRect = new RectangleF(ballInfo.X, ballInfo.Y, currentBall.Width, currentBall.Height);

                // Перебрать все стены
                foreach (PictureBox currentWall in walls)
                {
                    // Получить оконтуривающий прямоугольник для текущей стены
                    RectangleF wallRect = new RectangleF(currentWall.Left, currentWall.Top, currentWall.Width, currentWall.Height);

                    // Если прямоугольники текущего шарика и текущей стены пересекаются
                    if (wallRect.IntersectsWith(ballRect))
                    {
                        // Удаление шарика из списка шариков
                        balls.Remove(currentBall);

                        // Компенсировать удаление шарика в индексе
                        i--;

                        // Удаление видимого шарика из окна
                        this.Controls.Remove(currentBall);

                        return;
                    }
                }

                // Переместить шарик в окне
                currentBall.Left = (int)ballInfo.X;
                currentBall.Top = (int)ballInfo.Y;
            }
        }
    }
}
