using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Army_Interfaces_Attack_Fly_Walk_
{
    class Human : IWalk, IFly
    {
        double IFly.this[int pos] => 7;

        double IFly.Height => 10000;

        public void GetCoords(int coord)
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IWalk coord:{coord}");
        }

        public void Run()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("Walk run!");
        }

        public void Stand()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("Walk stand!");
        }

        public void Stop()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("Walk Stop!");
        }

        public void Walk()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("Walk walk!");
        }

        void IFly.Land()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine("IFly Land!");
        }

        void IFly.Move(double x, double y, double z)
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IFly Move! coord: {x},{y},{z}");
        }

        void IFly.TakeOff()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IFly TakeOff!");
        }
    }
}
