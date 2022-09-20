using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Army_Interfaces_Attack_Fly_Walk_
{
    class Butterfly : IFly
    {
        public double this[int pos] => 777;

        public double Height => 50;

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
