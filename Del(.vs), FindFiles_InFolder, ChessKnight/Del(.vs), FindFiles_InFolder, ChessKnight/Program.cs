using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Del_.vs___FindFiles_InFolder__ChessKnight
{
    internal class Program
    {
        static void Task1(string path)
        {
            /*1.Пользователь вводит имя папки, программа сканирует эту папку с подпапками и удаляет папки:
            .vs  bin debug obj*/
            int cnt = 0;
            DirectoryInfo dinfo = new DirectoryInfo(path);
            if (dinfo.Exists)
            {
                try
                {
                    DirectoryInfo[] dirs = dinfo.GetDirectories();
                    foreach (DirectoryInfo current in dirs)
                    {
                        if (current.Name.Equals(".vs") || current.Name.Equals("debug") || current.Name.Equals("obj") || current.Name.Equals("bin"))
                        {
                            Directory.Delete(current.FullName, true);

                            Console.WriteLine($"Delete {current.FullName} successfull {++cnt}");
                        }
                        else
                            Task1(path + @"\" + current.Name);
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }



        }

        static void Task2(string path, StreamWriter sw)
        {
            /*2.Пользователь вводит маску файлов(*.txt, a *.jpg) и начальную папку, программа ищет все файлы по
                маске в папке и подпапках и результаты поиска записывает в файл отчёта*/
            Console.ForegroundColor = ConsoleColor.Blue;
            DirectoryInfo dinfo = new DirectoryInfo(path);
            string mask = "s*.txt";
            string extension;
            if (dinfo.Exists)
            {
                try
                {
                    FileInfo[] files = dinfo.GetFiles(mask);
                    foreach (FileInfo current in files)
                    {
                        extension = Path.GetExtension(current.FullName);
                        {
                            Console.WriteLine($"{current.FullName}");
                            sw.WriteLine(current.FullName);
                        }
                    }

                    DirectoryInfo[] dirs = dinfo.GetDirectories();
                    foreach (DirectoryInfo current in dirs)
                    {
                        //Console.WriteLine("<DIR>\t" + path + "\\" + current.Name);
                        Task2(path + @"\" + current.Name, sw);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        static void Task3()
        {
            /*3.Шахматный конь начинает "скакать" с поля а1.Он должен проскакать через всё поле, посетив каждую клетку
              ОДИН раз. Все найденные варианты нужно записывать в файл отчёта.
              Программу можно прервать и она может продолжить при следующем запуске работу с места прерывания.*/

            bool[,] field = new bool[8, 8];
            field[0, 0] = true; // a1
            int close = 0;
            int i = 0, k = 0;
            while (true)
            {
                if (field[i + 2, k + 1] == false)
                    field[i + 2, k + 1] = true;
            }

        }

        static void Main(string[] args)
        {
            Task1("V:\\Study\\Home Work\\Program");
            //StreamWriter sw = new StreamWriter("result(2).txt");
            //Task2("V:\\TEMP\\task2", sw);
            //sw.Close();

            //Task3();
        }
    }
}