using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mozgovoi_M.B_
{
    class Molecule
    {
        public int X, Y; // координаты молекулы
        public int Vx, Vy; // состовляющие скорости молекулы
        public bool wasCollision; // указывает на столкновение с броуновской
                                  // частицей во время пред. итерации
    }

    class Chapter1_GasMolecule
    {
        double angle; // угол, задающий начальное направление полета
        const int R = 1; // радиус молекулы
        const int V = 1; // скорость молекулы
        private const int screenWidth = 60;
        private const int screenHeight = 30;
        bool isRuning = false;
        static int N = 10; // кол-во молекул
        Molecule[] mol = new Molecule[N]; // массив молекул
        int CurV; // выбранная случайная скорость молекулы
        const int Rb = 1; // радиус броуновской частицы
        double K = 0.01; // "коэффициет передачи"
        double Xb, Yb; // координаты броуновской частицы
        double Vxb, Vyb; // состовляющие скорости броун. частицы

        public Chapter1_GasMolecule()
        {
            for (int i = 0; i < N; i++)
            {
                mol[i] = new Molecule();
            }
        }

        void DrawBrownian()
        {
            Console.SetCursorPosition((int)Math.Round(Xb - Rb), (int)Math.Round(Yb - Rb));
            Console.Write("☻");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb-1), (int)Math.Round(Yb - Rb));
            Console.Write("☻");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb), (int)Math.Round(Yb - Rb-1));
            Console.Write("☻");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb-1), (int)Math.Round(Yb - Rb - 1));
            Console.Write("☻");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb+1), (int)Math.Round(Yb - Rb));
            Console.Write("☻");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb), (int)Math.Round(Yb - Rb + 1));
            Console.Write("☻");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb + 1), (int)Math.Round(Yb - Rb + 1));
            Console.Write("☻");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb + 1), (int)Math.Round(Yb - Rb - 1));
            Console.Write("☻");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb - 1), (int)Math.Round(Yb - Rb + 1));
            Console.Write("☻");
        }

        void CleanBrownian()
        {
            Console.SetCursorPosition((int)Math.Round(Xb - Rb), (int)Math.Round(Yb - Rb));
            Console.Write(" ");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb-1), (int)Math.Round(Yb - Rb));
            Console.Write(" ");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb), (int)Math.Round(Yb - Rb-1));
            Console.Write(" ");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb-1), (int)Math.Round(Yb - Rb - 1));
            Console.Write(" ");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb+1), (int)Math.Round(Yb - Rb));
            Console.Write(" ");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb), (int)Math.Round(Yb - Rb + 1));
            Console.Write(" ");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb + 1), (int)Math.Round(Yb - Rb + 1));
            Console.Write(" ");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb + 1), (int)Math.Round(Yb - Rb - 1));
            Console.Write(" ");
            Console.SetCursorPosition((int)Math.Round(Xb - Rb - 1), (int)Math.Round(Yb - Rb + 1));
            Console.Write(" ");
        }

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Random random = new Random();
            //Броуновская частица находится в центе
            Xb = screenWidth / 2;
            Yb = screenHeight / 2;
            Vxb = 0;
            Vyb = 0;
            for (int i = 0; i < N; i++) // цикл по всем молекулам
            {
                mol[i].X = random.Next(R, screenWidth - R); // выбор начального положения
                mol[i].Y = random.Next(R, screenHeight - R); // молекулы и её направления
                angle = random.Next(360) * Math.PI / 180;
                CurV = random.Next(1, V);
                //double d = -42.132 + rnd.NextDouble() * (7.003 + 42.132); // При таком подходе мы получим диапазон [-42.132;7.003), потому что NextDouble() генерирует числа в диапазоне [0; 1).
                mol[i].Vx = (int)Math.Round(V * Math.Sin(angle)); // получение состовляющих
                mol[i].Vy = (int)Math.Round(V * Math.Cos(angle)); // скорости молекулы
                mol[i].wasCollision = false; // молекула ни разу не столкнулась с броуновской ч
            }

            Console.WriteLine("Press enter to start: ");
            while (true)
            {
                if (Console.KeyAvailable) // если нажата клавиша
                {
                    var key = Console.ReadKey().Key; // читаем её
                    if(key == ConsoleKey.Enter)
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
                if(isRuning)
                {
                    for (int i = 0; i < N; i++) // основной цикл по всем
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(mol[i].X - R, mol[i].Y - R);
                        Console.Write("☼");

                        if (Math.Sqrt(mol[i].X - Xb) * (mol[i].X - Xb) +
                            (mol[i].Y - Yb) * (mol[i].Y - Yb) < Rb + R) // если произошло столкновение
                        {
                            if (mol[i].wasCollision == false) // если на пред. итерации эта мол не сталкивалась
                            {
                                Vxb = Vxb + K * mol[i].Vx;
                                Vyb = Vyb + K * mol[i].Vy;
                            }
                            mol[i].wasCollision = true;
                        }
                        else
                            mol[i].wasCollision = false;
                    }
                    Console.SetCursorPosition((int)Math.Round(Xb - Rb), (int)Math.Round(Yb - Rb));
                    Console.Write(" ");
                    //CleanBrownian();// стираем частицу
                    Xb = Xb + Vxb; // сдвигаем на новую позицию
                    Yb = Yb + Vyb;

                    if (Xb > screenWidth - Rb) // определяем не вышла ли молекула за границы
                    {
                        Xb = screenWidth - Rb;
                        Vxb = -Vxb;
                    }
                    if (Xb < Rb)
                    {
                        Xb = Rb;
                        Vxb = -Vxb;
                    }

                    if (Yb > screenHeight - Rb) // определяем не вышла ли молекула за границы
                    {
                        Yb = screenHeight - Rb;
                        Vyb = -Vyb;
                    }
                    if (Yb < Rb)
                    {
                        Yb = Rb;
                        Vyb = -Vyb;
                    }
                    Console.SetCursorPosition((int)Math.Round(Xb - Rb), (int)Math.Round(Yb - Rb));
                    Console.Write("☻");
                    //DrawBrownian();
                }
                Thread.Sleep(50);
            }

        }

    }
}
