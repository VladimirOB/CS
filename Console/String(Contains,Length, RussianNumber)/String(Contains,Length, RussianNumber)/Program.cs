using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace String_Contains_Length__RussianNumber_
{
    internal class Program
    {

        public class Set<T>
        {
            private List<T> _items = new List<T>();
            public int Count => _items.Count; // кол-во эл.

            public void Add(T item)
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (!_items.Contains(item))
                {
                    _items.Add(item);
                }
            }

            public void Remove(T item)
            {
                // Проверяем входные данные на пустоту.
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (!_items.Contains(item))
                {
                    throw new KeyNotFoundException($"Element {item} not found!");
                }
                _items.Remove(item); // удаляем
            }
            public void Print()
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    Console.Write($"{_items[i]} ");
                }
            }
        }
        static void Task1()
        {
            /*1.Пользователь вводит строку, программа удаляет из неё повторяющиеся слова*/
            Set<string> set = new Set<string>();
            string? str = Console.ReadLine();
            if (str != null)
            {
                str = str.Replace(",", "");
                str = str.Replace(".", "");
                string[] str2 = str.Split(" ");
                for (int i = 0; i < str2.Length; i++)
                {
                    set.Add(str2[i]);
                }
            }
            set.Print();
        }

        static void Task2()
        {
            /*2. Пользователь вводит количество строк и сами строки, программа проверяет, все ли строки идут в порядке возрастания длины*/

            Console.Write("Enter numbers of string: ");
            string? temp = Console.ReadLine();
            int size;
            bool flag = false;
            if (Int32.TryParse(temp, out size))
            {
                string[][] str = new string[size][];
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine($"Enter string {i + 1}/{size}:");
                    temp = Console.ReadLine();
                    str[i] = new string[temp.Length];
                    for (int k = 0; k < temp.Length; k++)
                    {
                        str[i][k] = temp[k].ToString();
                    }
                }
                if (str.Length < 2)
                {
                    Console.WriteLine("Error, at least 2 lines are required for validation.");
                    flag = true;
                }
                if (!flag)
                    for (int i = 0; i < str.Length; i++)
                    {
                        for (int k = i + 1; k < str.Length; k++)
                        {
                            if (str[i].Length == 0)
                            {
                                flag = true;
                                break;
                            }
                            if (str[i].Length > str[k].Length)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                if (flag)
                    Console.WriteLine("No");
                else Console.WriteLine("Yes");
            }
            else
                Console.WriteLine("Error size!");

        }

        public class Number
        {
            public static string Str(string str)
            {
                string[] hunds =
                {
                "", "сто ", "двести ", "триста ", "четыреста ",
                "пятьсот ", "шестьсот ", "семьсот ", "восемьсот ", "девятьсот "
                };
                string[] tens =
                {
                "", "десять ", "двадцать ", "тридцать ", "сорок ", "пятьдесят ",
                "шестьдесят ", "семьдесят ", "восемьдесят ", "девяносто "
                };
                string[] frac20 =
                {
                "", "один ", "два ", "три ", "четыре ", "пять ", "шесть ",
                "семь ", "восемь ", "девять ", "десять ", "одиннадцать ",
                "двенадцать ", "тринадцать ", "четырнадцать ", "пятнадцать ",
                "шестнадцать ", "семнадцать ", "восемнадцать ", "девятнадцать "
                };
                string temp = "";
                StringBuilder t = new StringBuilder();
                int new_val;
                int val;
                if (Int32.TryParse(str, out val))
                {
                    if (str.Length == 6)
                        return "сто тысяч";

                    if (str.Length > 3 && str.Length < 6)
                    {
                        new_val = val % 100000;
                        if (new_val > 1000)
                        {
                            string buffer = new_val.ToString();
                            if (buffer.Length == 4)
                                new_val = Convert.ToInt32(buffer[0] - 48);
                            if (buffer.Length == 5)
                            {
                                buffer = buffer[0].ToString() + buffer[1].ToString();
                                new_val = Convert.ToInt32(buffer);
                            }

                            if (new_val % 100 < 20)
                            {
                                t.Append(frac20[new_val % 100]);
                                t.Append("тысяч ");
                            }
                            else
                            {
                                t.Append(tens[new_val % 100 / 10]);
                                t.Append(frac20[new_val % 10]);
                                t.Append("тысяч ");
                            }
                        }
                    }
                    int num = val % 1000;

                    StringBuilder r = new StringBuilder(hunds[num / 100]);

                    r.Insert(0, t);
                    if (0 == num)
                        return r.ToString();
                    if (num < 0)
                        throw new ArgumentOutOfRangeException("val", "Параметр не может быть отрицательным");
                    if (num % 100 < 20)
                    {
                        r.Append(frac20[num % 100]);
                    }
                    else
                    {
                        r.Append(tens[num % 100 / 10]);
                        r.Append(frac20[num % 10]);
                    }


                    if (r.Length != 0) r.Append(" ");
                    return r.ToString();
                }
                else
                {
                    Console.WriteLine("Error number");
                    return null;
                }
            }
        };

        static void Task3()
        {
            /* 3. Пользователь вводит число, программа пишет его на русском языке (0 - 100000)*/
            Console.Write("Enter number(0 - 100000): ");
            string? str = Console.ReadLine();
            //for (int i = 0; i <= 100000; i+=10000)
            //{
            //    Console.WriteLine($"{(object)Number.Str(i.ToString())}");
            //}
            Console.WriteLine($"{(object)Number.Str(str)}");

        }

        static void Main(string[] args)
        {
            //Task1();
            //Task2();
           Task3();
        }
    }
}