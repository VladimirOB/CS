using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serial_Attributes_
{
    class Student
    {
        [Storable]
        public string Name { get; set; }

        [Storable]
        public double Height { get; set; }

        [Storable]
        public int Age { get; set; }

        [Storable]
        public int Weight { get; set; }

        [Storable]
        public bool MaritalStatus { get; set; }


        public Student()
        {

        }

        public Student(string name, double height, int age, bool maritalStat, int weight)
        {
            Name = name;
            Height = height;
            Age = age;
            MaritalStatus = maritalStat;

           Weight = weight;
        }

        public void Print()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Height: {Height}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Weight: {Weight}");
            Console.WriteLine($"MaritalStatus: {MaritalStatus}");
        }
    }
}
