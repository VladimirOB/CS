using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mozgovoi_M.B_
{
    class Chapter1_DefinitionOfPI
    {
        private const int screenWidth = 60;
        private const int screenHeight = 30;
        bool isRuning = false;
        Random random = new Random();

        int N, N0; // N - кол-во точек. N0 - кол-во точек попавших в круг
        double x, y;
        double sum;
        double current;
        const double R = 10000.5000;

        public void Start()
        {
            Console.WriteLine("Press enter to start: ");
            N = 0; N0 = 0; sum = 0;

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

                if (isRuning)
                {
                    N++; // кол-во эксперементов

                    x = -R + random.NextDouble() * (R + R);// определяем случ. точку
                    y = -R + random.NextDouble() * (R + R);

                    if (Math.Sqrt((x * x) + (y * y)) <= R)
                        N0++; // если точка лежит в круге

                    Console.SetCursorPosition(1, 1);
                    Console.Write($"Number of experiments: {N}");
                    Console.SetCursorPosition(1, 3);
                    current = (double)4 * N0 / N;
                    if (current == 3.1415926535897932)
                    {
                        Console.WriteLine("Yeees!!!");
                        isRuning = false;
                    }
                        
                    Console.Write($"Сurrent experimental value:{current}");
                    Console.SetCursorPosition(1, 5);
                    sum = sum + 4 * N0 / N;
                    Console.Write($"Mean Pi:{sum/N}");
                    if (N == 100000)
                        isRuning = false;

                }
            }
        }
    }
}
