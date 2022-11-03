using System;
using System.Windows.Forms;

namespace MouseBall
{
    public partial class Form1 : Form
    {
        // Конечные координаты движения шарика
        double x2, y2;

        // Текущие координаты шарика
        double currentX, currentY;
        
        // Приращение координат шарика (может быть отрицательным)
        // Расстояние, которое пройдёт шарик за 1 шаг
        double deltaX, deltaY;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Остановить таймер
            timer1.Stop();

            // Занести начальные координаты шарика в текущие координаты
            currentX = pictureBox1.Left;
            currentY = pictureBox1.Top;

            // Вычислить приращение координат шарика за 1 шаг таймера (при общих 100 шагах)
            deltaX = (e.X - currentX) / 100;
            deltaY = (e.Y - currentY) / 100;

            // Занести координаты мыши в конечные координаты движения шарика
            x2 = e.X;
            y2 = e.Y;

            // Запуск таймера
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Получение новых координат шарика
            // Изменение X-координаты шарика
            currentX = currentX + deltaX;

            // Изменение Y-координаты шарика
            currentY = currentY + deltaY;

            // Если шарик находится максимально близко от финальной точки - остановить движение шарика
            // Math.Abs(currentY - y2) - расстояние от текущей Y-координаты шарика до Y-финальной точки
            // Math.Abs(currentX - x2) - расстояние от текущей X-координаты шарика до X-финальной точки

            //if (currentY == y2 && currentX == x2)
            if (Math.Abs(currentY - y2) < 1 && Math.Abs(currentX - x2) < 1)
                timer1.Stop();

            // Переместить шарик в новые координаты
            pictureBox1.Top = (int)currentY;
            pictureBox1.Left = (int)currentX;
        }
    }
}
