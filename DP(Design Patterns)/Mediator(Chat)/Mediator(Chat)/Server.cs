using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_Chat_
{
    delegate void Subs(string message, Member source);

    // "Mediator" 
    abstract class Mediator
    {
        //protected Dictionary<string, Member> dict = new Dictionary<string, Member>();
        protected Dictionary<string, Subs> dict = new Dictionary<string, Subs>();
        public void Add(Member m)
        {
            dict.Add(m.GetType().Name, m.Notify);
        }

        public abstract void Send(string message, Member source, string toWhom);

    }


    // "ConcreteMediator"
    class Server : Mediator
    {
        public override void Send(string message, Member source, string toWhom)
        {
            foreach (var item in dict)
            {
                if (item.Key.Equals(toWhom))
                {
                    Log(source, toWhom, message);
                    item.Value.Invoke(message, source);
                }
            }
        }

        void Log(Member source, string dest, string message)
        {
            string time = "(" + DateTime.Now.ToLongTimeString() + ")";
            File.AppendAllText("../../../../chat.log", source.GetType().Name + " received a message from " + dest + time + "\n{" + message + " }\n\n");
        }
    }
}
