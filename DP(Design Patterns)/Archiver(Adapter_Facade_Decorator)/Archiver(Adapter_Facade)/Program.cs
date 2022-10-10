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

            Encryption encryptionHP = new Encryption(hp);
            RecoveryInfo recoveryGP = new RecoveryInfo(gp);
            Comment commentLZW = new Comment(lzw);
            ShutDown shutDownEncryptionHP = new ShutDown(encryptionHP);

            ar.algorithms.Add(".wav", shutDownEncryptionHP);
            ar.algorithms.Add(".txt", recoveryGP);
            ar.algorithms.Add(".jpg", commentLZW);
            ar.ZipFolder("V:/temp");

        }
    }
}