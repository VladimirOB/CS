using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Vector_Verb_Adjective_nouns
{
    internal class Program
    {
        static void Task1()
        {
            Vector v = new Vector(10);
            Console.WriteLine(v);
            v[0] = 777;
            v[1] = 111;
            v[2] = 70;
            v.Print();
            Console.WriteLine($"\nSum =  {v.Sum()}");
            //v.Save();
            v.Clear();
            Console.WriteLine(v);
            v.Load();
            Console.WriteLine(v);
        }

        static void Task2(string path, List<string> file_names)
        {
            /*2. Пользователь указывает путь к папке с текстовыми файлами. Программа сканирует папку и подпапки, разбивает текстовые файлы на слова
            и все обнаруженные глаголы, прилагательные и имена существительные кладёт в разные файлы, отсортированные в алфавитном порядке.
            Реализовать на 2 языках программирования: C++ и C# и засечь время выполнения и там и там.*/
            DirectoryInfo dinfo = new DirectoryInfo(path);
            if (dinfo.Exists)
            {
                try
                {
                    FileInfo[] files = dinfo.GetFiles("*.txt");
                    foreach (FileInfo current in files)
                    {
                        file_names.Add(current.FullName);
                        Console.WriteLine($"{current.FullName}");
                        
                    }
                    DirectoryInfo[] dirs = dinfo.GetDirectories();
                    foreach (DirectoryInfo current in dirs)
                    {

                        Task2(path + @"\" + current.Name, file_names); // вытянуть из рекурсии)
                    }
                    
                }
                catch(Exception ex)
                         { Console.ForegroundColor = ConsoleColor.Red;  Console.WriteLine(ex.ToString());}
            }


        }

        public static String readFileAsUtf8(string fileName)
        {
            //Encoding encoding = Encoding.Default;
            //String original = String.Empty;

            //using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            //{
            //    original = sr.ReadToEnd();
            //    encoding = sr.CurrentEncoding;
            //    sr.Close();
            //}

            //if (encoding == Encoding.UTF8)
            //    return original;

            //byte[] encBytes = encoding.GetBytes(original);
            //byte[] utf8Bytes = Encoding.Convert(encoding, Encoding.UTF8, encBytes);
            string str = File.ReadAllText(fileName, Encoding.ASCII);

            return str;
        }

        //byte[] ansiBytes = File.ReadAllBytes(file_names[k]);
        //var utf8String = Encoding.Default.GetString(ansiBytes);
        //File.WriteAllText("V:/www/111.txt", utf8String);
        //var t = File.ReadAllText(file_names[k], Encoding.Default);
        //File.WriteAllText("V:/www/111.txt", t, Encoding.UTF8);

        static void Prog(HashSet<string> lst_verb, HashSet<string> lst_adj, HashSet<string> lst_noun, List<string> file_names)
        {
            for (int k = 0; k < file_names.Count; k++)
            {
                string str = File.ReadAllText(file_names[k]);
                //str = System.Text.RegularExpressions.Regex.Replace(str, "[-.?!)«(,:\\d'[\\]']", "");
                string[] str2 = str.Split(' ', '\n', '\r');
                string buffer;
                int one, two, three, four;
                for (int i = 0; i < str2.Length; i++)
                {
                    buffer = str2[i];
                    if (buffer.Length == 0)
                        continue;

                    if (buffer.Length >= 4)
                    {
                        one = buffer.Count() - 1;
                        two = buffer.Count() - 2;
                        three = buffer.Count() - 3;
                        four = buffer.Count() - 4;

                        if (buffer[one] == 'л' && buffer[two] == 'и' || buffer[one] == 'л' && buffer[two] == 'е' || buffer[one] == 'ь' && buffer[two] == 'ш' && buffer[three] == 'е' || buffer[one] == 'ь' && buffer[two] == 'с' || buffer[one] == 'я' && buffer[two] == 'с' || buffer[one] == 'я' && buffer[two] == 'с' && buffer[three] == 'ь' && buffer[four] == 'т' || buffer[one] == 'ь' && buffer[two] == 'т' && buffer[three] == 'и' || buffer[one] == 'ь' && buffer[two] == 'т' && buffer[three] == 'а' || buffer[one] == 'ь' && buffer[two] == 'т' && buffer[three] == 'я')
                        {
                            lst_verb.Add(str2[i]);
                        }
                        if (buffer[one] == 'й' && buffer[two] == 'ы' || buffer[one] == 'о' && buffer[two] == 'г' && buffer[three] == 'о' || buffer[one] == 'у' && buffer[two] == 'м' && buffer[three] == 'о' || buffer[one] == 'й' && buffer[two] == 'и' || buffer[one] == 'о' && buffer[two] == 'г' && buffer[three] == 'е' || buffer[one] == 'у' && buffer[two] == 'м' && buffer[three] == 'е' || buffer[one] == 'й' && buffer[two] == 'о' || buffer[one] == 'м' && buffer[two] == 'и')
                        {
                            lst_adj.Add(str2[i]);
                        }
                        if (buffer[one] == 'я' && buffer[two] == 'м')
                        {
                            lst_noun.Add(str2[i]);
                        }
                    }

                    //if (str2[i].EndsWith("ил") || str2[i].EndsWith("ел") || str2[i].EndsWith("ешь") || str2[i].EndsWith("сь") || str2[i].EndsWith("ся") || str2[i].EndsWith("ться") || str2[i].EndsWith("ить") || str2[i].EndsWith("ать") || str2[i].EndsWith("ять"))
                    //{
                    //    if (!lst_verb.Contains(str2[i]))
                    //        lst_verb.Add(str2[i]);
                    //}

                    //if ((str2[i] != "его" && str2[i] != "ого" && str2[i] != "ему" && str2[i] != "им") && (str2[i].EndsWith("ый") || str2[i].EndsWith("ого") || str2[i].EndsWith("ому") || str2[i].EndsWith("ий") || str2[i].EndsWith("его") || str2[i].EndsWith("ему") || str2[i].EndsWith("ой") || str2[i].EndsWith("им")))
                    //{
                    //    if (!lst_adj.Contains(str2[i]))
                    //        lst_adj.Add(str2[i]);
                    //}

                    //if (str2[i].EndsWith("мя"))
                    //{
                    //    if (!lst_noun.Contains(str2[i]))
                    //        lst_noun.Add(str2[i]);
                    //}
                }
                //lst_verb.Sort();
                //lst_adj.Sort();
                //lst_noun.Sort();
                //lst_adj.Distinct().ToList();


                //foreach (var item in lst_verb)
                //{
                //    verb.WriteLine(item);
                //}
                //foreach (var item in lst_adj)
                //{
                //    adj.WriteLine(item);
                //}
                //foreach (var item in lst_noun)
                //{
                //    noun.WriteLine(item);
                //}
            }
           
        }

        static void Main(string[] args)
        {
            //Task1();
            // С++ -> 21 секунда
            // 1й замер +- пол часа.
            // 2й замер без EndsWith -> 5 мин 21 сек (adj = 42млн строк(700мб), verb = 52млн строк(980мб))
            // 3й замер List заменил на HashSet -> 7 секунд
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();

            StreamWriter verb = new StreamWriter("Task2\\verb.txt");
            StreamWriter adjective = new StreamWriter("Task2\\adjective.txt");
            StreamWriter nouns = new StreamWriter("Task2\\nouns.txt");
            HashSet<string> set_verb = new HashSet<string>();
            HashSet<string> set_adj = new HashSet<string>();
            HashSet<string> set_noun = new HashSet<string>();
            List<string> file_names = new List<string>();

            Task2("V:\\temp", file_names);
            Prog(set_verb, set_adj, set_noun, file_names);

            List<string> l_verb = new List<string>(set_verb);
            List<string> l_adj = new List<string>(set_adj);
            List<string> l_noun = new List<string>(set_noun);
            l_verb.Sort();
            l_adj.Sort();
            l_noun.Sort();
            foreach (var item in l_verb)
            {
                verb.WriteLine(item);
            }
            foreach (var item in l_adj)
            {
                adjective.WriteLine(item);
            }
            foreach (var item in l_noun)
            {
                nouns.WriteLine(item);
            }
            verb.Close();
            adjective.Close();
            nouns.Close();


            myStopwatch.Stop();
            TimeSpan ts = myStopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}.{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}