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

        public void Pack(FileInfo file)
        {
            Console.Write(file.Name);
            Console.WriteLine(" : Huffman Pack");
        }

        public void UnPack(FileInfo file)
        {
            Console.Write(file.Name);
            Console.WriteLine(" : Huffman UnPack");
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
