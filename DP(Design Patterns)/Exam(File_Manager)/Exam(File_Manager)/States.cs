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

    enum Operation
    {
        logicalDisks = 1,
        search = 2,
        copy = 3,
        movingAFile = 4,
        creationFile = 5,
        deleteFile = 6,
        exit = 0
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
            Console.WriteLine("Make a choise: ");
            Console.WriteLine("Press 1 to view Logical disks");
            Console.WriteLine("Press 2 to search");
            Console.WriteLine("Press 3 to copy");
            Console.WriteLine("Press 4 to moving a file");
            Console.WriteLine("Press 5 to creation file");
            Console.WriteLine("Press 6 to delete file");
            Console.WriteLine("Press 0 to exit");
            Operation op;
            if (Enum.TryParse(Console.ReadLine(), out op))
            {
                
                switch (op)
                {
                    case Operation.logicalDisks:
                        {
                            context.State = new LogicalDrives();
                            break;
                        }
                    case Operation.search:
                        {
                            context.State = new SearchState();
                            break;
                        }
                    case Operation.copy:
                        {
                            context.State = new CopyState();
                            break;
                        }
                    case Operation.movingAFile:
                        {
                            context.State = new MovingState();
                            break;
                        }
                    case Operation.creationFile:
                        {
                            context.State = new CreateState();
                            break;
                        }
                    case Operation.deleteFile:
                        {
                            context.State = new DeleteStateProxy();
                            break;
                        }
                    case Operation.exit:
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
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class SearchState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Search State");
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class CopyState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Copy State");
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class MovingState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Moving State");
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class CreateState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Create State");
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            context.State = OperationMenu.Instance();
        }
    }

    class DeleteState : State
    {
        public override void Handle(FileManager context)
        {
            Console.WriteLine("Delete State");
            Console.WriteLine("Press any key...");
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
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            context.State = new DeleteState(); // операция обычной панели
        }
    }




}
