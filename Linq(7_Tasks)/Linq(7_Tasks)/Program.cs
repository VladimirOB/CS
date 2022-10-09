using System.Collections;
using System.Collections.Generic;

namespace Linq_7_Tasks_
{
    class Program
    {
        /*1. Функция возвращает количество вхождений элемента в заданном массиве чисел.
           var arr = { 1, 0, 2, 2, 3 };
           NumberOfOccurrences(arr, 0) -> 1

        2. Функция принимает массив, который содержит повторяющиеся числа. Только одно число в 
           массиве не повторяется. Функция возвращает это число
           GetUnique([1, 1, 1, 2, 1, 1]) -> 2
           GetUnique([0, 2, 0.1, 2, 0]) -> 0.1

        3. Строка состоит из слов, которые разделены пробелами и могут повторяться. Функция принимает строку
           и удаляет в ней все повторяющиеся слова, оставляя их в одном экземпляре в том месте, где они первый раз встретились.
           RemoveDuplicateWords("Hello big world big Hello") -> "Hello big world"    

        4. Функция принимает строку, которая содержит буквы и цифры и возвращает число, которое состоит
           из максимального количества цифр, идущих подряд в строке
           Solve("12hello987big89world") -> 987

        5. Функция принимает строку, содержащую слова, разделённые пробелами, производит реверс каждого
           слова, объединяет их в результирующую строку и возвращает эту строку
           ReverseWords("Hello Big World") -> "olleH giB dlroW"

        6. Функция принимает строку и возвращает строку, состоящую из первых букв каждого слова исходной строки
           MakeString("Miry Mir") -> "MM"

        7. Функция принимает строку и возвращает отсортированный массив индексов заглавных букв
           Capitals("Hello World") -> { 0, 6 }*/

        public static int NumberOfOccurrences(int[] arr, int n) => arr.Count(number => n.ToString().Contains(number.ToString())); // 1

        public static double GetUnique(double[] arr) => arr.GroupBy(x => x).Where(g => g.Count() < 2).Select(y => y.Key).First(); // 2 Single?
       
        public static string RemoveDuplicateWords(string str) => string.Join(' ', str.Split(' ').Distinct()); // 3

        public static char[] Solve(string str) => str.Select(x => char.IsDigit(x) ? x : ' ').ToArray(); // 4

        public static IEnumerable ReverseWords(string str) => string.Join("", str.Reverse()).Split(' ').Reverse(); // 5

        public static IEnumerable<int> Capitals(string str) => from ch in str.ToArray() where (Char.IsUpper(ch)) orderby str select str.IndexOf(ch);
        public static IEnumerable MakeString(string str) => str.Split(' ').Select(a => a.First());

        static void Main(string[] args)
        {
            //Console.WriteLine($"Task 1 = " + NumberOfOccurrences(new int[] { 1, 0, 2, 2, 3 }, 0));
            //Console.WriteLine($"Task 2 = " + GetUnique(new double[] { 1, 1, 0.1, 1, 1 }));
            //Console.WriteLine($"Task 3 = " + RemoveDuplicateWords("Hello big world big Hello"));

            //Console.Write("Task 4 = ");
            //string str4 = new string(Solve("12hello987big89world"));
            //string[] str44 = str4.Split(' ');
            //Console.WriteLine(str44.Max(a=>a));

            //Console.Write("Task 5 = ");
            //var str5 = ReverseWords("Hello big world");
            //foreach (var item in str5)
            //{
            //    Console.Write(item + " ");
            //}

            //var makeString = MakeString("Miru Mir"); Console.Write("\nTask 6 = "); foreach (var item in makeString) { Console.Write(item); } Console.WriteLine();
            //var arr = Capitals("Hello World"); Console.Write("Task 7 = "); foreach (var item in arr) { Console.Write(item + " "); }
        }
    }
}