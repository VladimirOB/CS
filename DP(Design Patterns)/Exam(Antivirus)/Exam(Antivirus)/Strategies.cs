using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Antivirus_
{
    //для одной задачи!
    abstract class Strategy
    {
        public abstract void Scan();
    }

    class ScanSystem : Strategy
    {
        HashSet<FileInfo> viruses;
        Client client;
        ConcreteObserver observer;
        public ScanSystem()
        {
            client = new Client();
            observer = new ConcreteObserver(client);
        }
        public override void Scan()
        {
            DirectoryInfo dinfo = new DirectoryInfo("V:\\temp");
            viruses = dinfo.GetFiles("virus*.*", SearchOption.AllDirectories).ToHashSet();
            client.Notify(viruses);
        }
    }

    class ScanFirewall : Strategy
    {
        public override void Scan()
        {
            Console.WriteLine("Scan firewall");
        }
    }

    //"Adapter"
    class ScanWeb : Strategy
    {
        ScanWebLinux scanWL = new ScanWebLinux();
        public override void Scan()
        {
            scanWL.Scan();
        }
    }

    //адаптируемый класс
    class ScanWebLinux
    {
        public void Scan()
        {
            Console.WriteLine("Scan web Linux!");
        }
    }
}
