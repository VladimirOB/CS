using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTicket_Polymorphism_
{
    class Alternation : LuckyCriteria
    {
        public override bool isLucky(Ticket ticket)
        {
            string str = ticket.Number.ToString();
            if (str[0] == str[2] && str[1] == str[3] && str[2] == str[4] && str[3] == str[5])
                return true;
            else return false;
        }
    }
}
