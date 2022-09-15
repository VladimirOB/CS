using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Polymorphism_
{
    class Garage
    {
        List<Vehicle> vehicles = new List<Vehicle>();
        

        public void Add(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public void Print()
        {
            foreach(var vehicle in vehicles)
            {
                vehicle.Print();
            }
        }

        public int GetPrice()
        {
            int res = 0;
            foreach (var vehicle in vehicles)
            {
                res += vehicle.GetPrice();
            }
            return res;
        }
    }
}
