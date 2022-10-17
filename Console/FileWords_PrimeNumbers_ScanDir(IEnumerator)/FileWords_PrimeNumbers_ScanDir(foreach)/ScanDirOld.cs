using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWords_PrimeNumbers_ScanDir_foreach_
{
    internal class ScanDirOld : IEnumerable, IEnumerator
    {
        string path;
        string mask;
        int curpos, cnt;
        List<string> f;

        public ScanDirOld(string path, string mask)
        {
            this.path = path;
            this.mask = mask;
            f = new List<string>();
            Find(path);
            curpos = f.Count;
            cnt = curpos+1;
        }

        public void Find(string path)
        {
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(path);
                FileInfo[] files = dinfo.GetFiles(mask);
                foreach (var current in files)
                {
                    f.Add(current.FullName);
                }

                DirectoryInfo[] folders = dinfo.GetDirectories();
                foreach (var current in folders)
                {
                    Find(path + "/" + current.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public object Current
        {
            get
            {
                return f[curpos-cnt];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if(curpos > 0)
            {
                curpos--;
                cnt -= 2;
                return true;
            }
            else
            {
                curpos = f.Count;
                return false;
            }
        }
        public void Reset()
        {
            curpos = f.Count;
        }
    }
}
