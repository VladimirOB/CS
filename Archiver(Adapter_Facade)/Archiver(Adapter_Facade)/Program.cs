namespace Archiver_Adapter_Facade_
{
    class Program
    {
        /*2. Реализовать паттерн Адаптер (и паттерн Фасад) на примере программы "Архиватор".*/
        //операции фасад: распаковать архив, посмотреть весь архив, и тд
        static void Main(string[] args)
        {
            Archiver ar = new Archiver();
            ar.algorithms.Add(".wav", new HuffmanPack("Huffman"));
            ar.algorithms.Add(".txt", new GZipPack("GZip"));
            ar.ZipFolder("V:/123");

        }
    }
}