namespace Exam_Antivirus_
{
    class MainApp
    {
        static void Main()
        {
            Antivirus antivirus = Antivirus.Instance(new ScanSystem());

            antivirus.Scan();
            Console.Read();
            antivirus.Scan();
            Console.Read();
        }
    }
}