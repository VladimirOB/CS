using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_String
{
    internal class String
    {
        private string? Str;

        public String(string str)
        {
            Str = str;
        }
        public void Print()
        {
            Console.WriteLine(Str);
        }

        public void Set(string str)
        {
            this.Str = str;
        }

        public void ToUp()
        {
            Str = Str.ToUpper();
        }

        public void ToLow()
        {
            Str = Str.ToLower();
        }
        public void RemoveDig()
        {
            string temp = ""; 
            for (int i = 0; i < Str.Length; i++)
            {
                if (!char.IsDigit(Str[i]))
                {
                    temp += Str[i];
                }
            }
            Str = temp;
            while (Str.Contains("  ")) { Str = Str.Replace("  ", " "); } // удаляет лишние пробелы
           //Str = System.Text.RegularExpressions.Regex.Replace(Str, @"\s+", " "); // удаляет лишние пробелы
        }

        public string Get()
        {
            return Str;
        }

        public int GetVowelsCount()
        {
            int vow = 0;
            for (int i = 0; i < Str.Length; i++)
            {
                if (Str[i].Equals('a') || Str[i].Equals('e'))
                {
                    vow++;
                }
            }
            return vow;
        }

        public void Save()
        {
            //File.WriteAllText("file.txt", "самый простой способ");
            StreamWriter SW = new StreamWriter(new FileStream("DB.txt", FileMode.Create, FileAccess.Write));
            SW.Write(Str);
            SW.Close();
            
        }
        public void Load()
        {   
            //string readText = File.ReadAllText("Prime.txt");
            StreamReader SR = new StreamReader(new FileStream("DB.txt", FileMode.Open, FileAccess.Read));
            Str = SR.ReadLine();
            SR.Close();
            
        }
    }
}
