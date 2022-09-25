using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mozgovoi_M.B_
{
    class Chapter1_FallingBall
    {
        private const int screenWidth = 60;
        private const int screenHeight = 30;
        bool isRuning = false;
        int X, Y; // координаты молекулы
        double Vx, Vy; // состовляющие скорости молекулы
        const int R = 1; // радиус шара
        const int Vx0 = 2; // горизонтальная скорость шара
        double a = 1; // ускорение
        double K = 0.8; // коэффициент потери
        Random random = new Random();

        public void Start()
        {
            Console.WriteLine("Press enter to start: ");
            while (true)
            {
                if (Console.KeyAvailable) // если нажата клавиша
                {
                    var key = Console.ReadKey().Key; // читаем её
                    if (key == ConsoleKey.Enter)
                    {
                        isRuning = true;
                        Console.Clear();
                    }
                    if (key == ConsoleKey.Escape)
                    {
                        isRuning = false;
                        Environment.Exit(0);
                    }

                }
                Vy = 0; // вертикальная состовляющая скорости изначально равна 0
                Vx = Vx0;
                X = R + 5; // начальное положение шара
                Y = screenHeight / 2;
                Console.Clear(); // очистка экрана
                Console.SetCursorPosition(R+4, screenHeight/2);
                Console.Write("█"); // рисуем подставку на которй лежит шар

                Console.SetCursorPosition(X-R, Y-R); // рисуем шар
                Console.Write("☻");
                Thread.Sleep(500); // пауза, чтобы увидеть  нач. положение шара

                while(isRuning)
                {
                    Console.SetCursorPosition(X - R, Y - R); // стираем шар
                    Console.Write(" ");

                    X = X + (int)Math.Round(Vx); // сдвиг
                    Y = Y + (int)Math.Round(Vy); // сдвиг

                    if(X > screenWidth - R) // если шар вышел за экран
                        isRuning=false;
                    if(Y > screenHeight -R) // если столкнулся с землей
                    {
                        Y = screenHeight - R;
                        Vy = -Vy * K; // отражаем, но с уменьшением скорости в К раз

                        if (Math.Abs(Vy) < 1) // если скорость стала по модулю(2) меньше 1 стоп
                            isRuning = false; 
                    }
                    Vy = Vy + a; // изменяем скорость в соответствии с ускорением

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(X - R, Y - R); // рисуем шар
                    Console.Write("☻");
                    Thread.Sleep(30);
                }
            }

        }
    }
}
