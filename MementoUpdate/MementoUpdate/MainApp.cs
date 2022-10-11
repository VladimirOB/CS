using System.IO;
using System.IO.Compression;
using System.Security.AccessControl;
using System.Security.Principal;
namespace MementoUpdate
{
    class MainApp
    {
        public static Random rand = new Random();
        public static string[] name = { "Донателло", "Леонардо", "Микеланджело", "Рафаэль" };
        public static string[] phone = { "(111) 11-11-111", "(222) 22-22-222", "(333) 33-33-333", "(444) 44-44-444" };
        public static double[] budget = { 11111, 2222, 333, 77 };
        /*Реализовать паттерн "Мементо" обладающий следуюшими возможностями:
        - хранение в файловой системе (хранилище)
        - универсальность (возможность хранить данные различных типов) (контейнер)
        - возможность хранения множества копий, привязываясь к дате или к порядковому номеру (хранилище)
        - возможность просмотра всех резервных копий в хранилище (хранилище)

        Опционально:
        - сжатие / шифрование хранилища данных
        - контроль доступа к резервным копиям*/

        public static void SalesRand(SalesProspect s)
        {
            s.Name = name[rand.Next(0,4)];
            s.Phone = phone[rand.Next(0, 4)];
            s.Budget = budget[rand.Next(0, 4)];
        }

        static void Security()
        {
            //Console.Write("Введите полный путь к файлу: ");
            //string myFilePath = Console.ReadLine();

            //try
            //{
            //    using (FileStream myFile = new FileStream(
            //        myFilePath, FileMode.Open, FileAccess.Read))
            //    {
            //        FileSecurity fileSec = myFile.GetAccessControl();
            //        Console.WriteLine("Список ACL перед изменением: ");
            //        foreach (FileSystemAccessRule fileRule in
            //            fileSec.GetAccessRules(true, true, typeof(NTAccount)))
            //        {
            //            Console.WriteLine("{0} {1} доступ {2} для {3}", myFilePath,
            //                fileRule.AccessControlType == AccessControlType.Allow ? "разрешен" : "запрещен",
            //                fileRule.FileSystemRights, fileRule.IdentityReference);
            //        }

            //        Console.WriteLine("\nСписок ACL после изменений: ");
            //        FileSystemAccessRule newRule = new FileSystemAccessRule(
            //            new System.Security.Principal.NTAccount(@"admin"),
            //            FileSystemRights.FullControl,
            //            AccessControlType.Allow);

            //        fileSec.AddAccessRule(newRule);

                    
            //        File.SetAccessControl(myFilePath, fileSec);
            //        foreach (FileSystemAccessRule fileRule in
            //                fileSec.GetAccessRules(true, true, typeof(NTAccount)))
            //        {
            //            Console.WriteLine("{0} {1} доступ {2} для {3}", myFilePath,
            //                fileRule.AccessControlType == AccessControlType.Allow ? "разрешен" : "запрещен",
            //                fileRule.FileSystemRights, fileRule.IdentityReference);
            //        }
            //    }
            //}
            //catch
            //{
            //    Console.WriteLine("Некорректный путь к файлу!");
            //}
            //Console.ReadLine();
        }

        static void ZipFolder()
        {
            string sourceFolder = "V://temp/"; // исходная папка
            string zipFile = "V://temp.zip"; // сжатый файл
            string targetFolder = "V://newtemp"; // папка, куда распаковывается файл

            ZipFile.CreateFromDirectory(sourceFolder, zipFile);
            Console.WriteLine($"Папка {sourceFolder} архивирована в файл {zipFile}");
            ZipFile.ExtractToDirectory(zipFile, targetFolder);

            Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
        }

        static async void GZFile()
        {
            string sourceFile = "1.jpg"; // исходный файл
            string compressedFile = "1.gz"; // сжатый файл
            string targetFile = "1_new.jpg"; // восстановленный файл

            // создание сжатого файла
            await CompressAsync(sourceFile, compressedFile);
            // чтение из сжатого файла
            await DecompressAsync(compressedFile, targetFile);
        }

        static async Task CompressAsync(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate);
            // поток для записи сжатого файла
            using FileStream targetStream = File.Create(compressedFile);

            // поток архивации
            using GZipStream compressionStream = new GZipStream(targetStream, CompressionLevel.Fastest);
            await sourceStream.CopyToAsync(compressionStream); // копируем байты из одного потока в другой

            Console.WriteLine($"Сжатие файла {sourceFile} завершено.");
            Console.WriteLine($"Исходный размер: {sourceStream.Length}  сжатый размер: {targetStream.Length}");
        }

        static async Task DecompressAsync(string compressedFile, string targetFile)
        {
            // поток для чтения из сжатого файла
            using FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate);
            // поток для записи восстановленного файла
            using FileStream targetStream = File.Create(targetFile);
            // поток разархивации
            using GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress);
            await decompressionStream.CopyToAsync(targetStream);
            Console.WriteLine($"Восстановлен файл: {targetFile}");
        }

        static void Main()
        {
            SalesProspect s = new SalesProspect();
            ProspectMemory memory = new ProspectMemory();
            memory.Load("../../../../MemoryDB.txt.gz");
            //memory.Clear();
            //SalesRand(s);
            //memory.Add(s.SaveMemento());//Выполнение back-up. Тут происходит добавление в файл
            //SalesRand(s);
            //memory.Add(s.SaveMemento());
            //memory.SaveMemory("db.txt");

            //Восстановление данных основного объекта из резервной копии
            //s.RestoreMemento(memory.Restore(11.25513));
            //Console.WriteLine(s);

            memory.ViewMemory();
            Console.ReadKey();

        }
    }
}