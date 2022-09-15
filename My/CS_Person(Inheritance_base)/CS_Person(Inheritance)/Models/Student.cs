using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Person_Inheritance_.Models
{
    class Student : Person
    {
        public void Learn()
        {
            Console.WriteLine("Я учусь!");
        }
    }

}
