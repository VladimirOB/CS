using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Polymorphism_
{
    class RX8 : Mazda
    {
        int Horsepower;
        public RX8(int weight, int speed, int price) : base(weight, speed, price)
        {
            Horsepower = 200;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("RX8");
            Console.WriteLine($"Weight = {Weight}");
            Console.WriteLine($"Speed = {Speed}");
            Console.WriteLine($"Price = {Price}");
            Console.WriteLine($"Horsepower = {Horsepower}\n");
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
