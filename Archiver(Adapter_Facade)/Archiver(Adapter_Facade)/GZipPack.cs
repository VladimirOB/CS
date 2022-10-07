using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Archiver_Adapter_Facade_
{
    // Адаптер
    class GZipPack : ArchiverAlgorihtm
    {
        // ссылка на адаптируемый об
        GZipCompressor compressor;

        public GZipPack(string name)
        {
            // установка свойств адаптер
            Title = name;

            // установка свойств адаптируемого объекта
            compressor = new GZipCompressor(7777);

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
