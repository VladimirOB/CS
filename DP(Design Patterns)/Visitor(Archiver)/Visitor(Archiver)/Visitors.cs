using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Visitor_Archiver_
{
    //"Visitor"
    abstract class Visitor
    {
        //действия которые можно совершать
        public abstract void TakeFile(SubSystem subSystem);

        public abstract void Encrypt(SubSystem subSystem);

        public abstract void AddPassword(SubSystem subSystem);
        public abstract void ShutDown(SubSystem subSystem);


    }

    // "ConcreteVisitor1" 
    class VisitorMin : Visitor
    {
        public override void AddPassword(SubSystem subSystem)
        {
            Console.WriteLine("Add password! 1234");
        }
        public override void Encrypt(SubSystem subSystem)
        {
            Console.WriteLine("Encrypt light");
        }

        public override void ShutDown(SubSystem subSystem)
        {
            Console.WriteLine("ShutDown 7200");
        }

        public override void TakeFile(SubSystem subSystem)
        {
            Console.WriteLine("OpenFile: C:\\temp\\1.txt");
        }
    }

    // "ConcreteVisitor2" 
    class VisitorMax : Visitor
    {
        public override void AddPassword(SubSystem subSystem)
        {
            Console.WriteLine("Add password! 4321");
        }

        public override void Encrypt(SubSystem subSystem)
        {
            Console.WriteLine("Encrypt hard");
        }

        public override void ShutDown(SubSystem subSystem)
        {
            Console.WriteLine("ShutDown 3600");
        }

        public override void TakeFile(SubSystem subSystem)
        {
            Console.WriteLine("OpenFile: C:\\temp\\1.txt");
        }
    }
}
