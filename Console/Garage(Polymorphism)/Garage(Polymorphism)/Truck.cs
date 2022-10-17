using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Polymorphism_
{
    abstract class Truck : Vehicle
    {
        public Truck(int weight, int speed, int price) : base(weight, speed, price) { }

        public override void Print()
        {
            Console.WriteLine("Truck");
        }
    }
}
