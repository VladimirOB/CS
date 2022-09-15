using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTicket_Polymorphism_
{
    public class Ticket
    {
        int number;
        public static int Numbers_Count = 6;
        public Ticket(int num)
        {
            if(num < 111111 || num > 999999)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            this.number = num;
        }

        public int Number
        {
            get { return number; }
        }

        public void generateNumbers(int num)
        {
            if (num < 111111 || num > 999999)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
                this.number = num;
        }

        public bool isLucky(List<LuckyCriteria> criterias)
        {
            foreach (var criteria in criterias)
            {
                if (criteria.isLucky(this))
                    return true;
            }
            return false;
        }
    }
}
