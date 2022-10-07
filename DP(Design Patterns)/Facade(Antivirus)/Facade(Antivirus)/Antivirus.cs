using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_Antivirus_
{
    class ScanNet
    {
        public void Scan()
        {
            Console.WriteLine("Scan Net!");
        }
    }

    class ScanFireWall
    {
        public void Scan()
        {
            Console.WriteLine("Scan FireWall!");
        }
    }

    class ScanCore
    {
        public void Scan()
        {
            Console.WriteLine("Scan Core!");
        }
    }
    class ScanLocaleDisks
    {
        DirectoryInfo? dinfo;
        List<FileInfo> localeViruses;
        public List<FileInfo> Scan(string path)
        {
            dinfo = new DirectoryInfo(path);
            localeViruses = dinfo.GetFiles("virus.*", SearchOption.AllDirectories).ToList();
            Console.WriteLine("Scan Locale Disk!");
            Console.WriteLine($"Locale Disk Viruses found: {localeViruses.Count}");
            Console.WriteLine();
            return localeViruses;
        }
    }

    public class Antivirus
    {
        
        List<FileInfo>? localeViruses;
        ScanLocaleDisks sld;
        ScanFireWall sfw;
        ScanCore sc;
        ScanNet sn;
        
        public Antivirus()
        {
             sn = new ScanNet();
             sc = new ScanCore();
             sfw = new ScanFireWall();
             sld = new ScanLocaleDisks();
        }

        public void ScanLocaleDisk(string path)
        {
            localeViruses = sld.Scan(path);

        }

        public void ScanFirewall()
        {
            sfw.Scan();
            Console.WriteLine();
        }

        public void ScanCore()
        {
            sc.Scan();
        }
        public void ScanNet()
        {
            sn.Scan();
        }

        public void PrintDBViruses()
        {
            if(localeViruses != null)
            {
                foreach (var item in localeViruses)
                {
                    Console.WriteLine(item.FullName);
                }
            }
            
        }

        public void DeleteViruses()
        {
            if (localeViruses != null)
            {
                foreach (var item in localeViruses)
                {
                    item.Delete();
                }
            }
        }
    }
}
