using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mozgovoi_M.B_
{
    class Molecule2
    {
        public int X, Y; // координаты молекулы
        public int Vx, Vy; // состовляющие скорости молекулы
    }

    class Chapter1_Equilibrium
    {
        double angle; // угол, задающий начальное направление полета
        const int R = 1; // радиус молекулы
        const int V = 2; // скорость молекулы
        private const int screenWidth = 60;
        private const int screenHeight = 30;
        bool isRuning = false;
        static int N = 2; // кол-во молекул
        Molecule2[] mol = new Molecule2[N*2]; // массив молекул
        int RHole = 10; // радиус отверстия для прохода молекул посередине
        int T1; // температуа в левой части
        int T2; // температура в правой части

        public Chapter1_Equilibrium()
        {
            for (int i = 0; i < N*2; i++)
            {
                mol[i] = new Molecule2();
            }
        }


        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Random random = new Random();
            for (int i = 0; i < N*2; i++) // цикл по всем молекулам
            {
                if(i < N)
                {
                    mol[i].X = random.Next(R, screenWidth/2 - R); // выбор начального положения
                    mol[i].Y = random.Next(R, screenHeight - R); // молекулы и её направления
                    angle = random.Next(360) * Math.PI / 180;
                    mol[i].Vx = (int)Math.Round(V/2 * Math.Sin(angle)); // получение состовляющих
                    mol[i].Vy = (int)Math.Round(V/2 * Math.Cos(angle)); // скорости молекулы
                }
                else
                {
                    mol[i].X = random.Next(screenWidth / 2, screenWidth - R); // выбор начального положения
                    mol[i].Y = random.Next(R, screenHeight - R); // молекулы и её направления
                    angle = random.Next(360) * Math.PI / 180;
                    mol[i].Vx = (int)Math.Round(V * Math.Sin(angle)); // получение состовляющих
                    mol[i].Vy = (int)Math.Round(V * Math.Cos(angle)); // скорости молекулы
                }

            }

            Console.WriteLine("Press enter to start: ");
            T1 = N * (V / 2);
            T2 = N * V;
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
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(1, screenHeight - 1);
                    Console.Write($"TempLeft: {T1}");
                    Console.SetCursorPosition(screenWidth / 2, screenHeight - 1);
                    Console.Write($"TempRight:{T2}");
                    for (int i = 0; i < N*2; i++) // основной цикл по всем
                    {
                        if(i < N)
                        {
                            Console.SetCursorPosition(mol[i].X - R, mol[i].Y - R);
                            Console.Write(" "); // стираем молекулу
                            mol[i].X = mol[i].X + mol[i].Vx; // сдвигаем на новую позицию
                            mol[i].Y = mol[i].Y + mol[i].Vy;

                            if (mol[i].X > screenWidth - R) // определяем не вышла ли молекула за границы
                            {
                                mol[i].X = screenWidth - R;
                                mol[i].Vx = -mol[i].Vx;
                            }
                            if (mol[i].X < R)
                            {
                                mol[i].X = R;
                                mol[i].Vx = -mol[i].Vx;
                            }

                            if (mol[i].Y > screenHeight - R) // определяем не вышла ли молекула за границы
                            {
                                mol[i].Y = screenHeight - R;
                                mol[i].Vy = -mol[i].Vy;
                            }
                            if (mol[i].Y < R)
                            {
                                mol[i].Y = R;
                                mol[i].Vy = -mol[i].Vy;
                            }
                            if(mol[i].X > screenWidth/2 - R && mol[i].X - mol[i].Vx <= screenWidth/2 - R)
                            {
                                if (mol[i].Y >= screenHeight /2 -RHole && mol[i].Y <= screenHeight/2 +RHole)
                                {
                                    T1 -= 1; // изменяем температуру
                                    T2 += 1;
                                }
                                else
                                {
                                    mol[i].X = screenWidth / 2 - R; // отражаем от перегородки
                                    mol[i].Vx = -mol[i].Vx;
                                }
                            }
                            else if (mol[i].X < screenWidth / 2 + R && mol[i].X - mol[i].Vx >= screenWidth / 2 + R)
                            {
                                if (mol[i].Y >= screenHeight / 2 - RHole && mol[i].Y <= screenHeight / 2 + RHole)
                                {
                                    T1 += 1; // изменяем температуру
                                    T2 -= 1;
                                }
                                else
                                {
                                    mol[i].X = screenWidth / 2 + R; // отражаем от перегородки
                                    mol[i].Vx = -mol[i].Vx;
                                }
                            }

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.SetCursorPosition(mol[i].X - R, mol[i].Y - R);
                            Console.Write("☼");
                        }
                        else
                        {
                            Console.SetCursorPosition(mol[i].X - R, mol[i].Y - R);
                            Console.Write(" "); // стираем молекулу
                            mol[i].X = mol[i].X + mol[i].Vx; // сдвигаем на новую позицию
                            mol[i].Y = mol[i].Y + mol[i].Vy;

                            if (mol[i].X > screenWidth - R) // определяем не вышла ли молекула за границы
                            {
                                mol[i].X = screenWidth - R;
                                mol[i].Vx = -mol[i].Vx;
                            }
                            if (mol[i].X < R)
                            {
                                mol[i].X = R;
                                mol[i].Vx = -mol[i].Vx;
                            }

                            if (mol[i].Y > screenHeight - R) // определяем не вышла ли молекула за границы
                            {
                                mol[i].Y = screenHeight - R;
                                mol[i].Vy = -mol[i].Vy;
                            }
                            if (mol[i].Y < R)
                            {
                                mol[i].Y = R;
                                mol[i].Vy = -mol[i].Vy;
                            }


                            if (mol[i].X > screenWidth / 2 - R && mol[i].X - mol[i].Vx <= screenWidth / 2 - R)
                            {
                                if (mol[i].Y >= screenHeight / 2 - RHole && mol[i].Y <= screenHeight / 2 + RHole)
                                {
                                    T1 -= V; // изменяем температуру
                                    T2 += V;
                                }
                                else
                                {
                                    mol[i].X = screenWidth / 2 - R; // отражаем от перегородки
                                    mol[i].Vx = -mol[i].Vx;
                                }
                            }
                            else if (mol[i].X < screenWidth / 2 + R && mol[i].X - mol[i].Vx >= screenWidth / 2 + R)
                            {
                                if (mol[i].Y >= screenHeight / 2 - RHole && mol[i].Y <= screenHeight / 2 + RHole)
                                {
                                    T1 += V;
                                    T2 -= V;
                                }
                                else
                                {
                                    mol[i].X = screenWidth / 2 + R; // отражаем от перегородки
                                    mol[i].Vx = -mol[i].Vx;
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(mol[i].X - R, mol[i].Y - R);
                            Console.Write("☼");
                        }
                    }
                }
                Thread.Sleep(50);
            }

        }

    }
}
