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
        public Dictionary<string, ArchiverAlgorihtm> algorithms;

        public Archiver()
        {
            files = new List<FileInfo>();
            algorithms = new Dictionary<string, ArchiverAlgorihtm>();
        }
        
        void Add(string mask, ArchiverAlgorihtm file)
        {
            algorithms.Add(mask, file);
        }

        public void ZipFolder(string path)
        {
            dinfo = new DirectoryInfo(path);
            files = dinfo.GetFiles("*.*", SearchOption.AllDirectories).ToList();
            foreach (var item in files)
            {
                foreach (var current in algorithms)
                {
                    if(item.Extension.Equals(current.Key))
                    {
                        current.Value.Compress(item);
                    }
                }
            }
        }

        public void ZipFile(string path)
        {
            FileInfo file = new FileInfo(path);
            foreach (var item in algorithms)
            {
                if(file.Extension.Equals(item.Key))
                {
                    item.Value.Compress(file);
                }
            }
        }
        public void UnZipFile(string path)
        {
            FileInfo file = new FileInfo(path);
            foreach (var item in algorithms)
            {
                if (file.Extension.Equals(item.Key))
                {
                    item.Value.DeCompress(file);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in algorithms)
            {
                sb.Append(item.Value);
            }
            return sb.ToString();
        }
    }
}
