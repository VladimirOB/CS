using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    class Archiver
    {
        List<FileInfo> files;
        DirectoryInfo dinfo;
        List<AnyFile> children;

        public Archiver()
        {
            files = new List<FileInfo>();
            children = new List<AnyFile>();
        }
        
        void Add(AnyFile file)
        {
            children.Add(file);
        }

        public void ScanDir(string path)
        {
            dinfo = new DirectoryInfo(path);
            files = dinfo.GetFiles("*.*", SearchOption.AllDirectories).ToList();
            foreach (var current in files)
            {
                if(current.Extension == ".txt")
                {
                    children.Add(new MyTextFile((int)current.Length, current.Name));
                }
                if (current.Extension == ".jpg" || current.Extension == ".bmp" || current.Extension == ".png")
                {
                    children.Add(new MyImageFile((int)current.Length, current.Name));
                }
                if (current.Extension == ".mp3" || current.Extension == ".wav" || current.Extension == ".mid")
                {
                    children.Add(new MySoundFile((int)current.Length, current.Name));
                }
            }
        }

        public void Test()
        {
            foreach (var item in children)
            {
                (item as MyTextFile)?.textFile.Print();
            }
        }

        public void Print()
        {
            foreach (var item in children)
            {
                int cnt = item.Name.Length + item.Compression.Length;
                Console.Write(item.Name + " " + item.Compression);
                for (int i = cnt; i < 48; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine($"size: {item.Size}");
            }
        }
    }
}
