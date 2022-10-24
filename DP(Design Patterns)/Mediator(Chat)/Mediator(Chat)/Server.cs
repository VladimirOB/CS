using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_Chat_
{
    delegate void Subs(string message, Member m);
    // "Mediator" 
    abstract class Mediator
    { 
        //public event Subs subscribers;
        protected Dictionary<string, Member> dict = new Dictionary<string, Member>();

        public void Add(Member m)
        {
            dict.Add(m.GetType().Name, m);
        }

        public abstract void Send(string message, Member source, Member dest);

    }


    // "ConcreteMediator"
    class Server : Mediator
    {
       
        void Add(Member ev)
        {
            dict
        }


        public override void Send(string message, Member source, Member dest)
        {
            Log(source, dest, message);
            source.Notify(message, dest);
        }

        void Log(Member source, Member dest, string message)
        {
            string time = "(" + DateTime.Now.ToLongTimeString() + ")";
            File.AppendAllText("../../../../chat.log", source.GetType().Name + " received a message from " + dest.GetType().Name +time+ "\n{" + message + " }\n\n");
        }
    }
}
