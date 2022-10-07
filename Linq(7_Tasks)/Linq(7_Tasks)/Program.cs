﻿using System.Collections;
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


        /* Функция принимает строку, которая содержит буквы и цифры и возвращает число, которое состоит
           из максимального количества цифр, идущих подряд в строке
           Solve("12hello987big89world") -> 987 */

        public static int NumberOfOccurrences(int[] arr, int n) => arr.Count(number => n.ToString().Contains(number.ToString()));

        public static int GetUnique(int[] arr) => arr.GroupBy(x => x).Where(g => g.Count() < 2).Select(y => y.Key).First();

        public static string RemoveDuplicateWords(string str) => string.Join(' ', str.Split(' ').Distinct());

        public static int Solve(string str)
        {
            var result = str.TakeWhile(p => p > '0' && p <'9');
            foreach (var item in result)
            {
                Console.Write(item);
            }
            return 0;

        }

        static void Main(string[] args)
        {
            //Console.WriteLine($"Task 1 = " + NumberOfOccurrences(new int[] { 1, 0, 2, 2, 3 }, 0));
            //Console.WriteLine($"Task 2 = " + GetUnique(new int[] { 1, 1, 2, 1, 1 }));
            //Console.WriteLine($"Task 3 = " + RemoveDuplicateWords("Hello big world big Hello"));
            Console.WriteLine($"Task 4 = " + Solve("12hello987big89world"));
        }
    }
}