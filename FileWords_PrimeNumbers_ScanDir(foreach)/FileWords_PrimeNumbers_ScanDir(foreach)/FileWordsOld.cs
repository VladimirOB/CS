using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWords_PrimeNumbers_ScanDir_foreach_
{
    internal class FileWordsOld : IEnumerable, IEnumerator
    {
        string file_name;
        int curpos;
        string[] str;

        public FileWordsOld(string file_name)
        {
            this.file_name = file_name;
            string temp = "";
            try
            {
                temp = File.ReadAllText(file_name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            str = temp.Split(' ');
            curpos = str.Length;
        }

        public object Current
        {
            get
            {
                return str[curpos];
            }
        }

        public IEnumerator GetEnumerator()
        {
            // вызов стандартного энумератора стандартной коллекции
            //return people.GetEnumerator();

            return this;
        }

        public bool MoveNext()
        {
            if (curpos > 0)
            {
                curpos--;
                return true;
            }
            else
            {
                // сброс счётчика
                curpos = str.Length;
                return false;
            }
        }

        public void Reset()
        {
            Console.WriteLine("Reset");
            curpos = str.Length;
        }
    }
}
