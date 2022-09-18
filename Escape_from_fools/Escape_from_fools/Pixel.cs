using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_from_fools
{
    public struct Pixel
    {
        private char PixelChar = '█';

        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }


        public Pixel(int x, int y, ConsoleColor color, char pixelChar)
        {
            X = x;
            Y = y;
            Color = color;
            PixelChar = pixelChar;
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
