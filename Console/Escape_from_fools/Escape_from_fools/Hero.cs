using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_from_fools
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Stay,
    }

    public class Hero
    {
        private readonly ConsoleColor _headColor;

        public Pixel Head { get; private set; }

        public Hero(int initialX, int initialY, ConsoleColor bodyColor)
        {
            _headColor = bodyColor;
            Head = new Pixel(initialX, initialY, bodyColor, '☺');
            Draw();
        }

        public void Move(Direction direction)
        {
            Clear();
            Head = direction switch
            {
                Direction.Right => new Pixel(Head.X + 1, Head.Y, _headColor, '☻'),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, _headColor, '☻'),
                Direction.Up => new Pixel(Head.X, Head.Y - 1, _headColor, '☻'),
                Direction.Down => new Pixel(Head.X, Head.Y + 1, _headColor, '☻'),
                Direction.Stay => new Pixel(Head.X, Head.Y, _headColor, '☻'),
                _ => Head

            };
            Draw();

        }

        public void Draw()
        {
            Head.Draw();
        }

        public void Clear()
        {
            Head.Clear();
        }

    }
}
