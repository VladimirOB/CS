using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Event_Timer_ReserveFolder_
{

    delegate int Subscriber(FileInfo current);
    delegate int SubscriberTimer(System.Timers.Timer timer);

    class Server
    {
        //переменная-списка адресов функций (event)
        protected event Subscriber subscribers;
        protected event SubscriberTimer subscriberTimer;
        System.Timers.Timer timer;
        DirectoryInfo dinfo;
        DirectoryInfo dinfoResult;
        HashSet<FileInfo> filesTemp;
        HashSet<FileInfo> files;
        // попробовать HashMap
        string sourceDir;
        string backupDir;
        public Server(string path)
        {
            sourceDir = path;
            backupDir = @"V:\BackUp";
            dinfo = new DirectoryInfo(path);
            dinfoResult = new DirectoryInfo(backupDir);
            if (dinfoResult.Exists == false)
                dinfoResult.Create();
        }

        public void Start()
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            StartEvent();
            TimerEvent();
        }

        //метод, подписывающий клиентские классы на сообщ.
        public void Add(Subscriber ev)
        {
            subscribers += ev;
        }
        public void AddTimer(SubscriberTimer ev)
        {
            subscriberTimer += ev;
        }

        public void TimerEvent()
        {
            subscriberTimer?.Invoke(timer);
        }

        public void StartEvent()
        {
            
            filesTemp = dinfo.GetFiles("*.*", SearchOption.AllDirectories).ToHashSet();
            files = dinfoResult.GetFiles("*.*", SearchOption.AllDirectories).ToHashSet();
            foreach (FileInfo current in filesTemp)
            {
                if (!files.Any(a => a.Name == current.Name && a.Length == current.Length))
                {
                    files.Add(current);
                    File.Copy(Path.Combine(sourceDir, current.Name), Path.Combine(backupDir, current.Name), true);
                    // добавить дубликаты
                    subscribers?.Invoke(current);
                }
            }
        }
    }

    class Client1
    {
        // обработчик события
        public int ShowInfo(FileInfo current)
        {
            int cnt = current.FullName.Length;
            Console.Write($"Server copy {current.Name} to backUpDir");
            for (int i = cnt; i < 40; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine($"size: {current.Length}");
            return 0;
        }
    }


    class Client2
    {
        uint TimerCnt = 0;
        // обработчик события
        public int shutDownServer(System.Timers.Timer timer)
        {
            TimerCnt++;
            Console.WriteLine(TimerCnt);
            if (TimerCnt == 10)
            {
                Console.WriteLine("Press any key.");
                timer.Stop();
            }
               
            
                
            return 0;
        }
    }

    class Client3 : IDisposable
    {
        StreamWriter writer;

        public Client3()
        {
            writer = new StreamWriter("../../../../server.log");
        }
        public void Dispose()
        {
            writer.Close();
        }
        public int Log(FileInfo current)
        {
            int cnt = current.FullName.Length;
            writer.Write(current.Name);
            for (int i = cnt; i < 40; i++)
            {
                writer.Write(" ");
            }
            writer.WriteLine($"size: {current.Length}");
            return 0;
        }

    }
}
