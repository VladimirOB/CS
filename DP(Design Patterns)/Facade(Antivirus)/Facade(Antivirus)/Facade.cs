using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_Antivirus_
{
    public sealed class AntivirusFacade
    {
        private static readonly AntivirusFacade instance = new AntivirusFacade();

        private AntivirusFacade() { }

        public static AntivirusFacade Instance()
        {
            return instance;
        }

        public void ScanAll(Antivirus antivirus, string path)
        {
            antivirus.ScanNet();
            antivirus.ScanCore();
            antivirus.ScanFirewall();
            antivirus.ScanLocaleDisk(path);
        }
    }
}
