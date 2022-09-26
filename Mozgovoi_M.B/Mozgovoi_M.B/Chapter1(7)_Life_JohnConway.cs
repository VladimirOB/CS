using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mozgovoi_M.B_
{
    class Chapter1_Life_JohnConway
    {
        private const int screenWidth = 60;
        private const int screenHeight = 30;
        const int WIDTH = 30;
        const int HEIGHT = 15;
        bool isRuning = false;
        Random random = new Random();

        int x, y;
        int Rx, Ry; // ширина и высота клетки
        bool[,] Field = new bool[WIDTH + 1, HEIGHT + 1];
        bool[,] Changes = new bool[WIDTH + 1, HEIGHT + 1];
        int i, j, s;
        int[,] temp = new int[HEIGHT, WIDTH];
        public void Start()
        {
            Rx = (screenWidth / WIDTH) / 2; // определяем размер клетки
            Ry = (screenHeight / HEIGHT) / 2;

            for (i = 0; i < WIDTH + 1; i++)
            {
                for (j = 0; j < HEIGHT + 1; j++)
                {
                    Field[i, j] = false; // очистка поля
                    Changes[i, j] = false; // очистка поля
                }
            }
            string buffer = File.ReadAllText("../../../../dbLifeJC.txt");
            string[] buffer2 = buffer.Split(' ', '\r', '\n');
            buffer2 = buffer2.Where(a => a != "").ToArray();
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int k = 0; k < WIDTH; k++)
                {
                    temp[i, k] = Convert.ToInt32(buffer2[s++]);
                }
            }
            for (i = 1; i < WIDTH; i++) // создаем начальный конфиг
            {
                for (j = 1; j < HEIGHT; j++) 
                {
                    if(temp[j, i] != 0)
                    {
                        Field[i, j] = true;
                        Changes[i, j] = true;
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

                    for (i = 1; i < WIDTH; i++) // создаем начальный конфиг
                    {
                        for (j = 1; j < HEIGHT; j++)
                        {
                            if (Field[i, j])
                            {
                                Console.SetCursorPosition((2 * i - 1) * Rx - Rx, (2 * j - 1) * Ry - Ry);
                                Console.Write("☻");
                            }
                        }
                    }

                    for (x = 1; x < WIDTH; x++)
                    {
                        for (y = 1; y < HEIGHT; y++)
                        {
                            s = 0; // подсчитываем соседей

                            for (i = -1; i <= 1; i++)
                            {
                                for (j = -1; j <= 1; j++)
                                {
                                    //s = s + Ord(Field[x + i, y +j])
                                    //s = s - Ord(Field[x, y])
                                    s = s + Convert.ToInt32(Field[x + i, y + j]); // тернальный оператор

                                    if (Field[x, y])
                                        s++;

                                    if (Field[x, y] == false && s == 3 || (Field[x,y] == true && (s<2) || (s > 3)))
                                    {
                                        Changes[x, y] = true;
                                    }
                                }
                            }
                        }
                    }
                    for (x = 1; x < WIDTH; x++)
                    {
                        for (y = 1; y < HEIGHT; y++)
                        {
                            if (Changes[x,y])
                            {
                                Field[x, y] = !Field[x, y];
                                Changes[x, y] = false;
                            }
                        }
                    }
                    Thread.Sleep(100); // пауза 
                }
            }
        }
    }
}
