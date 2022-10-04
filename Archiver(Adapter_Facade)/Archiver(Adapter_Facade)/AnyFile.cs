using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    abstract class AnyFile
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Compression { get; set; }
    }
}
