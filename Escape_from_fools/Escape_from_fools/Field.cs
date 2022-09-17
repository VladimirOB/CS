using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_from_fools
{
    class Field
    {
        public string[,] field;

        public Field()
        {
            field = new string[10,10];
            for (int i = 0; i < 10; i++)
            {   
                field[0,i] = "0";
            }
            for (int i = 0; i < 10; i++)
            {
                field[i, 0] = "0";
            }
            for (int i = 0; i < 10; i++)
            {
                field[i, 9] = "0";
            }
            for (int i = 0; i < 10; i++)
            {
                field[9, i] = "0";
            }
        }

        public void PrintField()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Console.Write(field[i,k]);
                }
                Console.WriteLine();
            }
        }
    }
}
