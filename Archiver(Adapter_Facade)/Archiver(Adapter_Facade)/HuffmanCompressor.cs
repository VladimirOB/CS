using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    class HuffmanCompressor
    {
        public int Length { get; set; }

        public HuffmanCompressor(int x)
        {
            Length = x;
        }

        public void Pack(string name)
        {
            Console.WriteLine(name);
            Console.WriteLine("HuffmanCompressor Pack");
            Console.WriteLine();
        }

        public void UnPack(string name)
        {
            Console.WriteLine(name);
            Console.WriteLine("HuffmanCompressor UnPack");
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
