using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Army_Interfaces_Attack_Fly_Walk_
{
    class Army : IAttack, IWalk, IFly
    {
        public double Height => 10000;

        public double this[int pos] => 5;

        public void Attack(int x, int y)
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IAttack Attack coord: {x}, {y}.!");
        }

        public void Defend()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("IAttack Defend!");
        }

        void IAttack.Move()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("IAttack Move!");
        }

        public void Retreat()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("IAttack Retreat!");
        }

        void IAttack.Stop()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("IAttack Stop!");
        }



        public void Stand()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("Walk stand!");
        }

        public void Walk()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("Walk walk!");
        }

        public void Run()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("Walk run!");
        }

        public void GetCoords(int coord)
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IWalk coord:{coord}");
        }

        void IWalk.Stop()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IWalk stop!");
        }

        public void Land()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("IFly Land!");
        }

        public void Move(double x, double y, double z)
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IFly Move! coord: {x},{y},{z}");
        }

        public void TakeOff()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IFly TakeOff!");
        }
    }
}
