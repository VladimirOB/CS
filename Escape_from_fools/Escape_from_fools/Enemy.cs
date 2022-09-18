using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_from_fools
{
    abstract class Strategy
    {
        public abstract void Behavior();
    }

    class RandomBehavior : Strategy
    {
        public override void Behavior()
        {
            
        }
    }


    class Enemy
    {
        Strategy strategy;

        private readonly ConsoleColor _headColor;

        public Pixel Head { get; private set; }

        public Strategy Strategy    
        { 
          get { return strategy; } 
          set { strategy = value; } 
        }

        public Enemy(int initialX, int initialY, ConsoleColor bodyColor, Strategy strategy)
        {
            _headColor = bodyColor;
            Head = new Pixel(initialX, initialY, bodyColor, '♣');
            Draw();
            this.strategy = strategy;
        }

        public void Move(Direction direction)
        {
            Clear();
            Head = direction switch
            {
                Direction.Right => new Pixel(Head.X + 1, Head.Y, _headColor, '♣'),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, _headColor, '♣'),
                Direction.Up => new Pixel(Head.X, Head.Y - 1, _headColor, '♣'),
                Direction.Down => new Pixel(Head.X, Head.Y + 1, _headColor, '♣'),
                Direction.Stay => new Pixel(Head.X, Head.Y, _headColor, '♣'),
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
