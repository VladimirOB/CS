using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    abstract class ArchiverAlgorihtm
    {
        public string Title { get; set; }
        public int SourceSize { get; set; }

        public int PackSize { get; set; }

        public abstract void Compress(string filename);

        public abstract void DeCompress(string filename);

    }
}
