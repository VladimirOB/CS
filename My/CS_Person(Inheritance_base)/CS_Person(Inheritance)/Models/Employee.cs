using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Person_Inheritance_.Models
{
    class Employee : Person
    {
        public decimal Salary { get; set; } // Decimal : хранит десятичное дробное число. (16 байт)
    }
}
