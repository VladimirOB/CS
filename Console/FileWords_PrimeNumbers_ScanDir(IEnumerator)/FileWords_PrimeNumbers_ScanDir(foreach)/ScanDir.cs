using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWords_PrimeNumbers_ScanDir_foreach_
{
    /*3. Создать класс ScanDir, который принимает в конструкторе 
     * путь к папке и маску файлов и позволяет в цикле foreach 
     * перебрать все файлы в папке и в подпапках по указанному пути
    ScanDir fw = new ScanDir(@"c:\students", "*.jpg");
    */
    internal class ScanDir
    {
        string path;
        string mask;
        List<FileInfo> files;

        public ScanDir(string path, string maska)
        {
            this.path = path;
            this.mask = maska;
            DirectoryInfo dir = new DirectoryInfo(path);
            files = dir.GetFiles(mask, SearchOption.AllDirectories).ToList();
        }

        public IEnumerator GetEnumerator()
        {
            foreach (FileInfo file in files)
            {
                yield return file;
            }
        }

    }
}
