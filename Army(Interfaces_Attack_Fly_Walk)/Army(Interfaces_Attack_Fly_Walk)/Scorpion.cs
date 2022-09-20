using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Army_Interfaces_Attack_Fly_Walk_
{
    class Scorpion : IWalk
    {
        public void GetCoords(int coord)
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IWalk coord:{coord}");
        }

        public void Run()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IWalk Run!");
        }

        public void Stand()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IWalk Stand!");
        }

        public void Stop()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IWalk Stop!");
        }

        public void Walk()
        {
            Console.Write($"{GetType().Name} : ");
            Console.WriteLine($"IWalk walk!");
        }
    }
}
