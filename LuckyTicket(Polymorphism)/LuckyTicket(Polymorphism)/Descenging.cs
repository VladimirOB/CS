using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTicket_Polymorphism_
{
    class Descending : LuckyCriteria
    {
        public override bool isLucky(Ticket ticket)
        {
            string str = ticket.Number.ToString();
            for (int i = 0; i < 5; i++)
            {
                if (str[i] != str[i + 1] + 1) return false;
            }
            return true;
        }
    }
}
