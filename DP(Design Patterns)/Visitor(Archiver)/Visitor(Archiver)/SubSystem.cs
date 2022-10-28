using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor_Archiver_
{
    // "Element" 
    abstract class SubSystem
    {
        public abstract void Accept(Visitor visitor);
    }

    // "ConcreteElementA" 

    class PackFile : SubSystem // только простешие операции
    {
        public override void Accept(Visitor visitor)
        {
            visitor.TakeFile(this);
            visitor.Encrypt(this);
            visitor.ShutDown(this);
            OperationA();
        }

        public void OperationA()
        {
            //Console.WriteLine("CompressEncrypt end.\n");
        }
    }

    // "ConcreteElementB" 

    class UnpackFile : SubSystem // только простешие операции
    {
        public override void Accept(Visitor visitor)
        {
            visitor.TakeFile(this);
            visitor.Encrypt(this);
            visitor.ShutDown(this);
            OperationB();
        }

        public void OperationB()
        {
            //Console.WriteLine("OperationB");
        }
    }
}
