using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    class LZWPack : ArchiverAlgorihtm
    {
        // ссылка на адаптируемый об
        public LZWCompressor compressor;

        public LZWPack(string title)
        {
            this.Title = title;
            compressor = new LZWCompressor(99999);
        }

        public override void Compress(string filename)
        {
            compressor.Pack(filename);
        }

        public override void DeCompress(string filename)
        {
            compressor.UnPack(filename);
        }

        public override void Operation()
        {
           
        }
    }
}
