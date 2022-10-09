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

        public override void Compress(FileInfo file)
        {
            component.Compress(file);
            Operation();
        }

        public override void DeCompress(FileInfo file)
        {
            component.DeCompress(file);
            Operation();
        }

        public override void Operation()
        {
            //base.Operation();
            Console.WriteLine("Encryption \n");
        }
    }

    public class Comment : Decorator
    {
        public Comment(ArchiverAlgorihtm component) : base(component) { }

        public override void Compress(FileInfo file)
        {
            component.Compress(file);
            Operation();
        }

        public override void DeCompress(FileInfo file)
        {
            component.DeCompress(file);
            Operation();
        }

        public override void Operation()
        {
            Console.WriteLine("Comment\n");
        }
    }
    public class RecoveryInfo : Decorator
    {
        public RecoveryInfo(ArchiverAlgorihtm component) : base(component) { }

        public override void Compress(FileInfo file)
        {
            if(file.Length < (component as GZipPack).compressor.MaxSize)
            {
                component.Compress(file);
                Operation();
            }
            else
            {
                Console.WriteLine("Error RecoveryInfo. MaxLength of file 30000");
                Console.WriteLine($"Length of file is too much!({file.Length})");
                component.Compress(file);
            }
        }

        public override void DeCompress(FileInfo file)
        {
            component.DeCompress(file);
            Operation();
        }

        public override void Operation()
        {
            Console.WriteLine("Recovery Info + 20% memory\n");
        }
    }

    public class ShutDown : Decorator
    {
        public ShutDown(ArchiverAlgorihtm component) : base(component) { }

        public override void Compress(FileInfo file)
        {
            component.Compress(file);
            Operation();
        }

        public override void DeCompress(FileInfo file)
        {
            component.DeCompress(file);
            Operation();
        }

        public override void Operation()
        {
            Console.WriteLine("Shut Down");
        }
    }

}
