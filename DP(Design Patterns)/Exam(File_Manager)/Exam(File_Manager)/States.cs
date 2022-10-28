using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_File_Manager_
{
    //Subject - предок для заменяемого объекта и прокси (заместителя), задаёт правила использования
    //"State" - абстрактный класс состояний
    abstract class State
    {
        public abstract void Handle(FileManager context);
    }

    // непотокобезопасная, простая реализация
    class OperationMenu : State
    {
        private static OperationMenu _instance = new OperationMenu();

        //Умышленно пустой конструктор
        private OperationMenu() { }
        public static OperationMenu Instance()
        {
            // ленивое создание
            if (_instance == null)
            {
                // единоразовое создание экземпляра класса
                _instance = new OperationMenu();
            }

            // возврат существующего объекта
            return _instance;
        }

        public override void Handle(FileManager context)
        {
            Console.WriteLine();
            //Console.Clear();
            Console.WriteLine("Make a choise: ");
            Console.WriteLine("Press 1 to view Logical disks");
            Console.WriteLine("Press 2 to search");
            Console.WriteLine("Press 3 to copy");
            Console.WriteLine("Press 4 to moving a file");
            Console.WriteLine("Press 5 to creation file");
            Console.WriteLine("Press 6 to delete file");
            Console.WriteLine("Press 0 to exit");
            int ch;
            if (Int32.TryParse(Console.ReadLine(), out ch))
            {
                switch (ch)
                {
                    case 1:
                        {
                            context.State = new LogicalDrives();
                            break;
                        }
                    case 2:
                        {
                            context.State = new SearchState();
                            break;
                        }
                    case 3:
                        {
                            context.State = new CopyState();
                            break;
                        }
                    case 4:
                        {
                            context.State = new MovingState();
                            break;
                        }
                    case 5:
                        {
                            context.State = new CreateState();
                            break;
                        }
                    case 6:
                        {
                            context.State = new DeleteStateProxy();
                            break;
                        }
                    case 0:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }
    }

    class LogicalDrives : State
    {
        public override void Handle(FileManager context)
        {
            foreach (string current in Directory.GetLogicalDrives())
            {
                Console.WriteLine(current);
            }
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class SearchState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Search State");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class CopyState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Copy State");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class MovingState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Moving State");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class CreateState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Create State");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class DeleteState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Delete State");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    //Протоколирующий прокси: сохраняет в лог все вызовы «Субъекта» с их параметрами.
    class DeleteStateProxy : State
    {
        // Ссылка на класс заменяемого объекта, т.к. часть работы будет выполнена им
        DeleteState realSubject;

        public override void Handle(FileManager context)
        {
            Console.WriteLine("Proxy");

            //условный файл
            string file = "file_name1.exe " + DateTime.Now.ToString() + " size: 0\n";
            File.AppendAllText("../../../../DeleteFiles.log", file);
            Console.ReadLine();
            context.State = new DeleteState(); // операция обычной панели
        }
    }




}
