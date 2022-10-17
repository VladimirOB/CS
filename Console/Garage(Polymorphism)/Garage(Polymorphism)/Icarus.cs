using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Polymorphism_
{
    class Icarus : Bus
    {
        public Icarus(int weight, int speed, int price) : base(weight, speed, price)
        {

        }
        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Icarus");
            Console.WriteLine($"Weight = {Weight}");
            Console.WriteLine($"Speed = {Speed}");
            Console.WriteLine($"Price = {Price}\n");
        }

        public override int GetWeight()
        {
            return Weight;
        }

        public override int GetSpeed()
        {
            return Speed;
        }

        public override int GetPrice()
        {
            return Price;
        }
    }
}
