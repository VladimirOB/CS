using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ionic.Zip;
namespace MementoUpdate
{
    class ProspectMemory
    {
        Dictionary<double, Memento> _mementoPack;
        private double id;
        
        ushort secretKey = 0x00889; // Секретный ключ (16 bit).
        public ProspectMemory()
        {
            _mementoPack = new Dictionary<double, Memento>();
        }

        public void Add(Memento memento)
        {
            id = Convert.ToDouble(DateTime.Now.Hour.ToString() + "," + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString());
            _mementoPack.Add(id, memento);
        }

        public void SaveMemory(string path)
        {
            string tempFile = DonNetZipDeCompress("../../../../MemoryDB.zip"); // извлекаем старый db.txt для дозаписи
            StreamWriter sw = new StreamWriter(tempFile, true);
            foreach (var item in _mementoPack)
            {
                sw.Write(EncodeDecrypt(item.Key) + '|');
                foreach (var memento in item.Value.dict)
                {
                    sw.Write(EncodeDecrypt(memento.Value) + "|"); // шифруем и дозаписываем
                }
                sw.Write('\n');
            }
            sw.Close();
            DonNetZipCompress(path); // закидываем новый db.txt в архив
            File.Delete(tempFile); // удаляем временный db.txt
        }

        void DonNetZipCompress(string path)
        {
            if(File.Exists("../../../../MemoryDB.zip"))
            {
                using (ZipFile zip = ZipFile.Read("../../../../MemoryDB.zip"))
                {
                    foreach (ZipEntry e in zip)
                    {
                        if (e.FileName.Equals(path))
                            zip.RemoveEntry(e.FileName);
                    }
                    zip.AddFile(path);
                    zip.Save("../../../../MemoryDB.zip");
                }
            }
            else
            {
                using (ZipFile zip = new ZipFile("../../../../MemoryDB.zip"))
                {
                    zip.Password = "12321";
                    zip.AddFile(path);
                    zip.Save("../../../../MemoryDB.zip");
                }
            }
            
        }

        string DonNetZipDeCompress(string path)
        {
            if (File.Exists("../../../../MemoryDB.zip"))
            {
                string fileName = "db.txt";
                using (ZipFile zip = ZipFile.Read(path))
                {
                    ZipEntry e = zip[fileName];
                    e.ExtractWithPassword("V:\\Study\\Home Work\\CS\\MementoUpdate\\MementoUpdate\\bin\\Debug\\net6.0", "12321");
                }
                return fileName;
            }
            else
            {
                using (File.Create("db.txt"))
                return "db.txt";
            }
           
        }

        public void Load(string path)
        {
            string tempFile = DonNetZipDeCompress(path);
            string temp = File.ReadAllText(tempFile);
            string[] str = temp.Split(new[] { '|', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < str.Length; i+=4)
            {
                id = Convert.ToDouble(EncodeDecrypt(str[i]));
                _mementoPack.Add(id, new Memento(EncodeDecrypt(str[i + 1]), EncodeDecrypt(str[i + 2]), (EncodeDecrypt(str[i + 3]))));
            }
            File.Delete(tempFile);
        }

        // кодировка - расшифровка.
        string EncodeDecrypt(object str)
        {
            string str2 = new string(str.ToString());
            char[] ch = str2.ToArray();
            string newStr = "";
            foreach (var c in ch) //производим шифрование каждого отдельного символа
            {
                newStr += Secret(c);
            }
            return newStr;
        }

        char Secret(char ch)
        {
            ch = (char)(ch ^ secretKey); //Производим XOR операцию (сложение по модулю 2)
            return ch;
        }

        public void ViewMemory()
        {
            foreach (var item in _mementoPack)
            {
                Console.WriteLine($"Id: {item.Key}");
                Console.WriteLine($"Name: {item.Value.dict["name"]}");
                Console.WriteLine($"Phone: {item.Value.dict["phone"]}");
                Console.WriteLine($"Budget: {item.Value.dict["budget"]}\n");
            }
        }

        public Memento Restore(double id)
        {
            if(_mementoPack.ContainsKey(id))
            {
                Memento temp = _mementoPack[id];
                _mementoPack.Remove(id);
                return temp;
            }
            else
            {
                Console.WriteLine("Invalid id");
                return null;
            }
            
        }
    }
}


//void GZipCompress(string path)
//{
//    // поток для чтения исходного файла
//    using (FileStream sourceStream = new FileStream(path, FileMode.OpenOrCreate))
//    {
//        //поток для записи сжатого файла
//        using FileStream targetStream = File.Create("../../../../MemoryDB.txt.gz");

//        //поток архивации
//        using GZipStream compressionStream = new GZipStream(targetStream, CompressionLevel.Fastest);
//        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой

//        //Console.WriteLine($"Исходный размер: {sourceStream.Length}  сжатый размер: {targetStream.Length}");
//    }
//    File.Delete(path);
//    ZipFile arch = ZipFile.Open("../../../../MemoryDB.txt.gz", ZipArchiveMode.Update);

//}

//string GZipDecompress(string compressedFile, string targetFile)
//{
//    // поток для чтения из сжатого файла
//    using FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate);
//    // поток для записи восстановленного файла
//    using FileStream targetStream = File.Create(targetFile);
//    // поток разархивации
//    using GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress);
//    decompressionStream.CopyTo(targetStream);
//    return targetFile;
//}