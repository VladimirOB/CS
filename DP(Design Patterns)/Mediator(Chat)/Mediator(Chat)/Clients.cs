using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_Chat_
{
    // "Colleague"
    abstract class Member
    {
        protected Mediator mediator;

        public Member(Mediator mediator)
        {
            this.mediator = mediator;
            mediator.Add(this);
        }

        public abstract void Notify(string message);
    }

    //"ConcreteColleague1" - Пехота
    class Infantry : Member
    {
        // Конструктор от бати
        public Infantry(Mediator mediator) : base(mediator) { }

        //вызываем метод Send у Mediator(Server)
        public void Send(string message, Member m)
        {
            mediator.Send(message, m);
        }
        
        public override void Notify(string message)
        {
            Console.WriteLine("Colleague1 gets message: " + message);
        }
    }
    
    //"ConcreteColleague2"
    class HeavyArtillery : Member
    {
        // Конструктор от бати
        public HeavyArtillery(Mediator mediator) : base(mediator) { }

        //вызываем метод Send у Mediator(Server)
        public void Send(string message, Member m)
        {
            mediator.Send(message, m);
        }

        public override void Notify(string message)
        {
            Console.WriteLine("Colleague2 gets message: " + message);
        }
    }
    //"ConcreteColleague3"
    class Aviation : Member
    {
        // Конструктор от бати
        public Aviation(Mediator mediator) : base(mediator) { }

        //вызываем метод Send у Mediator(Server)
        public void Send(string message, Member m)
        {
            mediator.Send(message, m);
        }

        public override void Notify(string message)
        {
            Console.WriteLine("Colleague3 gets message: " + message);
        }
    }
}
