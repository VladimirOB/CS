using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Antivirus_
{
    sealed class Antivirus
    {
        private static readonly Antivirus _instance = new Antivirus();

        public Antivirus() { }

        private Strategy strategy;

        public static Antivirus Instance(Strategy scan)
        {
            _instance.strategy = scan;
            return _instance;
        }

        public void Scan()
        {
            strategy.Scan();
        }
    }
}
