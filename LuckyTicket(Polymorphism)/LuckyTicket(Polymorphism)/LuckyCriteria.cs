using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTicket_Polymorphism_
{
    public abstract class LuckyCriteria
    {
        public abstract bool isLucky(Ticket ticket);
    }
}
