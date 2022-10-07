using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    // Адаптер
    class HuffmanPack : ArchiverAlgorihtm
    {
        // ссылка на адаптируемый об
        HuffmanCompressor compressor;
        public int FgColor { get; set; }

        public int BgColor { get; set; }
        public HuffmanPack(string name)
        {
            Title = name;
            compressor = new HuffmanCompressor(55555);
        }

        public override void Compress(string filename)
        {
            compressor.Pack(filename);
        }

        public override void DeCompress(string filename)
        {
            compressor.UnPack(filename);
        }
    }
}
