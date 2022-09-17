using System;
using System.Diagnostics;
using static System.Console;
namespace SnakeGame
{
    class MainApp
    {
        private const int MapWidth = 30;
        private const int MapHeight = 20;

        private const int FrameMs = 200; // перерыв между кадрами

        //private const int ScreenWidth = MapWidth * 3;
        //private const int ScreenHeight = MapHeight * 3;

        private const ConsoleColor BorderColor = ConsoleColor.Gray;

        private const ConsoleColor Head = ConsoleColor.DarkBlue;
        private const ConsoleColor BodyColor = ConsoleColor.Cyan;

        private const ConsoleColor FoodColor = ConsoleColor.Green;

        private static readonly Random random = new Random();

        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i, 0, BorderColor).Draw();
                new Pixel(i, MapHeight-1, BorderColor).Draw();
            }

            for (int i = 0; i < MapHeight; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(MapWidth-1, i, BorderColor).Draw();
            }
        }

        static Pixel GenFood(Snake snake)
        {
            Pixel food;

            do
            {
                food = new Pixel(random.Next(1, MapWidth - 2), random.Next(1, MapHeight - 2), FoodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
                || snake.Body.Any(b => b.X == food.X && b.Y == food.Y)); // продолжать в том случае если еда вдруг попала на положение головы или тела
            return food;
        }

        static Direction ReadMovement(Direction currentDirection)
        {
            if (!KeyAvailable)
                return currentDirection;
            ConsoleKey key = ReadKey(true).Key;

            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };
            return currentDirection;
        }

        static void Main()
        {
            SetWindowSize(MapWidth, MapHeight);
            SetBufferSize(MapWidth, MapHeight);
            CursorVisible = false;

            while(true)
            {
                StartGame();
                Thread.Sleep(1000);
                ReadKey();
            }
        }

        static void StartGame()
        {

            Clear();

            DrawBorder();

            Direction currenMovement = Direction.Right;

            var snake = new Snake(10, 5, Head, BodyColor);

            Pixel food = GenFood(snake);
            food.Draw();

            int score = 0;

            int lagMs = 0;

            Stopwatch sw = new Stopwatch();
            while (true)
            {
                sw.Restart();

                Direction oldMovement = currenMovement;

                while (sw.ElapsedMilliseconds <= FrameMs - lagMs)
                {
                    if (currenMovement == oldMovement)
                        currenMovement = ReadMovement(currenMovement);
                }

                sw.Restart();

                if(snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(currenMovement, true);

                    food = GenFood(snake); // генерация новой еды
                    food.Draw();
                    score++;
                    Task.Run(() => Beep(1200, 200)); // в отдельном потоке звук
                }
                else
                {
                    snake.Move(currenMovement);
                }

                

                if (snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                    break;
                lagMs = (int)sw.ElapsedMilliseconds;
            }
            snake.Clear();
            SetCursorPosition(MapWidth / 3, MapHeight / 2);
            WriteLine("Game over!");
            SetCursorPosition(MapWidth / 3, (MapHeight / 2)+1);
            WriteLine($"Score:{score}");
            Task.Run(() => Beep(200, 600));
        }
    }

}