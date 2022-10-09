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
        public GZipCompressor compressor;

        public GZipPack(string name)
        {
            // установка свойств адаптер
            Title = name;

            // установка свойств адаптируемого объекта
            compressor = new GZipCompressor(30000);
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
            Console.WriteLine("operation");
        }
    }
}
