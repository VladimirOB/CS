using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Polymorphism_
{
    abstract class Vehicle
    {
        // - Print()
        //- GetWeight() - возвратить вес транспортного средства
        //- GetSpeed() - возвратить скорость
        //- GetPrice() - возвратить текущую стоимость

        protected int Weight;
        protected int Speed;
        protected int Price;
        static int Count;

        public static int VehicleCount
        {
            get { return Count; }
        }

        static Vehicle()
        {
            Count = 0;
        }

        public Vehicle(int weight, int speed, int price)
        {
            Weight = weight;
            Speed = speed;
            Price = price;
            Count++;
        }
        ~Vehicle()
        {
            Count--;
        }

        public abstract void Print();

        public abstract int GetWeight();

        public abstract int GetSpeed();

        public abstract int GetPrice();
    }
}
