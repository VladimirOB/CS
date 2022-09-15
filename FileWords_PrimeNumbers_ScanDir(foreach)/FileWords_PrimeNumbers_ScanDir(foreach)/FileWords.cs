using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWords_PrimeNumbers_ScanDir_foreach_
{
    /*1. Создать класс FileWords, который принимает в конструкторе имя текстового файла и позволяет в цикле foreach перебрать все слова из этого файла в обратном порядке
    Пример:
    FileWords fw = new FileWords(@"c:\text.txt");
    foreach(string word in fw)
    {
	    Console.WriteLine(word);
    }*/
    internal class FileWords
    {
        string file_name;

        public FileWords(string file_name)
        {
            this.file_name = file_name;
        }

        public IEnumerator GetEnumerator()
        {
            string str = "";
            try
            {
                str = File.ReadAllText(file_name);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            string[] str2 = str.Split(' ');
            for (int i = str2.Length - 1; i >= 0; i--)
            {
                yield return str2[i];
            }
        }
    }
}
