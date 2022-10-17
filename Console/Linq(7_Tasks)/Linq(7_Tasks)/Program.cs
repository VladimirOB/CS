using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        public static string Solve(string str) => string.Join("", str.Select(x => char.IsDigit(x) ? x : ' ').ToArray()).Split(' ').OrderByDescending(x=>x.Length).First(); // 4
      
        public static string ReverseWords(string str) => string.Join(" ",string.Join("", str.Reverse()).Split(' ').Reverse()); // 5

        public static string MakeString(string str) => string.Join("",str.Split(' ').Select(a => a.First())); //6

        public static string Capitals(string str) => string.Join(" ", from ch in str where (char.IsUpper(ch)) select str.IndexOf(ch)); // 7

        static void Main()
        {
            Console.WriteLine($"Task 1 = {NumberOfOccurrences(new int[] { 1, 0, 2, 2, 3 }, 0)}");
            Console.WriteLine($"Task 2 = {GetUnique(new double[] { 1, 1, 0.1, 1, 1 })}");
            Console.WriteLine($"Task 3 = {RemoveDuplicateWords("Hello big world big Hello")}");
            Console.WriteLine($"Task 4 = {Solve("000012hello987big89world")}");
            Console.WriteLine($"Task 5 = {ReverseWords("Hello big world")}");
            Console.WriteLine($"Task 6 = {MakeString("Miru Mojno Mir")}");
            Console.WriteLine($"Task 7 = {Capitals("Hello World World")}");
        }
    }
}

namespace CS_Linq_Solutions
{
    class Program
    {
        // 1. Функция принимает массив чисел и возвращает исходный массив, но без нечётных чисел
        public static int[] NoOdds(int[] values)
        {
            return (
              from v in values
              where (v % 2 == 0)
              select v
            ).ToArray();
        }

        public static int[] NoOdds2(int[] values) => values.Where(v => v % 2 == 0).ToArray();

        // 2. Функция возвращает количество вхождений элемента в заданном массиве чисел.
        //    var arr = { 1, 0, 2, 2, 3 };
        //    NumberOfOccurrences(0, arr) == 1;
        public static int NumberOfOccurrences(int x, int[] xs) => xs.Count(Item => Item == x);

        public static int NumberOfOccurrences2(int x, int[] xs) => (from int i in xs
                                                                    where i == x
                                                                    select i).Count();

        // 3. Функция принимает массив, который содержит повторяющиеся числа. Только одно число в 
        //    массиве не повторяется. Функция возвращает это число
        //    findUniq([1, 1, 1, 2, 1, 1]) -> 2
        //    findUniq([0, 2, 0.1, 2, 0]) -> 0.1

        public static int GetUnique(IEnumerable<int> numbers)
        {
            return numbers.GroupBy(s => s)
            .Where(x => x.Count() == 1)
            .Select(x => x.Key)
            .FirstOrDefault();
        }

        public static int GetUnique2(IEnumerable<int> numbers)
        {
            return numbers.GroupBy(x => x).Single(x => x.Count() == 1).Key;
        }

        public static int GetUnique3(IEnumerable<int> numbers)
        {
            return numbers.OrderBy(x => numbers.Where(y => y == x).Count()).First();
        }

        // 4. Строка состоит из слов, которые разделены пробелами и могут повторяться. Функция принимает строку
        //    и удаляет в ней все повторяющиеся слова, оставляя их в одном экземпляре в том месте, где они первый раз встретились.
        public static string RemoveDuplicateWords(string s) => String.Join(" ", s.Split(' ').Distinct());

        public static string RemoveDuplicateWords2(string s) => string.Join(" ", s.Split(' ').GroupBy(x => x).Select(x => x.Key));

        // 5. Функция принимает строку, которая содержит буквы и цифры и возвращает число, которое состоит
        //    из максимального количества цифр, идущих подряд в строке

        public static int Solve(string s)
        {
            return Int32.Parse(Regex.Split(s, "[a-z]+")
                        .Where(e => e != string.Empty)
                        .OrderByDescending(x => x.Length).First());
        }

        // 6. Функция возвращает количество гласных букв в переданной строке
        public static int GetVowelCount(string str)
        {
            return str.Count(i => "aeiouy".Contains(i));
        }

        public static int GetVowelCount2(string str)
        {
            return str.ToLower().Count(c => "aeiouy".IndexOf(c) != -1);
        }

        // 7. Функция принимает строку, содержащую слова, разделённые пробелами, производит реверс каждого
        // слова, объединяет их в результирующую строку и возвращает эту строку
        // ReverseWords("Hello Big World") -> "olleH giB dlroW"

        public static string ReverseWords(string str)
            => string.Join(" ", str.Split().Select(x => string.Concat(x.Reverse())));

        public static string ReverseWords2(string str)
        {
            return string.Join(" ", str.Split(' ').Select(i => new string(i.Reverse().ToArray())));
        }

        // 8. Функция принимает строку и возвращает строку, состоящую из первых букв каждого слова исходной строки
        // MakeString("Hello Big World") -> "HBW"
        public static string MakeString(string s)
        {
            return string.Join("", s.Split(' ').Select(x => x[0]));
        }

        // 9. Функция принимает строку и возвращает отсортированный массив индексов заглавных букв
        // Capitals("Hello World") -> { 0, 6 }

        /*public static int[] Capitals(string word)
        {
            return word.Select((x, n) => n).Where((x, i) => char.IsUpper(word, i)).ToArray();
        }*/

        public static int[] Capitals(string str)
            => str.Select((s, i) => i).Where(i => Char.IsUpper(str[i])).ToArray();

        // 10. Функция возвращает индекс минимального элемента входного массива
        public static int FindSmallest(int[] numbers)
        {
            return Array.IndexOf(numbers, numbers.Min());
        }

        static void Main(string[] args)
        {
            var res = Capitals("Hello World World World");
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }

            //Console.WriteLine(Solve("qwe000123erety1234zx"));

        }
    }
}