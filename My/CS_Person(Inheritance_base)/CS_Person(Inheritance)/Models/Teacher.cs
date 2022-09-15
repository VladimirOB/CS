using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Person_Inheritance_.Models
{
    class Teacher : Employee
    {
        public void Teach()
        {
            Console.WriteLine("Я учу!");
        }
    }
}
