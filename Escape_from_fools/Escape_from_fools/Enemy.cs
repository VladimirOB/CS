using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_from_fools
{
    abstract class Strategy
    {
        public bool patrol;
        public abstract void Move(ref Pixel Head, char body);
        public abstract void Chase(ref Pixel Head, Direction direction, char body);
    }

    class RandomBehavior : Strategy
    {

        Direction direction;
        Random random = new Random();
        public override void Move(ref Pixel Head, char body)
        {
            Head.Clear();
            direction = (Direction)random.Next(0, 4);
            Head = direction switch
            {
                Direction.Right when Head.X != 57 => new Pixel(Head.X + 1, Head.Y, ConsoleColor.Red, body),
                Direction.Left when Head.X != 2 => new Pixel(Head.X - 1, Head.Y, ConsoleColor.Red, body),
                Direction.Up when Head.Y != 1 => new Pixel(Head.X, Head.Y - 1, ConsoleColor.Red, body),
                Direction.Down when Head.Y != 28 => new Pixel(Head.X, Head.Y + 1, ConsoleColor.Red, body),
                Direction.Stay => new Pixel(Head.X, Head.Y, ConsoleColor.Red, body),
                _ => Head

            };
            Head.Draw();
        }
        public override void Chase(ref Pixel Head, Direction direction, char body)
        {
           
        }
    }

    class ChaseBehavior : Strategy
    {
        bool patrol = true;
        Direction direction;
        Random random = new Random();
        public override void Move(ref Pixel Head, char body)
        {
            if(patrol)
            {
                Head.Clear();
                direction = (Direction)random.Next(0, 4);
                Head = direction switch
                {
                    Direction.Right when Head.X != 57 => new Pixel(Head.X + 1, Head.Y, ConsoleColor.Red, body),
                    Direction.Left when Head.X != 2 => new Pixel(Head.X - 1, Head.Y, ConsoleColor.Red, body),
                    Direction.Up when Head.Y != 1 => new Pixel(Head.X, Head.Y - 1, ConsoleColor.Red, body),
                    Direction.Down when Head.Y != 28 => new Pixel(Head.X, Head.Y + 1, ConsoleColor.Red, body),
                    Direction.Stay => new Pixel(Head.X, Head.Y, ConsoleColor.Red, body),
                    _ => Head

                };
                Head.Draw();
            }
        }

        public override void Chase(ref Pixel Head, Direction direction, char body)
        {
            Head.Clear();
            Head = direction switch
            {
                Direction.Right when Head.X != 57 => new Pixel(Head.X + 1, Head.Y, ConsoleColor.Red, body),
                Direction.Left when Head.X != 2 => new Pixel(Head.X - 1, Head.Y, ConsoleColor.Red, body),
                Direction.Up when Head.Y != 1 => new Pixel(Head.X, Head.Y - 1, ConsoleColor.Red, body),
                Direction.Down when Head.Y != 28 => new Pixel(Head.X, Head.Y + 1, ConsoleColor.Red, body),
                Direction.Stay => new Pixel(Head.X, Head.Y, ConsoleColor.Red, body),
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
        public char Body;
        public Strategy Strategy    
        { 
          get { return strategy; } 
          set { strategy = value; } 
        }

        public Enemy(int initialX, int initialY, ConsoleColor bodyColor, Strategy strategy, char body)
        {
            _headColor = bodyColor;
            Body = body;
            Head = new Pixel(initialX, initialY, bodyColor, Body);
            Draw();
            this.strategy = strategy;
        }


        public void Move()
        {
            strategy.Move(ref Head, Body);
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
