using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_Chat_
{
    // "Mediator" 
    abstract class Mediator
    {
        public abstract void Send(string message, Member m);

        protected List<Member> lst = new List<Member>();

        public void Add(Member m)
        {
            lst.Add(m);
        }
    }
    // "ConcreteMediator"
    class Server : Mediator
    {
        public override void Send(string message, Member m)
        {
            m.Notify(message);
        }
    }
}
