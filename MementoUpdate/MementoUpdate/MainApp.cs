namespace MementoUpdate
{
    class MainApp
    {
        /*Реализовать паттерн "Мементо" обладающий следуюшими возможностями:
        - хранение в файловой системе (хранилище)
        - универсальность (возможность хранить данные различных типов) (контейнер)
        - возможность хранения множества копий, привязываясь к дате или к порядковому номеру (хранилище)
        - возможность просмотра всех резервных копий в хранилище (хранилище)

        Опционально:
        - сжатие / шифрование хранилища данных
        - контроль доступа к резервным копиям*/

        static void Main()
        {
            // Класс имеет начальные данные
            SalesProspect<string, string, decimal> s = new SalesProspect<string, string, decimal>();
            //s.Name = "Noel van Halen";
            //s.Phone = "(412) 256-0990";
            //s.Budget = 25000.0m;

            // Инициализация хранилища резервных копий
            ProspectMemory memory = new ProspectMemory();
            memory.Load("../../../../db.txt");
            memory.ViewMemory();
            // Выполнение back-up
            //memory.Memento = s.SaveMemento();
            //memory.Add(s.SaveMemento());

            // Изменение свойств основного объекта
            //s.Name = "Leo Welch";
            //s.Phone = "(310) 209-7111";
            //s.Budget = 1000000.0m;
            //memory.Add(s.SaveMemento());

            // Восстановление данных основного объекта из резервной копии
            //s.RestoreMemento(memory.Memento);
            //s.RestoreMemento(memory.Restore(1));


            // Wait for user
            Console.ReadKey();
        }
    }
}