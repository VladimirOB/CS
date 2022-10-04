using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    class MySoundFile : AnyFile
    {
        public SoundFile soundFile;

        public MySoundFile(int size, string name)
        {
            // установка свойств адаптер
            Size = size;
            Name = name;
            
            // установка свойств адаптируемого объекта
            soundFile = new SoundFile(size, name);
            Compression = soundFile.compression;

        }
    }
}
