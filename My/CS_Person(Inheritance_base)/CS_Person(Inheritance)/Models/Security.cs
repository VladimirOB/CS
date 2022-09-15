using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Person_Inheritance_.Models
{
    class Security : Employee
    {
        public void Guard()
        {
            Console.WriteLine("Я охраняю");
        }
    }
}
