using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public readonly struct Pixel
    {
        private const char PixelChar = '█';

        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }


        public Pixel(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public void Draw()
        {
            Console.ForegroundColor = Color;

            Console.SetCursorPosition(X, Y);
            Console.Write(PixelChar);
        }

        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }
    }
}
