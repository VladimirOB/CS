using System.IO;

namespace Prime_Fibonachi
{
    /*class StreamWriterTwo
    {
        public static async Task ExampleAsync()
        {

            using StreamWriter file = new("WriteLines2.txt", append: true);
            await file.WriteLineAsync("Fourth line");
            // полная перезапись файла 
            using (StreamWriter writer = new StreamWriter("test.txt", false))
            {
                await writer.WriteLineAsync("test.txt");
            }
        }
    }*/

    internal class Program
    {
        static void Main(string[] args)
        {

            /*1. Пользователь вводит числа, пока не введёт слово "exit". Программа подсчитывает количество простых чисел*/

            //int countOfPrime = 0, number = 0;

            //while (true)
            //{
            //    string? str1 = Console.ReadLine();

            //    if (str1.Equals("exit"))
            //        break;

            //    if (Int32.TryParse(str1, out number))
            //    {
            //        for (int i = 2; i < number / 2; i++)
            //        {
            //            if (number % i == 0 && number != i)
            //            {
            //                number = 0;
            //                break;
            //            }
            //        }
            //        if (number > 2)
            //        {
            //            countOfPrime++;
            //        }
            //    }
            //}


            ////File.WriteAllText("file.txt", "самый простой способ");
            //StreamWriter SW = new StreamWriter(new FileStream("Prime.txt", FileMode.Create, FileAccess.Write));
            //SW.Write($"Numbers of prime: {countOfPrime}");
            //SW.Close();
            //Console.WriteLine($"Numbers of prime = {countOfPrime}");


            //string readText = File.ReadAllText("Prime.txt");
            //Console.WriteLine($"File:\n{readText}");

            /*2. Пользователь вводит числа, пока не введёт слово "exit". Программа выясняет, является ли эта последовательность чисел числами Фибоначчи.*/


           // 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711,
            int[] number = new int[100];
            int i = 0, sum = 0;
            bool flag = false;
            while (true)
            {
                string? str1 = Console.ReadLine();

                if (str1.Equals("exit"))
                    break;

                if (Int32.TryParse(str1, out number[i]))
                {
                    i++;
                    if (i > 2)
                    {
                        if (number[i-3] < number[i-2] || number[i - 3] == 1 && number[i - 2] == 1) // вот
                        sum = number[i - 3] + number[i - 2];
                        if (sum == 0 || sum > number[i-1])
                        {
                            flag = true;
                            break;
                        }
                        if (sum == number[i - 1])
                        {
                            continue;
                        }
                        else
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }

            //Console.WriteLine($"0 = {temp}, 1 = {number[1]}, 2 = {number[2]}");
            if (!flag)
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");

        }
    }
}