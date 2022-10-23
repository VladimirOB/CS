﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_Chat_
{
    delegate void Subs(string message);

    // "Colleague"
    abstract class Member
    {
        protected event Subs? subscribers;
        protected Mediator mediator;

        public Member(Mediator mediator)
        {
            this.mediator = mediator;
            //mediator.Add(this);
        }

        //public abstract void AddSubs(Subs ev);
        public abstract void Notify(string message, Member member);
    }

    //"ConcreteColleague1" - Пехота
    class Infantry : Member
    {
        // Конструктор от бати
        public Infantry(Mediator mediator) : base(mediator) { }

        //вызываем метод Send у Mediator(Server)
        public void Send(string message, Member m) 
        {
            mediator.Send(message, m, this);
        }
        public override void Notify(string message, Member member)
        {
            Console.WriteLine($"Infantry gets message from {member.GetType().Name}: {message} ({DateTime.Now.ToLongTimeString()})");
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
            mediator.Send(message, m, this);
        }

        public override void Notify(string message, Member member)
        {
            Console.WriteLine($"HeavyArtillery gets message from {member.GetType().Name}: {message} ({DateTime.Now.ToLongTimeString()})");
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
            mediator.Send(message, m, this);
        }
        public override void Notify(string message, Member member)
        {
            Console.WriteLine($"Aviation gets message from {member.GetType().Name}: {message} ({DateTime.Now.ToLongTimeString()})");
        }
    }
}
