using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor_Archiver_
{
    class Conveyor
    {
        private List<SubSystem> elements = new List<SubSystem>();

        public void Attach(SubSystem element)
        {
            elements.Add(element);
        }

        public void Detach(SubSystem element)
        {
            elements.Remove(element);
        }

        public void Accept(Visitor visitor)
        {
            foreach (SubSystem e in elements)
            {
                e.Accept(visitor);
            }
        }
    }
}
