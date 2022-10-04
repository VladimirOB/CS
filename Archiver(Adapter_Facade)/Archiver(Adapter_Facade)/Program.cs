namespace Archiver_Adapter_Facade_
{
    class Program
    {
        /*2. Реализовать паттерн Адаптер (и паттерн Фасад) на примере программы "Архиватор".*/
        static void Main(string[] args)
        {
            Archiver ar = new Archiver();
            ar.ScanDir("V:/temp");
            ar.Print();
        }
    }
}