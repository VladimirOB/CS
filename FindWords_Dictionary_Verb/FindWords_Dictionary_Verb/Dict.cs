using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FindWords_Dictionary_Verb
{

    /*2. Создать программу-переводчик, имеющую следующее меню:
    - добавить словарную пару
    - удалить пару
    - просмотреть все пары
    - перевести слово в обоих направлениях
    - перевести предложение (разбив на слова)
    - сохранение / загрузка словаря
    */

    [Serializable]
    public class Dict
    {
        //public SortedDictionary<string, string> pair;
        //public SortedDictionary<string, string> rev_pair ;
        public SortedDictionary<string, string> pair { get; set; }
        public SortedDictionary<string, string> rev_pair { get; set; }

        public Dict()
        {
            pair = new SortedDictionary<string, string>();
            rev_pair = new SortedDictionary<string, string>();
            //try
            //{
            //    StreamWriter sw = new StreamWriter(new FileStream("db.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite)); // вдруг файл удалён
            //    sw.Close();
            //    StreamReader sr = new StreamReader("db.txt");
            //    string? line;
            //    string[] words;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        words = line.Split(' ');
            //        if (!pair.ContainsKey(words[0]))
            //            pair.Add(words[0], words[1]);
            //        if (!rev_pair.ContainsKey(words[1]))
            //            rev_pair.Add(words[1], words[0]);
            //    }
            //    sr.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine(ex.Message);
            //    Console.ReadKey();
            //}
        }

        //public void Load(ref Dict d)
        //{   try
        //    {
        //        using (FileStream fstream2 = File.OpenRead("db.dat"))
        //        {
        //            BinaryFormatter bf2 = new BinaryFormatter();
        //            d = (Dict)bf2.Deserialize(fstream2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        enum Operation
        {
            Add = 1,
            Del = 2 ,
            ViewAll = 3,
            Translate = 4 ,
            TranslateStr = 5,
            ShowRand = 6,
            Load = 7,
            Exit = 0
        }

        void Add()
        {
            string? temp_eng, temp_ru;
            Console.WriteLine("Enter word in English: ");
            temp_eng = Console.ReadLine();
            Console.WriteLine("Enter word in Russian: ");
            temp_ru = Console.ReadLine();
            if (!pair.ContainsKey(temp_eng) && !rev_pair.ContainsKey(temp_ru))
            {
                pair.Add(temp_eng.ToLower(), temp_ru.ToLower());
                Console.WriteLine("Add successfully!(eng)");
                rev_pair.Add(temp_ru.ToLower(), temp_eng.ToLower());
                Console.WriteLine("Add successfully!(ru)");
            }
            else
                Console.WriteLine($"{temp_eng} or {temp_ru} already use.");
            
            Console.ReadKey();
        }
        void DeletePair()
        {
            Console.WriteLine("Enter the word you want to delete: ");
            string? temp = Console.ReadLine();
            if (pair.ContainsKey(temp.ToLower()))
            {
                pair.Remove(temp, out temp);
                rev_pair.Remove(temp);
            }
            if(rev_pair.ContainsKey(temp.ToLower()))
            {
                rev_pair.Remove(temp, out temp);
                pair.Remove(temp);
            }
        }


        void ViewAllPair()
        {
            int i = 1;
            char letter, prev = '0';
            foreach (var item in pair)
            {
                letter = item.Key[0];
                if(letter != prev)
                {
                    Console.WriteLine($"\n{letter}:\n");
                    i = 1;
                }    
                   
                Console.WriteLine(@"{0}){1} - {2}", i++, item.Key, item.Value);
                prev = item.Key[0];
            }
            Console.ReadKey();
        }
        void TranslateWord()
        {
            Console.WriteLine("Enter the word you want to translate: ");
            string? temp = Console.ReadLine();
            bool flag = false;
            if(temp != null)
            {
                if (pair.ContainsKey(temp.ToLower()))
                {
                    Console.WriteLine(pair[temp]);
                    flag = true;
                }
                if (rev_pair.ContainsKey(temp.ToLower()))
                {
                    Console.WriteLine(rev_pair[temp]);
                    flag = true;
                }
            }
            if(!flag)
                Console.WriteLine($"{temp} - not found");
            Console.ReadKey();
        }
        void TranslateString()
        {
            Console.WriteLine("Enter the string you want to translate: ");
            string? temp = Console.ReadLine();
            string[] buffer = temp.Split(' ');
            bool flag = false;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (pair.ContainsKey(buffer[i]))
                {
                    Console.Write(pair[buffer[i]] + " ");
                    flag = true;
                }
                if (rev_pair.ContainsKey(buffer[i]))
                {
                    Console.Write(rev_pair[buffer[i]] + " ");
                    flag = true;
                }
                if(!flag)
                    Console.Write($"(!{buffer[i]}) ");
                flag = false;
            }
            Console.ReadKey();
        }

        void ShowRand()
        {
            Random rand = new Random();
            int cmd = rand.Next(0,pair.Count);
            int cnt = 0;
            foreach (var item in pair)
            {
                if(cnt==cmd)
                    Console.WriteLine(@"{0} - {1}", item.Key, item.Value);
                cnt++;
            }
            Console.ReadKey();
        }

        void Save()
        {
            //StreamWriter sw = new StreamWriter("db.txt");
            //foreach (var item in pair)
            //{
            //    sw.WriteLine(item.Key + " " + item.Value);
            //}
            //sw.Close();
        }

        void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("1) Add pair");
            Console.WriteLine("2) Delete pair");
            Console.WriteLine("3) View all pair");
            Console.WriteLine("4) Translate word");
            Console.WriteLine("5) Traslate string");
            Console.WriteLine("6) Show random pair");
            Console.WriteLine("7) Load");
            Console.WriteLine("0) Exit (save)");
        }

        public int Run()
        {
            Operation op;
            string ch2 = "";
            while(true)
            {
                PrintMenu();
                var ch = Console.ReadKey(true).Key;
                if (ch == ConsoleKey.Enter)
                {
                    continue;
                }
                ch2 = ch.ToString();
                if (ch2.Length > 1 && ch2.Length < 3)
                {
                    Enum.TryParse(ch2[1].ToString(), out op);
                    switch (op)
                    {
                        case Operation.Add:
                            {
                                Add();
                                break;
                            }
                        case Operation.Del:
                            {
                                DeletePair();
                                break;
                            }
                        case Operation.ViewAll:
                            {
                                ViewAllPair();
                                break;
                            }
                        case Operation.Translate:
                            {
                                TranslateWord();
                                break;
                            }
                        case Operation.TranslateStr:
                            {
                                TranslateString();
                                break;
                            }
                        case Operation.ShowRand:
                            {
                                ShowRand();
                                break;
                            }
                        case Operation.Load:
                            {
                                return -1;
                            }


                        case Operation.Exit:
                            {
                                //Save();
                                return 1;
                            }

                        default:
                            break;

                    }
                }
               
                
            }
            
        }
    }
}
