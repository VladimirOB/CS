using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Archiver_Adapter_Facade_
{
    class MyImageFile : AnyFile
    {
        public ImageFile imageFile;

        public MyImageFile(int size, string name)
        {
            // установка свойств адаптер
            Size = size;
            Name = name;
           
            // установка свойств адаптируемого объекта
            imageFile = new ImageFile(size, name);
            Compression = imageFile.compression;

        }
    }
}
