namespace FileWords_PrimeNumbers_ScanDir_foreach_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Task_1:");
            //FileWords fw = new FileWords("V:/dz.txt");
            //foreach (string words in fw)
            //{
            //    Console.Write(words + " ");
            //}

            //Console.WriteLine("\nOld:");

            //FileWordsOld fwo = new FileWordsOld("V:/dz.txt");
            //foreach (string words in fwo)
            //{
            //    Console.Write(words + " ");
            //}

            //Console.WriteLine();
            //Console.WriteLine("\nTask_2:");
            //PrimeNumbers pn = new PrimeNumbers(1, 13);
            //foreach (var item in pn)
            //{
            //    Console.Write(item + " ");
            //}

            //Console.WriteLine("Old:");

            //PrimeNumbersOld pno = new PrimeNumbersOld(13, 13);
            //foreach (var item in pno)
            //{
            //    Console.Write(item + " ");
            //}

            Console.WriteLine();
            Console.WriteLine("\nTask_3:");
            ScanDir sd = new ScanDir("V:\\temp", "*.jpg");
            foreach (var current in sd)
            {
                Console.WriteLine(current);
            }
            Console.WriteLine();
            Console.WriteLine("\nOld:");
            ScanDirOld sdo = new ScanDirOld("V:\\temp", "*.jpg");
            foreach (var current in sdo)
            {
                Console.WriteLine(current);
            }
        }
    }
}