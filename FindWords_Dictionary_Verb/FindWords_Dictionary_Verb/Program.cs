using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FindWords_Dictionary_Verb
{
    public class Program
    {
        static void Task1(string path)
        {
            /*1. Пользователь вводит имя папки, программа сканирует папку, 
             * находит в ней текстовые файлы и формирует результирующий файл, 
             * содержащий столбец со словами без повторений*/
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(path);
                HashSet<FileInfo> files = dinfo.GetFiles("*.txt", SearchOption.AllDirectories).ToHashSet();
                HashSet<string> file = new HashSet<string>();
                string str;
                string[] str2;
                foreach (FileInfo current in files)
                {
                    str = File.ReadAllText(current.FullName);
                    str2 = str.Split(' ', '\n', ',', '.', '!', '?', '\r', ';', ':');
                    for (int i = 0; i < str2.Length; i++)
                    {
                        if(str2[i] != "")
                        if (!file.Contains(str2[i]))
                            file.Add(str2[i]);
                    }
                }
                StreamWriter sw = new StreamWriter("1.txt");
                foreach (var item in file)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
                Console.WriteLine("Oookey!");
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
           
        }

        static void Task3(string path)
        {
            /*3. Пользователь вводит путь к папке с текстовыми файлами. Программа создаёт файл отчёта, который содержит глаголы с предлогами, с которыми эти глаголы употребляются
            */
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(path);
                HashSet<FileInfo> files = dinfo.GetFiles("*.txt", SearchOption.AllDirectories).ToHashSet();
                HashSet<string> set = new HashSet<string>();
                foreach (var item in files)
                {
                    string str = File.ReadAllText(item.FullName);
                    string[] str2 = str.Split(' ', '\n');
                    str2 = str2.Where(x => x != "").ToArray(); // убрал пустые
                    string buffer;
                    string next = "";
                    int one, two = 0;
                    for (int i = 0; i < str2.Length; i++)
                    {
                        if (i < str2.Length - 1)
                        {
                            next = str2[i + 1];
                            two = next.Count() - 1;
                        }
                        buffer = str2[i];

                        if (buffer.Length >= 4)
                        {
                            one = buffer.Count() - 1;
                            if (buffer[one] == 'л' && buffer[one - 1] == 'и' || buffer[one] == 'л' && buffer[one - 1] == 'е' || buffer[one] == 'ь' && buffer[one - 1] == 'ш' && buffer[one - 2] == 'е' || buffer[one] == 'ь' && buffer[one - 1] == 'с' || buffer[one] == 'я' && buffer[one - 1] == 'с' || buffer[one] == 'я' && buffer[one - 1] == 'с' && buffer[one - 2] == 'ь' && buffer[one - 3] == 'т' || buffer[one] == 'ь' && buffer[one - 1] == 'т' && buffer[one - 2] == 'и' || buffer[one] == 'ь' && buffer[one - 1] == 'т' && buffer[one - 2] == 'а' || buffer[one] == 'ь' && buffer[one - 1] == 'т' && buffer[one - 2] == 'я')
                            {
                                if (two == 0)
                                {
                                    if (next[two] == 'о' || next[two] == 'с' || next[two] == 'в' || next[two] == 'у')
                                        set.Add(str2[i] + " " + next);
                                }
                                else if (two == 1)
                                {
                                    if (next[two] == 'о' && next[two - 1] == 'с' ||
                                        next[two] == 'о' && next[two - 1] == 'в' ||
                                        next[two] == 'б' && next[two - 1] == 'о' ||
                                        next[two] == 'о' && next[two - 1] == 'п' ||
                                        next[two] == 'о' && next[two - 1] == 'д' ||
                                        next[two] == 'т' && next[two - 1] == 'о' ||
                                        next[two] == 'а' && next[two - 1] == 'з' ||
                                        next[two] == 'а' && next[two - 1] == 'н' ||
                                        next[two] == 'з' && next[two - 1] == 'и')
                                        set.Add(str2[i] + " " + next);
                                }
                                else if (two > 1 && two < 5)
                                {
                                    if(next[two] == 'д' && next[two - 1] == 'а' && next[two - 2] == 'н' ||
                                       next[two] == 'о' && next[two - 1] == 'т' && next[two - 2] == 'о' ||
                                       next[two] == 'д' && next[two - 1] == 'о' && next[two - 2] == 'п' ||
                                       next[two] == 'о' && next[two - 1] == 'р' && next[two - 2] == 'п' ||
                                       next[two] == 'о' && next[two - 1] == 'б' && next[two - 2] == 'о' ||
                                       next[two] == 'з' && next[two - 1] == 'е' && next[two - 2] == 'б' ||
                                       next[two] == 'о' && next[two - 1] == 'д' && next[two - 2] == 'а' && next[two - 3] == 'н' ||
                                       next[two] == 'о' && next[two - 1] == 'д' && next[two - 2] == 'о' && next[two - 3] == 'п' ||
                                       next[two] == 'з' && next[two - 1] == 'и' && next[two - 2] == 'л' && next[two - 3] == 'б' ||
                                       next[two] == 'з' && next[two - 1] == 'е' && next[two - 2] == 'р' && next[two - 3] == 'ч' ||
                                       next[two] == 'д' && next[two - 1] == 'е' && next[two - 2] == 'р' && next[two - 3] == 'п' ||
                                       next[two] == 'о' && next[two - 1] == 'л' && next[two - 2] == 'о' && next[two - 3] == 'к' && next[two - 4] == 'о' ||
                                       next[two] == 'з' && next[two - 1] == 'е' && next[two - 2] == 'р' && next[two - 3] == 'е' && next[two - 4] == 'ч' ||
                                       next[two] == 'а' && next[two - 1] == 'з' && next[two - 2] == '-' && next[two - 3] == 'з' && next[two - 4] == 'и' ||
                                       next[two] == 'д' && next[two - 1] == 'е' && next[two - 2] == 'р' && next[two - 3] == 'е' && next[two - 4] == 'п' ||
                                       next[two] == 'ь' && next[two - 1] == 'л' && next[two - 2] == 'о' && next[two - 3] == 'д' && next[two - 4] == 'в')
                                       set.Add(str2[i] + " " + next);
                                }
                                else if(two > 4)
                                {
                                    if(next[two] == 'г' && next[two - 1] == 'у' && next[two - 2] == 'р' && next[two - 3] == 'к' && next[two - 4] == 'о' && next[two - 5] == 'в' ||
                                       next[two] == 'ь' && next[two - 1] == 'з' && next[two - 2] == 'о' && next[two - 3] == 'в' && next[two - 4] == 'к' && next[two - 5] == 'с' ||
                                       next[two] == 'д' && next[two - 1] == 'о' && next[two - 2] == 'п' && next[two - 3] == '-' && next[two - 4] == 'з' && next[two - 5] == 'и' ||
                                       next[two] == 'и' && next[two - 1] == 'з' && next[two - 2] == 'и' && next[two - 3] == 'л' && next[two - 4] == 'б' && next[two - 5] == 'в' ||
                                       next[two] == 'м' && next[two - 1] == 'о' && next[two - 2] == 'г' && next[two - 3] == 'у' && next[two - 4] == 'р' && next[two - 5] == 'к')
                                       set.Add(str2[i] + " " + next);
                                }

                            }
                        }
                    }
                }
                List<string> lst = new List<string>(set);
                lst.Sort();
                StreamWriter sw = new StreamWriter("verb.txt");
                foreach (var item in lst)
                {
                    Console.WriteLine(item);
                    sw.WriteLine(item);
                }
                sw.Close();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }

        static void SaveDictBF(Dict d)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fstream = new FileStream("db.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                bf.Serialize(fstream, d);
                fstream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        
        static void SaveDictJS(Dict d)
        {
            string res = JsonSerializer.Serialize<Dict>(d);
            File.WriteAllText("db.json", res);
        }

        static Dict Load()
        {
            Dict d;
            using (FileStream reader = File.OpenRead("../../../../db.json"))
            {
                d = JsonSerializer.Deserialize<Dict>(reader);
            }
            return d;
        }

        static void Main(string[] args)
        {
            //Task1("V:\\temp")

            Dict d = new Dict();
            int res = d.Run();
            if(res == 1)
            {
                SaveDictBF(d);
            }
            if(res == -1)
            {
                d = Load();
                d.Run();
            }

            //Dict d;

            //try
            //{
            //    using (FileStream reader = File.OpenRead("../../../../db.json"))
            //    {
            //        d = JsonSerializer.Deserialize<Dict>(reader);
            //        d.Run();
            //    }
            //    SaveDictJS(d);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}


            //Dict d;
            //try
            //{
            //    using (FileStream fstream = File.OpenRead("db.dat"))
            //    {
            //        BinaryFormatter bf2 = new BinaryFormatter();
            //        d = (Dict)bf2.Deserialize(fstream);
            //        d.Run();
            //    }

            //    SaveDictBF(d);

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            //myStopwatch.Start();
            //Task3("V:\\MyDocuments\\TextFiles\\UTF"); // 6 sek
            //myStopwatch.Stop();
            //TimeSpan ts = myStopwatch.Elapsed;
            //string elapsedTime = String.Format("{0:00}.{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            //Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}