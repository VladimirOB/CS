using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    //"Visitor"
    abstract class Visitor
    {
        //действия которые можно совершать
        //расшифровать / зашифровать файл
        public abstract void Encrypt(SubSystem subSystem);

        public abstract void Comment(SubSystem subSystem);
        //выключение
        public abstract void ShutDown(SubSystem subSystem);

        public abstract void RecoveryInfo(SubSystem subSystem);


    }

    // "ConcreteVisitor1" 
    class ConcreteVisitor1 : Visitor
    {
        public override void Comment(SubSystem subSystem)
        {
            Console.WriteLine("Comment: world. {0} visited by {1}", subSystem.GetType().Name, this.GetType().Name);
        }

        public override void Encrypt(SubSystem subSystem)
        {
            Console.WriteLine("Encrypt light. {0} visited by {1}", subSystem.GetType().Name, this.GetType().Name);
        }

        public override void RecoveryInfo(SubSystem subSystem)
        {
            Console.WriteLine("Recovery Info 30%. {0} visited by {1}", subSystem.GetType().Name, this.GetType().Name);
        }

        public override void ShutDown(SubSystem subSystem)
        {
            Console.WriteLine("ShutDown 7200. {0} visited by {1}", subSystem.GetType().Name, this.GetType().Name);
        }
    }

    // "ConcreteVisitor1" 
    class ConcreteVisitor2 : Visitor
    {
        public override void Comment(SubSystem subSystem)
        {
            Console.WriteLine("Comment: hello. {0} visited by {1}", subSystem.GetType().Name, this.GetType().Name);
        }

        public override void Encrypt(SubSystem subSystem)
        {
            Console.WriteLine("Encrypt hard. {0} visited by {1}", subSystem.GetType().Name, this.GetType().Name);
        }

        public override void RecoveryInfo(SubSystem subSystem)
        {
            Console.WriteLine("Recovery Info 20%. {0} visited by {1}", subSystem.GetType().Name, this.GetType().Name);
        }

        public override void ShutDown(SubSystem subSystem)
        {
            Console.WriteLine("ShutDown 3600. {0} visited by {1}", subSystem.GetType().Name, this.GetType().Name);
        }
    }


    // "Element" 
    abstract class SubSystem
    {
        public abstract void Accept(Visitor visitor);
    }

    // "ConcreteElementA" 

    class ConcreteElementA : SubSystem
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Comment(this);
            visitor.Encrypt(this);
            visitor.ShutDown(this);
            OperationA();
        }

        public void OperationA()
        {
            Console.WriteLine("OperationA");
        }
    }

    // "ConcreteElementB" 

    class ConcreteElementB : SubSystem
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Comment(this);
            visitor.Encrypt(this);
            visitor.ShutDown(this);
            OperationB();
        }

        public void OperationB()
        {
            Console.WriteLine("OperationB");
        }
    }

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
