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
        public int Length { get; set; }

        public GZipCompressor(int x)
        {
            Length = x;
        }

        public void Pack(string name)
        {
            Console.WriteLine(name);
            Console.WriteLine("GZipCompressor Pack");
            Console.WriteLine();
        }

        public void UnPack(string name)
        {
            Console.WriteLine(name);
            Console.WriteLine("GZipCompressor UnPack");
            Console.WriteLine();
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
