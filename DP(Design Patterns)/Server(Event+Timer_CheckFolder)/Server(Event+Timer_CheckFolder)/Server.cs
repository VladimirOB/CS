using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Event_Timer_CheckFolder_
{
    /*1. Сервер работает по таймеру и следит за определённой папкой. При добавлении в папку файла, сервер шлёт уведомления всем подписчикам
    Подписчики:
    1) выводит информацию о файле на экран
    2) удаляет файл из папки, если это картинка
    3) пишет в файл .log*/

    delegate int Subscriber(FileInfo current);

    class Server
    {
        //переменная-списка адресов функций (event)
        protected event Subscriber subscribers;
        System.Timers.Timer timer;
        DirectoryInfo dinfo;
        HashSet<FileInfo> filesTemp;
        HashSet<FileInfo> files;
        public Server(string path)
        {
            dinfo = new DirectoryInfo(path);
            files = dinfo.GetFiles("*.*", SearchOption.AllDirectories).ToHashSet();
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
        }

        //метод, подписывающий клиентские классы на сообщ.
        public void Add(Subscriber ev)
        {
            subscribers += ev;
        }

        public void StartEvent()
        {
            filesTemp = dinfo.GetFiles("*.*", SearchOption.AllDirectories).ToHashSet();
            foreach (FileInfo current in filesTemp)
            {
                if (!files.Any(a => a.FullName == current.FullName && a.LastWriteTime == current.LastWriteTime))
                {
                    files.Add(current);
                    subscribers?.Invoke(current);

                    if (current.Extension == ".jpg" || current.Extension == ".bmp" || current.Extension == ".png")
                        files.Remove(current);
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
            Console.Write($"Client 1: {current.FullName}");
            for (int i = cnt; i < 40; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine($"size: {current.Length}");
            return 0;
        }
    }

    // второй класс подписчик
    class Client2
    {
        // обработчик события
        public int DeleteImage(FileInfo current)
        {
            if(current.Extension == ".jpg" || current.Extension == ".bmp" || current.Extension == ".png")
            {
                File.Delete(current.FullName);
            }
            return 0;
        }
    }

    class Client3 : IDisposable
    {
        // обработчик события
        StreamWriter writer;
        public Client3()
        {
            writer = new StreamWriter("../../../../server.log");
        }

        public void Dispose()
        {
            Console.WriteLine("Destr dispose");
            writer.Close();
        }
        
        public int Log(FileInfo current)
        {
            int cnt = current.FullName.Length;
            writer.Write(current.FullName);
            for (int i = cnt; i < 40; i++)
            {
                writer.Write(" ");
            }
            writer.WriteLine($"size: {current.Length}");
            return 0;
        }

        
    }
}
