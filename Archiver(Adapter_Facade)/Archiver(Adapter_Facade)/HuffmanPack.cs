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
        public HuffmanPack(string name)
        {
            Title = name;
            compressor = new HuffmanCompressor(55555);
        }

        public override void Compress(FileInfo file)
        {
            compressor.Pack(file);
        }

        public override void DeCompress(FileInfo file)
        {
            compressor.UnPack(file);
        }

        public override void Operation()
        {
            
        }
    }
}
