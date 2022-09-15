using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTicket_Polymorphism_
{
    class LuckySums : LuckyCriteria
    {
        public override bool isLucky(Ticket ticket)
        {
            string str = ticket.Number.ToString();
            int sum1 = 0, sum2 = 0;
            for (int i = 0; i < 3; i++)
            {
                sum1 += str[i]-48;
            }

            for (int i = 3; i < 6; i++)
            {
                sum2 += str[i]-48;
            }
            if (sum1 == sum2)
                return true;
            else
                return false;
        }
    }
}
