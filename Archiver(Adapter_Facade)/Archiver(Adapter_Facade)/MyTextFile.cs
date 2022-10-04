using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    class MyTextFile : AnyFile
    {
        public TextFile textFile;

        public MyTextFile(int size, string name)
        {
            // установка свойств адаптер
            Size = size;
            Name = name;
            // установка свойств адаптируемого объекта
            textFile = new TextFile(size, name);
            Compression = textFile.compression;
        }
    }
}
