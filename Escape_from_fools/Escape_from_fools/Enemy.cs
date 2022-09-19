using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_from_fools
{
    abstract class Strategy
    {
        public abstract void Move(ref Pixel Head);
    }

    class RandomBehavior : Strategy
    {

        Direction direction;
        Random random = new Random();
        public override void Move(ref Pixel Head)
        {
            Head.Clear();
            direction = (Direction)random.Next(0, 4);
            Head = direction switch
            {
                Direction.Right when Head.X != 57 => new Pixel(Head.X + 1, Head.Y, ConsoleColor.Red, '♣'),
                Direction.Left when Head.X != 2 => new Pixel(Head.X - 1, Head.Y, ConsoleColor.Red, '♣'),
                Direction.Up when Head.Y != 1 => new Pixel(Head.X, Head.Y - 1, ConsoleColor.Red, '♣'),
                Direction.Down when Head.Y != 28 => new Pixel(Head.X, Head.Y + 1, ConsoleColor.Red, '♣'),
                Direction.Stay => new Pixel(Head.X, Head.Y, ConsoleColor.Red, '♣'),
                _ => Head

            };
            Head.Draw();
        }
    }

    class ChaseBehavior : Strategy
    {

        Direction direction;
        Random random = new Random();
        public override void Move(ref Pixel Head)
        {
            Head.Clear();
            direction = (Direction)random.Next(0, 4);
            Head = direction switch
            {
                Direction.Right when Head.X != 57 => new Pixel(Head.X + 1, Head.Y, ConsoleColor.Red, '♣'),
                Direction.Left when Head.X != 2 => new Pixel(Head.X - 1, Head.Y, ConsoleColor.Red, '♣'),
                Direction.Up when Head.Y != 1 => new Pixel(Head.X, Head.Y - 1, ConsoleColor.Red, '♣'),
                Direction.Down when Head.Y != 28 => new Pixel(Head.X, Head.Y + 1, ConsoleColor.Red, '♣'),
                Direction.Stay => new Pixel(Head.X, Head.Y, ConsoleColor.Red, '♣'),
                _ => Head

            };
            Head.Draw();
        }
    }


    class Enemy
    {
        Strategy strategy;

        private readonly ConsoleColor _headColor;

        public Pixel Head;

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


        public void Move()
        {
            strategy.Move(ref Head);
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
