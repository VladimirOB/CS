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

        public static Random rand = new Random();
        public static string[] name = { "Донателло", "Леонардо", "Микеланджело", "Рафаэль" };
        public static string[] phone = { "(111) 11-11-111", "(222) 22-22-222", "(333) 33-33-333", "(444) 44-44-444" };
        public static double[] budget = { 11111, 2222, 333, 77 };
        public static void SalesRand(SalesProspect s)
        {
            s.Name = name[rand.Next(0,4)];
            s.Phone = phone[rand.Next(0, 4)];
            s.Budget = budget[rand.Next(0, 4)];
        }

        static void Main()
        {
            SalesProspect s = new SalesProspect();
            ProspectMemory memory = new ProspectMemory();
            memory.Load("../../../../db.txt");

            //SalesRand(s);
            //memory.Add(s.SaveMemento());//Выполнение back-up. Тут происходит добавление в файл
            //SalesRand(s);
            //memory.Add(s.SaveMemento());

            //Восстановление данных основного объекта из резервной копии
            s.RestoreMemento(memory.Restore(0.45182));


            memory.ViewMemory();
            Console.ReadKey();
        }
    }
}