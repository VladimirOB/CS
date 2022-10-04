﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    class SoundFile
    {
        public int Size { get; set; }
        public string FileName { get; set; }

        public string compression = "Compression sound";

        public SoundFile(int size, string fileName)
        {
            Size = size;
            FileName = fileName;
        }

        public void Print()
        {
            int cnt = FileName.Length;
            Console.Write(FileName);
            for (int i = cnt; i < 48; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine($"size: {Size}");
            Console.WriteLine(compression);
        }
    }
}
