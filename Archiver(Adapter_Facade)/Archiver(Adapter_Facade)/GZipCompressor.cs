using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Archiver_Adapter_Facade_
{
    class GZipCompressor
    {
        public double MaxSize { get; set; }

        public GZipCompressor(int x)
        {
            MaxSize = x;
        }

        public void Pack(FileInfo file)
        {
            Console.Write(file.Name);
            Console.WriteLine(" : GZip Pack");
        }

        public void UnPack(FileInfo file)
        {
            Console.Write(file.Name);
            Console.WriteLine(" : GZip UnPack");
        }

        public void ArchiveChecker()
        {

        }

        public List<string> ArchView()
        {
            return null;
        }
    }
}
