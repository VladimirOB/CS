using System.Linq;
using System.Text;

namespace Array__2DArray
{
    internal class Program
    {  
       
        static void Task1()
        {
            /*1. Пользователь вводит размер и сам массив 
         Программа меняет местами минимальный и максимальный эл*/
            Console.WriteLine("Enter size of array: ");
            string temp = Console.ReadLine();
            int size;
            int min = 0, max = 0, numMin =0, numMax = 0;
            if (Int32.TryParse(temp, out size))
            {
                if(size > 0)
                {
                    int[] arr = new int[size];
                    Console.WriteLine("Enter array numbers: ");
                    for (int i = 0; i < size; i++)
                    {
                        Console.Write($"{i + 1}/{size} = ");
                        temp = Console.ReadLine();
                        min = max = arr[0];
                        if (Int32.TryParse(temp, out arr[i]))
                        {
                            if (arr[i] < min)
                            {
                                numMin = i;
                                min = arr[i];
                            }
                            if (arr[i] > max)
                            {
                                numMax = i;
                                max = arr[i];
                            }
                            continue;
                        }
                        else i--;
                    }
                    int t = arr[numMin];
                    arr[numMin] = arr[numMax];
                    arr[numMax] = t;
                    Console.WriteLine("");
                    foreach (var item in arr)
                    {
                        Console.Write($"{item} ");
                    }
                }
                else Console.WriteLine("Invalid size!");
            }
            else Console.WriteLine("Invalid input!");

        }

        static void Task2()
        {
            /*2.Пользователь вводит размеры и сам двумерный прямоугольный массив.
              Программа выводит на экран те элементы массива,
              которые слева, справа, сверху и снизу
              окружены числами большими, чем сам элемент*/

            int rows, cols, cnt = 1, count = 0;
            Console.WriteLine("Enter rows: ");
            string temp = Console.ReadLine();
            Int32.TryParse(temp, out rows);
            Console.WriteLine("Enter cols: ");
            temp = Console.ReadLine();
            Int32.TryParse(temp, out cols);
            if(rows == 0 || cols == 0)
            {
                Console.WriteLine("Invalid input rows or cols");
                return;
            }
            int[] result = new int[rows * cols];
            int[,] arr = new int[rows, cols];
            Console.WriteLine("Enter array numbers: ");

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int k = 0; k < arr.GetLength(1); k++)
                {
                    Console.Write($"{cnt++}/{rows*cols} = ");
                    temp = Console.ReadLine();
                    if (Int32.TryParse(temp, out arr[i, k]))
                    {
                        continue;
                    }
                    else { k--; cnt--; }
                }
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int k = 0; k < arr.GetLength(1); k++)
                {
                    Console.Write($"{arr[i, k]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Result: ");

            for (int i = 1; i < arr.GetLength(0)-1; i++)
            {
                for (int k = 1; k < arr.GetLength(1)-1; k++)
                {
                    if (arr[i, k] < arr[i - 1, k] && arr[i, k] < arr[i, k - 1] && arr[i, k] < arr[i + 1, k] && arr[i, k] < arr[i, k + 1])
                        result[count++] = arr[i, k];

                }
            }
            for (int i = 0; i < count; i++)
            {
                Console.Write($"{result[i]} ");
            }
        }

        static void Task3()
        {
            /*3.Создать и вывести на экран такие массивы(пользователь указывает высоту):

         1           1 2 3 4 5       1
       1 2           1 2 3 4       1 2 3
     1 2 3           1 2 3       1 2 3 4 5
   1 2 3 4           1 2       1 2 3 4 5 6 7
 1 2 3 4 5           1                       */

            Console.WriteLine("Enter height of array: ");
            string temp = Console.ReadLine();
            int size;
            if(Int32.TryParse(temp, out size))
            {
                int[][] arr = new int[size][];
                Task3_s1(arr, size);
                Task3_s2(arr, size);
                Task3_s3(arr, size);
            }
            else
                Console.WriteLine("Wrong input");
        }



        static void Task3_s1(int[][] arr, int size)
        {
            Console.WriteLine("First: ");
            for (int i = 0; i < size; i++)
            {
                arr[i] = new int[i + 1];
                for (int k = 0; k < i + 1; k++)
                {
                    arr[i][k] = k + 1;
                }
            }
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j < arr.Length; j++)
                {
                    Console.Write("  ");
                }
                for (int k = 0; k < arr[i].Length; k++)
                {
                    
                    Console.Write($"{arr[i][k]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static void Task3_s2(int[][] arr, int size)
        {
            Console.WriteLine("Second: ");
            for (int i = 0; i < size; i++)
            {
                arr[i] = new int[size - i];
                for (int k = 0; k < size - i; k++)
                {
                    arr[i][k] = k + 1;
                }
            }
            for (int i = 0; i < arr.Length; i++)
            {
                for (int k = 0; k < arr[i].Length; k++)
                {

                    Console.Write($"{arr[i][k]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Task3_s3(int[][] arr, int size)
        {
            Console.WriteLine("Third: ");
            int num = 0;
            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    arr[i] = new int[i + 1];
                    for (int k = 0; k < i + 1; k++)
                    {
                        arr[i][k] = k + 1;
                    }
                }
                else
                {
                    arr[i] = new int[i + 2 + num];
                    for (int k = 0; k < i + 2 + num; k++)
                    {
                        arr[i][k] = k + 1;
                    }
                    num ++;
                }
                
            }
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j < arr[arr.Length-1].Length; j++)
                {
                    Console.Write("  ");
                }
                for (int k = 0; k < arr[i].Length; k++)
                {
                    Console.Write($"{arr[i][k]} ");
                }
                Console.WriteLine();
            }

        }

        public static string Encrypt(string name)
        {
            StringBuilder sb = new StringBuilder(name);
            for (int i = 0; i < name.Length - 4; i++)
            {
                sb[i] = '#';
            }
            return sb.ToString();
        }

        public static IEnumerable<string> OpenOrSenior(int[][] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var str =  (data[i][0] > 54 && data[i][1] > 7) ? "Senior" : "Open";
                yield return str;
            }

        }

        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            //Task3();

            //var str = OpenOrSenior(new[] { new[] { 45, 12 }, new[] { 55, 21 }, new[] { 19, 2 }, new[] { 104, 20 } });
            //Console.WriteLine(Encrypt("5168755456421726"));
        }
    }
}