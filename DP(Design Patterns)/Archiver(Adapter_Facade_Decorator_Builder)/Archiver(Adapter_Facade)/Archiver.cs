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
        Product product = null;

        public Archiver()
        {
            files = new List<FileInfo>();
            algorithms = new Dictionary<string, ArchiverAlgorihtm>();
        }
        
        void Add(string mask, ArchiverAlgorihtm file)
        {
            algorithms.Add(mask, file);
        }

        void ZipArch(string path, IBuilder builder)
        {
            FileInfo file = new FileInfo(path);
            builder.ReadHeader(file.FullName);
            builder.ReadBody();
            product = builder.GetResult();
            //product.Arch();
        }

        public void UnZipArch(string path, IBuilder builder)
        {
            FileInfo file = new FileInfo(path);
            
            builder.ReadHeader(file.Name);
            builder.ReadBody();
            product = builder.GetResult();
            //product.UnArch();
        }

        public void ZipFolder(string path, IBuilder builder)
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
                        ZipArch(item.FullName, builder);
                    }
                }
            }
            product.Arch();
            product = null;
        }

        public void UnZipFolder(string path, IBuilder builder)
        {
            dinfo = new DirectoryInfo(path);
            files = dinfo.GetFiles("*.*", SearchOption.AllDirectories).ToList();
            foreach (var item in files)
            {
                foreach (var current in algorithms)
                {
                    if (item.Extension.Equals(current.Key))
                    {
                        current.Value.DeCompress(item);
                        UnZipArch(item.FullName, builder);
                    }
                }
            }
            product.UnArch();
            product = null;
        }

        public void ZipFile(string path, IBuilder builder)
        {
            FileInfo file = new FileInfo(path);
            foreach (var item in algorithms)
            {
                if(file.Extension.Equals(item.Key))
                {
                    item.Value.Compress(file);
                    ZipArch(path, builder);
                }
            }
        }
        public void UnZipFile(string path, IBuilder builder)
        {
            FileInfo file = new FileInfo(path);
            foreach (var item in algorithms)
            {
                if (file.Extension.Equals(item.Key))
                {
                    item.Value.DeCompress(file);
                    UnZipArch(path, builder);
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
