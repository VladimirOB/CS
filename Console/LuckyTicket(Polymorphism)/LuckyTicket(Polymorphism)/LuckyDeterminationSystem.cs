using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTicket_Polymorphism_
{
    public class LuckyDeterminationSystem
    {
        public List<LuckyCriteria> criteria = new List<LuckyCriteria>();
        HashSet<Ticket> luckyTickets = new HashSet<Ticket>();
        public LuckyDeterminationSystem()
        {
            
        }

        public void Add(LuckyCriteria lc)
        {
            criteria.Add(lc);
        }

        public void AddLucky(Ticket ticket)
        {
            luckyTickets.Add(ticket);
        }

        public bool isLucky(Ticket ticket)
        {
            foreach (var criterium in criteria)
            {
                if (criterium.isLucky(ticket))
                    return true;
            }
            return false;
        }

        public void SaveLuckyTicket(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine($"Count of luckyTickets ticket = {luckyTickets.Count}");
            foreach (var ticket in luckyTickets)
            {
                sw.WriteLine(ticket.Number.ToString());
            }
            sw.Close();
            Console.WriteLine($"Lucky ticket's: {luckyTickets.Count} saved!");
        }
    }
}
