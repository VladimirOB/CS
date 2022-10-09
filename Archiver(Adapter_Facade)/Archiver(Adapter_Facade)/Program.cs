namespace Archiver_Adapter_Facade_
{
    class Program
    {
        /*2. Реализовать паттерн Адаптер (и паттерн Фасад) на примере программы "Архиватор".*/
        //операции фасад: распаковать архив, посмотреть весь архив, и тд

        //декораторы:
        // 1)Шифрование.
        // 2)Отвести часть архива под "Информацию под восстановление"
        // 3)Комментарий к архиву
        // 4)Выключение питания после завершения архивации

        // В компонент наследовались элементы управления, а здесь алгоритмы сжатия.
        // Навешивать декоратор на сам алгоритм сжатия
        static void Main(string[] args)
        {
            Archiver ar = new Archiver();

            HuffmanPack hp = new HuffmanPack("Huffman");
            GZipPack gp = new GZipPack("GZip");
            LZWPack lzw = new LZWPack("LZW");

            Encryption encryption = new Encryption(hp);
            RecoveryInfo ri = new RecoveryInfo(gp);
            Comment com = new Comment(lzw);


            ar.algorithms.Add(".wav", encryption);
            ar.algorithms.Add(".txt", ri);
            ar.algorithms.Add(".jpg", com);
            //ar.ZipFile("V:/temp/test.txt");
            Console.WriteLine();
            ar.ZipFolder("V:/temp");

        }
    }
}