using System;
using System.Diagnostics;
using System.Xml.Linq;
using static System.Console;
using static System.Formats.Asn1.AsnWriter;

namespace Escape_from_fools
{
    class MainApp
    {
        /*3. (+1 балл) Реализовать консольную игру "Побег от дурачков", которая является консольной копией легендарной игры
        "Догони меня кирпич". Цель игры - продержаться как можно дольше на поле так, чтобы "враги" не догнали.
        Требования к игре:
        - игра в консоли
        - движение спрайтов по таймеру
        - поле генерируется случайно
        - управление главным героем с клавиатуры
        - минимум 3 вида врагов
        - наличие меню
        - использование паттерна "Стратегия" для алгоритма выбора следующего хода для врагов
        примеры алгоритмов: случайный шаг, волновой алгоритм - кратчайшее расстояние до героя, ходит маршруту,
        идет по волновому алгоритму в случае маленького расстояния.*/

        private const int MapWidth = 60;
        private const int MapHeight = 30;
        static System.Timers.Timer timer;
        public static int time = 0;
        public static int score = 0;
        private const ConsoleColor BorderColor = ConsoleColor.Gray;
        private const ConsoleColor HeroColor = ConsoleColor.Green;
        private const ConsoleColor EnemyColor = ConsoleColor.Red;
        private const int FrameMs = 200; // перерыв между кадрами
        private static readonly Random rand = new Random();
        public static Enemy[] Enemies;
        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i, 0, BorderColor, '█').Draw();
                new Pixel(i, MapHeight - 1, BorderColor, '█').Draw();
            }

            for (int i = 0; i < MapHeight-1; i++)
            {
                new Pixel(0, i, BorderColor, '█').Draw();
                new Pixel(1, i, BorderColor, '█').Draw();
                new Pixel(MapWidth - 1, i, BorderColor, '█').Draw(); 
                new Pixel(MapWidth - 2, i, BorderColor, '█').Draw();
            }

        }

        static Direction ReadMovement(Hero hero, Pixel[] wall)
        {
            Direction currentDirection = Direction.Stay;
            ConsoleKey key = ReadKey(true).Key;

            switch(key)
            {
                case ConsoleKey.UpArrow:
                    {
                        if (wall.Any(b => b.Y == hero.Head.Y - 1 && b.X == hero.Head.X) || hero.Head.Y == 1)
                            currentDirection = Direction.Stay;
                        else
                            currentDirection = Direction.Up;
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if (wall.Any(b => b.Y == hero.Head.Y + 1 && b.X == hero.Head.X) || hero.Head.Y == MapHeight-2)
                            currentDirection = Direction.Stay;
                        else
                            currentDirection = Direction.Down;
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        if (wall.Any(b => b.Y == hero.Head.Y && b.X == hero.Head.X-1) || hero.Head.X == 2)
                            currentDirection = Direction.Stay;
                        else
                            currentDirection = Direction.Left;
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        if (wall.Any(b => b.Y == hero.Head.Y && b.X == hero.Head.X + 1) || hero.Head.X == MapWidth - 3)
                            currentDirection = Direction.Stay;
                        else
                            currentDirection = Direction.Right;
                        break;
                    }
            }

            //currentDirection = key switch
            //{
            //    ConsoleKey.UpArrow when wall.Any(b => b.Y != hero.Head.Y-1 && b.X != hero.Head.X) => Direction.Up,
            //    ConsoleKey.DownArrow when wall.Any(b => b.Y-1 != hero.Head.Y+1 && b.X != hero.Head.X) => Direction.Down,
            //    ConsoleKey.LeftArrow when wall.Any(b => b.Y != hero.Head.Y && b.X != hero.Head.X) => Direction.Left,
            //    ConsoleKey.RightArrow when wall.Any(b => b.Y != hero.Head.Y && b.X != hero.Head.X) => Direction.Right,
            //    _ => currentDirection
            //};
            return currentDirection;
        }

        static Pixel GenWall(Hero hero)
        {
            Pixel wall;
            do
            {
                wall = new Pixel(rand.Next(2, MapWidth - 2), rand.Next(2, MapHeight - 2), BorderColor, '█');
            }
            while (hero.Head.X == wall.X && hero.Head.Y == wall.Y); // продолжать в том случае если еда вдруг попала на положение головы или тела

            return wall;
        }

        static void EnemyMove()
        {
            foreach (var Enemy in Enemies)
            {
                Enemy.Move();
            }
        }

        static Pixel GenFood(Hero hero)
        {
            Pixel food;
            do
            {
                food = new Pixel(rand.Next(2, MapWidth - 3), rand.Next(2, MapHeight - 3), ConsoleColor.Cyan, '☼');
            } while (hero.Head.X == food.X && hero.Head.Y == food.Y); // продолжать в том случае если еда вдруг попала на положение героя
            return food;
        }

        static void StartGame(Enemy[] Enemies)
        {
            Clear();
            DrawBorder();
            Pixel[] wall = new Pixel[70];
            var Hero = new Hero(rand.Next(2, MapWidth-2), rand.Next(2, MapHeight-2), HeroColor);

            for (int i = 0; i < 70; i++)
            {
                wall[i] = GenWall(Hero);
                wall[i].Draw();
            }

            Pixel food = GenFood(Hero);
            food.Draw();
            Stopwatch sw = new Stopwatch();
            while (true)
            {
                sw.Restart();
                if (KeyAvailable)
                {
                    while (sw.ElapsedMilliseconds <= FrameMs)
                    {
                        Direction currentMovement = ReadMovement(Hero, wall);
                        if (Enemies.Any(a => a.Head.X == Hero.Head.X && a.Head.Y == Hero.Head.Y))
                            break;
                        if (Hero.Head.X == food.X && Hero.Head.Y == food.Y)
                        {
                            Hero.Move(currentMovement);
                            food = GenFood(Hero); // генерация новой еды
                            food.Draw();
                            score++;
                            Task.Run(() => Beep(1200, 200)); // в отдельном потоке звук
                        }
                        else
                        {
                            Hero.Move(currentMovement);
                        }
                    }
                }
                sw.Restart();
                if (Enemies.Any(a => a.Head.X == Hero.Head.X && a.Head.Y == Hero.Head.Y))
                    break;
            }
            Hero.Clear();
            SetCursorPosition(MapWidth / 2 - 4, MapHeight / 2);
            WriteLine("Game over!");
        }

        
        static void Main()
        {
            SetWindowSize(MapWidth, MapHeight);
            SetBufferSize(MapWidth, MapHeight);
            CursorVisible = false;
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;

            Enemies = new Enemy[10];
            char[] EnemyBody = { '♣', '♠', '♥', '♦' };
            for (int i = 0; i < 10; i++)
            {
                Enemies[i] = new Enemy(rand.Next(2, MapWidth - 2), rand.Next(2, MapHeight - 2), EnemyColor, new RandomBehavior(), EnemyBody[rand.Next(0,3)]);
            }

            //Enemies[9] = new Enemy(rand.Next(2, MapWidth - 2), rand.Next(2, MapHeight - 2), EnemyColor, new ChaseBehavior(), 'X');

            while (true)
            {
                timer.Start();
                StartGame(Enemies);
                timer.Stop();
                SetCursorPosition(MapWidth /2-4, MapHeight / 2+1);
                WriteLine($"Time: {time}");
                SetCursorPosition(MapWidth / 2 - 4, MapHeight / 2 + 2);
                WriteLine($"Score: {score}");
                time = 0;
                score = 0;
                Thread.Sleep(1000);
                ReadKey();
            }
        }

        static void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            SetCursorPosition(MapWidth/2-4, 0);
            WriteLine($"Time: {time}");
            time++;
            EnemyMove();
        }
    }
}