using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Event_Timer_ReserveFolder_
{

    delegate int Subscriber(FileInfo current, uint timerCnt);

    class Server
    {
        //переменная-списка адресов функций (event)
        protected event Subscriber subscribers;
        System.Timers.Timer timer;
        DirectoryInfo dinfo;
        DirectoryInfo dinfoResult;
        HashSet<FileInfo> filesTemp;
        HashSet<FileInfo> files;
        uint timerCnt = 0;
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
            timerCnt++;
            Console.WriteLine($"Time: {timerCnt}");
            StartEvent();
        }

        //метод, подписывающий клиентские классы на сообщ.
        public void Add(Subscriber ev)
        {
            subscribers += ev;
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
                    
                    subscribers?.Invoke(current, timerCnt);
                }
            }
            if (timerCnt == 10)
                Environment.Exit(0);
        }
    }

    class Client1
    {
        // обработчик события
        public int ShowInfo(FileInfo current, uint timerCnt)
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
        // обработчик события
        public int shutDownServer(FileInfo current, uint timerCnt)
        {
            if (timerCnt == 10)
                Environment.Exit(0);
            return 0;
        }
    }

    class Client3
    {
        // обработчик события
        public int Log(FileInfo current, uint timerCnt)
        {
            int cnt = current.FullName.Length;
            using (StreamWriter writer = File.AppendText("../../../../server.log"))
            {
                writer.Write(current.FullName);
                for (int i = cnt; i < 40; i++)
                {
                    writer.Write(" ");
                }
                writer.WriteLine($"size: {current.Length}");
            }

            return 0;
        }

    }
}
