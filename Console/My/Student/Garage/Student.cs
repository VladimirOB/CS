using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage123
{
    enum Col
    {
        Blue,
        Red,
        Green
    }
    class Student
    {
        public Guid id; // Guid структура для создания уникальных id.
        public string? firstName;
        public string? lastName;
        public string? middleName;
        public int age;
        public string? group;
        public Col color;

        public void Print()
        {
            Console.WriteLine("Information about student: ");
            Console.WriteLine($"Id: {id}");
            Console.WriteLine($"Last: {lastName}");
            Console.WriteLine($"Name: {firstName}");
            Console.WriteLine($"Mid: {middleName}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Group: {group}");
        }

        public string GetFullName()
        {
            return $"{firstName} {lastName} {middleName}";
        }
    }
}
