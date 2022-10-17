namespace CS_Second
{
    internal class Program
    {
       /* Пользователь вводит размер массива и потом сам массив, 
            программа находит  2 самых маленьких элемента массива 
            за один проход по массиву и выводит их на экран*/

        static void Main(string[] args)
        {
            //string size = Console.ReadLine();
            //int new_size = Convert.ToInt32(size);
            //int[] a = new int[new_size];
            //for (int i = 0; i < a.Length; i++)
            //{
            //    string n = Console.ReadLine();
            //    a[i] = Convert.ToInt32(n);
            //}
            //int min = 1111110;
            //int pre_min = 1111110;
            //for (int i = 0; i < a.Length; i++)
            //{

            //    if (a[i] < min)
            //    {
            //        pre_min = min;
            //        min = a[i];

            //    }
            //    else if (a[i] < pre_min)
            //    {
            //        pre_min = a[i];

            //    }

            //}
            // Console.Write(@"Min1 = {0}, Min2 = {1}", min, pre_min);

            //int[,] a = new int[3, 5] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 0 }, { 1, 2, 3, 4, 5 } };
            //for (int i = 0; i < a.GetLength(0); i++)
            //{
            //    for (int k = 0; k < a.GetLength(1); k++)
            //    {
            //        Console.Write($"{a[i, k]} ");
            //    }
            //    Console.WriteLine();
            //}

            //Задача.Пользователь вводит размеры прямоугольного массива и диапазон.
            //Программа заполняет массив случайными числами в диапазоне и находит количество простых чисел

            //Random rand = new Random();
            //Console.WriteLine("Enter rows: ");
            //string size = Console.ReadLine();
            //int rows = Convert.ToInt32(size);
            //Console.WriteLine("Enter cols: ");
            //size = Console.ReadLine();
            //int cols = Convert.ToInt32(size);
            //int[,] a = new int[rows, cols];
            //Console.WriteLine("Enter start range: ");
            //size = Console.ReadLine();
            //int start = Convert.ToInt32(size);
            //Console.WriteLine("Enter end range: ");
            //size = Console.ReadLine();
            //int end = Convert.ToInt32(size);
            //for (int i = 0; i < a.GetLength(0); i++)
            //{
            //    for (int k = 0; k < a.GetLength(1); k++)
            //    {
            //        a[i, k] = rand.Next(start, end);
            //    }
            //}
            //int countOfPrime = 0;
            //for (int i = 0; i < a.GetLength(0); i++)
            //{
            //    for (int k = 0; k < a.GetLength(1); k++)
            //    {
            //        Console.Write($"{a[i, k]} ");
            //    }
            //    Console.WriteLine();
            //}

            //for (int i = 0; i < a.GetLength(0); i++)
            //{
            //    for (int k = 0; k < a.GetLength(1); k++)
            //    {
            //        for (int j = 2; j < a[i,k] / 2; j++)
            //        {
            //            if (a[i, k] % j == 0 && a[i, k] != j)
            //            {
            //                a[i, k] = 0;
            //                break;
            //            }
            //        }
            //        if (a[i, k] > 2)
            //        {
            //            countOfPrime++;
            //        }
            //    }
            //}
            //Console.WriteLine($"count = {countOfPrime} ");

            int[][] a = new int[5][];
            for (int i = 0; i < 5; i++)
            {
                a[i] = new int[i + 1];
                for (int k = 0; k < i + 1; k++)
                {
                    a[i][k] = i * k + 1;
                }
            }

            for (int i = 0; i < a.Length; i++)
            {
                for (int k = 0; k < a[i].Length; k++)
                {
                    Console.Write($"{a[i][k]} ");
                }
                Console.WriteLine();
            }
        }
    }
}