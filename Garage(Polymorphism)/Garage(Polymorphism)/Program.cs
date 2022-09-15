namespace Garage_Polymorphism_
{
    class Program
    {
        /*1. Имеется иерархия классов
        Vehicle (транспортное средство)
        |	\	\
        Car	Bus	Truck

        От класса Car наследуют конкретные автомобили
        От класса Bus наследуют конкретные автобусы
        От класса Truck наследуют конкретные грузовики

        Виртуальные или абстрактные методы класса Vehicle:
        - Print()
        - GetWeight() - возвратить вес транспортного средства
        - GetSpeed() - возвратить скорость
        - GetPrice() - возвратить текущую стоимость

        Класс Garage:
        - List<Vehicle> vehicles;
        - Add(Vehicle vehicle) - добавление в гараж
        - Print() - печать содержимого гаража
        - GetPrice() - посчитать общую стоимость содержимого гаража*/

        static void Main(string[] args)
        {
            Garage garage = new Garage();
            garage.Add(new RX8(2000, 250, 10000));
            garage.Add(new Icarus(4000, 120, 50000));
            garage.Add(new KAMAZ(5500, 160, 40000));
            garage.Print();
            Console.WriteLine($"Vehicle Count = {Vehicle.VehicleCount}"); 
            int sum = garage.GetPrice();
            Console.WriteLine($"Sum = {sum}");
        }
    }
}