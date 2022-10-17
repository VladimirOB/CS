using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Garage
{
    internal class Cars
    {
        private string? brand;
        private string? model;
        private string? address;
        private string? owner;
        private string? maxSpeed;
        private string? price;
        private string? id;
        public void EnterCar()
        {
            Console.WriteLine("Enter brand: ");
            brand = Console.ReadLine();
            Console.WriteLine("Enter model: ");
            model = Console.ReadLine();
            Console.WriteLine("Enter address: ");
            address = Console.ReadLine();
            Console.WriteLine("Enter owner: ");
            owner = Console.ReadLine();
            Console.WriteLine("Enter max speed: ");
            maxSpeed = Console.ReadLine();
            Console.WriteLine("Enter price: ");
            price = Console.ReadLine();
            Console.WriteLine("Enter id: ");
            id = Console.ReadLine();
        }

        public void PrintCarList()
        {
            Console.WriteLine($"Brand: {brand}");
            Console.WriteLine($"Model: {model}");
            Console.WriteLine($"Address: {address}");
            Console.WriteLine($"Owner: {owner}");
            Console.WriteLine($"Max speed: {maxSpeed}");
            Console.WriteLine($"Price: {price}");
            Console.WriteLine($"ID: {id}");
            Console.WriteLine("");
        }
        public void Save()
        {
            StreamWriter SW = new StreamWriter(new FileStream("DB.txt", FileMode.Append, FileAccess.Write));
            SW.Write($"{brand} ");
            SW.Write($"{model} ");
            SW.Write($"{address} ");
            SW.Write($"{owner} ");
            SW.Write($"{maxSpeed} ");
            SW.Write($"{price} ");
            SW.WriteLine($"{id}");
            SW.Close();
        }
        
        public int LoadSize()
        {
            string[] lines = File.ReadAllLines("DB.txt");
            return lines.Length;
        }

        public void Load(int currentSize)
        {
            string[] lines = File.ReadAllLines("DB.txt");
            string[] columns = lines[currentSize].Split(' ');
            if(columns.Length == 7)
            {
                brand = columns[0];
                model = columns[1];
                address = columns[2];
                owner = columns[3];
                maxSpeed = columns[4];
                price = columns[5];
                id = columns[6];
            }
        }
        public void Search()
        {
            string? id2 = Console.ReadLine();
            if (id2.Equals(id))
            {
                Console.WriteLine($"Brand: {brand}");
                Console.WriteLine($"Model: {model}");
                Console.WriteLine($"Address: {address}");
                Console.WriteLine($"Owner: {owner}");
                Console.WriteLine($"Max speed: {maxSpeed}");
                Console.WriteLine($"Price: {price}");
                Console.WriteLine($"ID: {id}");
            }
            else
                return;
        }
        public int Delete()
        {
            Console.WriteLine("Enter ID: ");
            string? id2 = Console.ReadLine();
            if (id2.Equals(id))
            {
                return 1;
            }
            else
                return 0;
        }
    }
}
