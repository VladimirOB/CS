using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Vector_v2_operators__Timer_del_.jpg_
{
    internal class Program
    {
        //static string path = "V:/folder";
        static System.Timers.Timer timer;
        static int stop = 0;

        static void Task1()
        {
            Vector v = new Vector(10);
            Console.WriteLine(v);
            Vector v2 = v;
            v--;
            v = v - 9;
            Console.WriteLine(v);
            v = v + v2;
            Console.WriteLine(v2);
            int implic = (int)v;
            Console.WriteLine($"implicit: {implic}");
            if(v == v2)
                Console.WriteLine("Yes");
            else Console.WriteLine("No");
        }

        static void Task2_v1(string path)
        {
            /*2.Пользователь вводит имя папки, программа следит за папкой по таймеру и удаляет из неё и из подпапок все картинки(.jpg, .png, .bmp)*/
            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] files = dinfo.GetFiles();
            foreach (var current in files)
            {
                if (current.Extension == ".jpg" || current.Extension == ".bmp" || current.Extension == ".png")
                //if (current.Extension == mask)
                {
                    File.Delete(current.FullName);
                    //Console.WriteLine(current.FullName);
                }
            }

            DirectoryInfo[] folders = dinfo.GetDirectories();
            foreach (var current in folders)
            {
                Task2_v1(path + "/" + current.Name);
            }
        }

        static void Task2Timer(object sender, ElapsedEventArgs e)
        {
            
            Console.WriteLine("Timer tick: {0}", e.SignalTime.ToString());
            Task2_v1("V:/folder");
        }

        static void Main(string[] args)
        {

            //Task1();

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Task2Timer;
            timer.Start();
            for (int i = 1; i < 1000; i++)
            {
                Console.Write($"{i} ");
                if (i == 1)
                    Console.WriteLine("секунда");
                if (i > 2 && i < 5)
                    Console.WriteLine("секунды");
                if (i == 5)
                {
                    Console.WriteLine("секунд");
                }
                Thread.Sleep(1000);
            }
        }
    }
}