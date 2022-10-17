using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mozgovoi_M.B_
{
    class Chapter1_Life
    {
        private const int screenWidth = 60;
        private const int screenHeight = 30;
        const int WIDTH = 30;
        const int HEIGHT = 15;
        bool isRuning = false;
        Random random = new Random();
        
        int x, y;
        int Rx, Ry; // ширина и высота клетки
        bool[,] Field = new bool[WIDTH+1, HEIGHT+1];
        int i, j, s;
        
        public void Start()
        {
            Rx = (screenWidth / WIDTH) / 2; // определяем размер клетки
            Ry = (screenHeight / HEIGHT) / 2;

            for (i = 0; i < WIDTH+1; i++)
            {
                for (j = 0; j < HEIGHT+1; j++)
                {
                    Field[i, j] = false; // очистка поля
                }
            }

            for (i = 1; i < WIDTH; i++) // создаем начальный конфиг
            {
                for (j = 1; j < HEIGHT; j++) // в среднем будет одна инфузория на 4 клетки
                {
                    if(random.Next(4) == 0)
                    {
                        Field[i, j] = true;
                        Console.SetCursorPosition((2 * i - 1) * Rx - Rx, (2 * j - 1) * Ry - Ry);
                        Console.Write("☻");
                    }
                }
            }

            while (true)
            {
                if (Console.KeyAvailable) // если нажата клавиша
                {
                    var key = Console.ReadKey().Key; // читаем её
                    if (key == ConsoleKey.Enter)
                    {
                        isRuning = true;
                    }
                    if (key == ConsoleKey.Escape)
                    {
                        isRuning = false;
                        Environment.Exit(0);
                    }
                }

                if (isRuning)
                {
                    x = random.Next(1, WIDTH); // выбираем случ. клетку
                    y = random.Next(1, HEIGHT);

                    s = 0; // подсчитываем соседей

                    for (i = -1; i <= 1; i++)
                    {
                        for (j = -1; j <= 1; j++)
                        {

                            int temp = Convert.ToInt32(Field[x + i, y + j]);
                            s = (temp > 0) ? s +1 : s +0;
                            temp = Convert.ToInt32(Field[x, y]);
                            s = (temp > 0) ? s - temp : s -temp;

                            //s = s + Convert.ToInt32(Field[x + i, y + j]);

                            //if (Field[x, y])
                            //    s++;

                            if (Field[x, y] == false && s > 2)
                            {
                                Console.SetCursorPosition((2*x - 1) * Rx - Rx, (2*y - 1) * Ry - Ry);
                                Console.Write("☻");
                                Field[x, y] = true;
                            }
                            else if (Field[x,y] == true && ((s<3) || (s>4)))
                            {
                                Console.SetCursorPosition((2 * x - 1) * Rx - Rx, (2 * y - 1) * Ry - Ry);
                                Console.Write(" ");
                                Field[x, y] = false;
                            }
                        }
                    }
                    Thread.Sleep(5); // пауза 5 мс
                }
            }
        }
    }
}
