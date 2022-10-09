using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    public abstract class Decorator : ArchiverAlgorihtm
    {
        protected ArchiverAlgorihtm? component;

        public Decorator(ArchiverAlgorihtm component)
        {
            this.component = component;
        }
        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }

    public class Encryption : Decorator
    {
        public Encryption(ArchiverAlgorihtm component) : base(component){ }

        public override void Compress(string filename)
        {
            component.Compress(filename);
            Operation();
        }

        public override void DeCompress(string filename)
        {
            component.DeCompress(filename);
            Operation();
        }

        public override void Operation()
        {
            //base.Operation();
            Console.WriteLine("(Encryption)");
        }
    }

    public class Comment : Decorator
    {
        public Comment(ArchiverAlgorihtm component) : base(component) { }

        public override void Compress(string filename)
        {
            component.Compress(filename);
            Operation();
        }

        public override void DeCompress(string filename)
        {
            component.DeCompress(filename);
            Operation();
        }

        public override void Operation()
        {
            Console.WriteLine("Comment");
        }
    }
    public class RecoveryInfo : Decorator
    {
        public RecoveryInfo(ArchiverAlgorihtm component) : base(component) { }

        public override void Compress(string filename)
        {
            component.Compress(filename);
            Operation();
        }

        public override void DeCompress(string filename)
        {
            component.DeCompress(filename);
            Operation();
        }

        public override void Operation()
        {
            Console.WriteLine("Recovery Info + 20% memory");
        }
    }

    public class ShutDown : Decorator
    {
        public ShutDown(ArchiverAlgorihtm component) : base(component) { }

        public override void Compress(string filename)
        {
            component.Compress(filename);
            Operation();
        }

        public override void DeCompress(string filename)
        {
            component.DeCompress(filename);
            Operation();
        }

        public override void Operation()
        {
            Console.WriteLine("Shut Down");
        }
    }

}
