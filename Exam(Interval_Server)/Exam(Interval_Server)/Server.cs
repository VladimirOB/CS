using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace Exam_Interval_Server_
{
    /*Задача 2. (6 баллов)

    Программа по таймеру, 1 раз в секунду, просматривает содержимое определённой папки, 
    находит все новые текстовые файлы, делит их на слова и пополняет свой частотный словарь слов без учёта регистра букв,
    который сразу сохраняется в результирующий файл. Важно, чтобы текстовые файлы участвовали в пополнении частотного словаря
    только один раз, вне зависимости от изменений. Также программа пишет в файл .log пути ко всем обработанным файлам и время,
    когда произошла обработка.
    Для решения задачи организовать сервер, который 1 раз в секунду всем подписчикам присылает список файлов для обработки.
    Сам сервер обеспечивает неповторяемость файлов в списке. Должно быть 2 клиента: один формирует частотный словарь и сохраняет 
    (или обновляет) его в файл, второй пишет в файл .log

    Требования: использовать делегаты и events или интерфейсы
    */
    interface IReceiver
    {
        void OnReceive(HashSet<FileInfo> files); // метод делегат
    }

    class Server
    {
        public List<IReceiver> subscribers { get; } = new List<IReceiver>(); //event
        System.Timers.Timer? timer;
        HashSet<FileInfo> newFiles;
        HashSet<FileInfo> files;
        HashSet<FileInfo>? filesTemp;
        DirectoryInfo dinfo;
        bool changes;
        uint timerCnt = 0;
        public Server(string path)
        {
            newFiles = new HashSet<FileInfo>();
            files = new HashSet<FileInfo>();
            dinfo = new DirectoryInfo(path);
        }

        public void Start()
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        //public void Start()
        //{
        //   while(true)
        //    {
        //        Console.WriteLine("Enter 1");
        //        string? str = Console.ReadLine();
        //        if (str == "1")
        //            StartEvent();
        //        if(str == "0")
        //            break;
        //    }
        //}

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Console.WriteLine(timerCnt++);
            StartEvent();
        }

        void StartEvent()
        {
            newFiles.Clear();
            filesTemp = dinfo.GetFiles("*.txt", SearchOption.AllDirectories).ToHashSet();
            changes = false;
            foreach (FileInfo current in filesTemp)
            {
                if (!files.Any(a => a.Name == current.Name))
                {
                    files.Add(current);
                    newFiles.Add(current);
                    changes = true;
                }
            }
            if(changes)
            {
                foreach (var subCurrent in subscribers)
                {
                    subCurrent.OnReceive(newFiles); // Invoke
                }
            }
        }

        public void Add(IReceiver ev)
        {
            subscribers.Add(ev);
        }

        public void Remove(IReceiver ev)
        {
            subscribers.Remove(ev);
        }
    }

    class Client1 : IReceiver, IDisposable
    {
        StreamWriter? sw;
        string path = "../../../../ServerDB.txt";
        HashSet<string> dictionary = new HashSet<string>();
        SortedSet<string> result = new SortedSet<string>();
        char[] separators = { ' ', '\r', '\n', '\t', ',', '.', '-', '_', '<', '?', '!', ':', ';', '1', '2', '3', '4', '5', '6', '7', '8','9','0', '"' };

        public Client1()
        {
            File.WriteAllText(path, "");
        }

        public void Dispose()
        {
            //открыть файл и сортирнуть
            string str = File.ReadAllText(path);
            string[] str2 = str.Split(' ', '\r', '\n');
            foreach (var item in str2)
            {
                result.Add(item);
            }
            StreamWriter sw2 = new StreamWriter(path);
            foreach (var item in result)
            {
                sw2.WriteLine(item);
            }
            sw2.Close();
        }

        void IReceiver.OnReceive(HashSet<FileInfo> files)
        {
            sw = File.AppendText(path);
            foreach (var current in files)
            {
                dictionary.Clear();
                string SourceStr = File.ReadAllText(current.FullName);
                string[] SourceStr2 = SourceStr.Split(separators);
                SourceStr2 = SourceStr2.Where(a => a != "").ToArray();
                foreach (string word in SourceStr2)
                {
                    if (!dictionary.Contains(word))
                    dictionary.Add(word);
                }
                 
                foreach (var word in dictionary)
                {
                    sw.WriteLine(word);
                }
            }
            sw.Close();
        }
    }
    class Client2 : IReceiver
    {
        string path = "../../../../server.log";

        public Client2()
        {
            File.WriteAllText(path, "processed files:\n");
        }

        void IReceiver.OnReceive(HashSet<FileInfo> files)
        {
            
            foreach (var current in files)
            {
                File.AppendAllText("../../../../server.log", current.Name + "\n");
            }
            
        }
    }
}
