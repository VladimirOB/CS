﻿using System.ComponentModel;
using System;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Linq;
using System.Numerics;

namespace CodeWars
{
    class MainApp
    {
        public static string Encrypt(string name)
        {
            //замазать строку, оставив последние 4 символа

            StringBuilder sb = new StringBuilder(name);
            for (int i = 0; i < name.Length - 4; i++)
            {
                sb[i] = '#';
            }
            return sb.ToString();
        }

        public static IEnumerable<string> OpenOrSenior(int[][] data)
        {
            //1-е возраст, 2-е очки. если возраст выше 55 и очки больше 7, Senior. Иначе Open

            for (int i = 0; i < data.Length; i++)
            {
                var str = (data[i][0] > 54 && data[i][1] > 7) ? "Senior" : "Open";
                yield return str;
            }

        }

        public static int ShortWord(string str) //=> string.Join(' ', str.Split(' ').OrderByDescending(x => x.Length))
        {
            //Просто, учитывая строку слов, вернуть длину кратчайшего слова(слов).
            string[] str2 = str.Split(' ');
            int min = int.MaxValue;
            for (int i = 0; i < str2.Length; i++)
            {
                if (str2[i].Length < min)
                    min = str2[i].Length;
            }
            return min;
        }

        public static void Dict(string s)
        {
            //частотный словарь букв в строке

            Dictionary<char, int> chars = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (chars.ContainsKey(s[i]))
                {
                    chars[s[i]]++;
                }
                else
                    chars[s[i]] = 1;
            }
            foreach (var item in chars)
            {
                Console.WriteLine(item.Key + " - " + item.Value);
            }
        }

        public static string ROT13(string str) // a = 97, z = 122, n = 110 | A = 65, Z = 90, N = 78
        {
            //шифрование

            char[] chars = str.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] > 96 && chars[i] < 110)
                    chars[i] += (char)13;
                else if (chars[i] > 109 && chars[i] < 123)
                    chars[i] -= (char)13;

                else if (chars[i] > 64 && chars[i] < 78)
                    chars[i] += (char)13;
                else if (chars[i] > 77 && chars[i] < 91)
                    chars[i] -= (char)13;

            }
            return new string(chars);
        }

        public static bool orCompSame(int[] a, int[] b)
        {
            // если элемент b равен любому а в квадрате.

            if (a == null || b == null)
                return false;
            if (a.Length != b.Length || a.Length == 0 || b.Length == 0)
                return false;
            //int cnt = 0;
            //for (int i = 0; i < b.Length; i++)
            //{
            //    for (int k = 0; k < a.Length; k++)
            //    {
            //        if (b[i] == a[k] * a[k])
            //        {
            //            cnt++;
            //            break;
            //        }
            //    }
            //}
            //bool check = b.All(i => a.Contains((int)Math.Sqrt(b)));

            //bool check = a.Where(i => b.Contains(i * i)).Count() == b.Length;
            //bool check = a.All(i => b.Contains(i * i));
            //return check;
            //return !b.Except(a.Select(x => x * x)).Any();
            var aSort = a.Select(x => x * x).OrderBy(x => x);
            var bSort = b.OrderBy(x => x);
            foreach (var item in aSort)
            {
                Console.WriteLine("a = " + item);
            }
            foreach (var item in bSort)
            {
                Console.WriteLine("b = " + item);
            }
            return aSort.SequenceEqual(bSort);
        }

        public static int CountSmileys(string[] smileys)
        {
            //количество улыбок

            if (smileys.Length == 0)
                return 0;
            else
            {
                int cnt = 0;
                for (int i = 0; i < smileys.Length; i++)
                {
                    string temp = smileys[i];
                    if (temp.Length == 2)
                    {
                        if ((temp[0] == ';' || temp[0] == ':') && (temp[1] == ')' || temp[1] == 'D'))
                            cnt++;
                    }
                    if (temp.Length == 3)
                    {
                        if ((temp[0] == ';' || temp[0] == ':') && (temp[1] == '-' || temp[1] == '~') && (temp[2] == 'D' || temp[2] == ')'))
                            cnt++;

                    }
                }// :) :D ;-D :~)
                return cnt;
            }
        }

        public static int[] MoveZeroes(int[] arr)
        {
            // переместить нули в конец массива

            //List<int> lst = new List<int>(arr);
            //List<int> lst2 = new List<int>(arr);
            //foreach (var item in lst)
            //{
            //    if(item.Equals(0))
            //    {
            //        lst2.Remove(item);
            //        lst2.Add(0);
            //    }
            //}
            //return lst2.ToArray();
            return arr.OrderBy(x => x == 0).ToArray();
        }

        public static string CreatePhoneNumber(int[] arr)
        {
            // приходит массив чисел, возвращается номер телефона

            //return $"({arr[0]}{arr[1]}{arr[2]}) {arr[3]}{arr[4]}{arr[5]}-{arr[6]}{arr[7]}{arr[8]}{arr[9]}";
            return long.Parse(string.Concat(arr)).ToString("(000) 000-0000");
        }

        public static int[] DelNRepeat(int[] arr, int num)
        {
            //Учитывая список и число, создайте новый список, который содержит каждое число списка не более N раз без изменения порядка.
            //Например, если входное число равно 2, а входной список — [1,2,3,1,2,1,2,3], вы берете[1, 2, 3, 1, 2], отбрасываете следующий[1, 2], поскольку это приведет к тому, что 1 и 2 будут в результате 3 раза, а затем взять 3, что приведет к[1, 2, 3, 1, 2, 3].
            //Со списком[20, 37, 20, 21] и числом 1 результатом будет[20, 37, 21].

            List<int> result = new List<int>();
            Dictionary<int, int> numberCount = new Dictionary<int, int>();
            foreach (var item in arr)
            {
                if (numberCount.ContainsKey(item))
                    numberCount[item]++;
                else
                    numberCount.Add(item, 1);
                if (numberCount[item] <= num)
                    result.Add(item);
            }
            return result.ToArray();

            //var gg = arr.GroupBy(i => i).Where(x=>x.Count() < num);
            //values = values.Where.values. values.RemoveAll(item => item);
        }

        public static char MissingLetter(string str)
        {
            char[] chars = str.ToCharArray();
            char ch ='0';
            for (int i = 0; i < chars.Length-1; i++)
            {
                if (chars[i] != chars[i + 1] - 1)
                    ch = chars[i]+=(char)1;
            }
            return ch;
            // public static char FindMissingLetter(char[] array) => (char)Enumerable.Range(array[0], 25).First(x => !array.Contains((char)x));
        }

        public static long IP(string start, string end)
        {
            //Count IP Addresses
            //Implement a function that receives two IPv4 addresses, and returns the number of addresses between them(including the first one, excluding the last one).
            //All inputs will be valid IPv4 addresses in the form of strings. The last address will always be greater than the first one.
            //Examples
            //* With input "10.0.0.0", "10.0.0.50"  => return 50
            //* With input "10.0.0.0", "10.0.1.0"   => return 256
            //* With input "20.0.0.10", "20.0.1.0"  => return 246
            string[] str1 = start.Split('.');
            string[] str2 = end.Split('.');
            long[] arr1 = new long[4];
            long[] arr2 = new long[4];
            long[] res = new long[4];
            for (int i = 0; i < str1.Length; i++)
            {
                arr1[i] = Convert.ToInt64(str1[i]);
            }
            for (int i = 0; i < str2.Length; i++)
            {
                arr2[i] = Convert.ToInt64(str2[i]);
            }

            long result = 0;
            if (arr1[0] != arr2[0])
            {
                long t = arr2[0] - arr1[0];
                res[0] = t * 256 * 256 * 256;
                result = res[0];
            }
            if (arr1[1] != arr2[1])
            {
                long t = arr2[1] - arr1[1];
                res[1] = t * 256 * 256;
                result += res[1];
            }
            if (arr1[2] != arr2[2])
            {
                long t = arr2[2] - arr1[2];
                res[2] = t * 256;
                result += res[2];
            }
            if (arr1[3] != arr2[3])
            {
                long t = arr2[3] - arr1[3];
                res[3] = t;
                result += res[3];
            }
            return result;

            //return (long)(uint)IPAddress.NetworkToHostOrder((int)IPAddress.Parse(end).Address) - 
            //(long)(uint)IPAddress.NetworkToHostOrder((int)IPAddress.Parse(start).Address);
        }

        public static bool CheckString(string str)
        {
            if (!string.IsNullOrEmpty(str)
            && str.All(c => char.IsLetterOrDigit(c) && (c < 128)))
            {
                return true;
            }
            return false;
            //public static bool Alphanumeric(string str) => new Regex("^[a-zA-Z0-9]+$").Match(str).Success;
        }

        public static bool IsValidIp(string ipAddres)
        {
            string ip = @"^((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])[\.]){3}(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])$";
            MatchCollection m = Regex.Matches(ipAddres, ip);
            if (m.Count > 0)
            {
                return true;
            }
            return false;
            //public static bool IsValidIp(string ipAddres) =>
            //new Regex("^((2[0-5][0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[1-9]|0)\\.){3}(2[0-5][0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[1-9]|0)$")
            //.IsMatch(ipAddres);
        }

        public static int MaxSubarraySum(int[] a)
        {
            if (a.All(s => s < 0)) // If all array before zero, return zero
                return 0;
            int max_so_far = int.MinValue;
            int max_ending_here = 0;

            for (int i = 0; i < a.Length; i++)
            {
                max_ending_here = max_ending_here + a[i];

                if (max_so_far < max_ending_here)
                    max_so_far = max_ending_here;

                if (max_ending_here < 0)
                    max_ending_here = 0;
            }

            return max_so_far;

            /*int max = 0, res = 0, sum = 0;
            foreach(var item in arr)
            {
                sum += item;
                max = sum > max ? max : sum;
                res = res > sum - max ? res : sum - max;
            }
            return res;*/
        }

        public static int StrCount(string str, string letter) => str.Count(x => x.ToString() == letter);

        public static double[] Tribonacci(double[] signature, int n)
        {
            double[] r = new double[n];
            if (n < 3)
            {
                for (int i = 0; i < n; i++)
                {
                    r[i] = signature[i];
                }
                return r;
            }
            else
            {
                List<double> t = new List<double>();
                for (int i = 0; i < signature.Length; i++)
                {
                    t.Add(signature[i]);
                }

                for (int i = 1; i < n - 2; i++)
                {
                    t.Add(t[i] + t[i + 1] + t[i - 1]);
                }
                return t.ToArray();
            }
            /*double[] res = new double[n];
            Array.Copy(s, res, Math.Min(3, n));
    
            for(int i = 3; i < n; i++)
              res[i] = res[i - 3] + res[i - 2] + res[i - 1];
    
            return n == 0 ? new double[]{0} : res;*/

        }

        public static string StripComments(string text, string[] commentSymbols) // 4 kyu
        {
            //string stripped = StripCommentsSolution.StripComments("яблоки, груши # и бананы\ngrapes\nbananas !apples", new[] { "#", "!" })
            // результат должен == "яблоки, груши\nвиноград\nбананы"

            //return string.Join("", Regex.Replace(text, commentSymbols[1] + ".+", string.Empty).Trim());

            var lines = Regex.Split(text, @"\n");
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                foreach (var comm in commentSymbols)
                {
                    int ind = line.IndexOf(comm);
                    if (ind >= 0)
                    {
                        line = line.Substring(0, ind);
                    }
                }
                lines[i] = line.TrimEnd();
            }
            return string.Join("\n", lines);

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < text.Length; i++)
            //{
            //    char character = text[i];
            //    if (sb.Length > 0)
            //        if (sb[sb.Length - 1] == '\n' && character == ' ')
            //        {
            //            continue;
            //        }

            //    if (commentSymbols.Any(x => x.Contains(character)))
            //    {
            //        // перемещаем i непосредственно перед концом текущей строки
            //        i = text.IndexOf('\n', i) - 1;
            //        // Удаляем пробел, который мы уже добавили в конце
            //        sb.Remove(sb.Length - 1, 1);
            //        // Если в конце последней строки нет символа новой строки
            //        if (i < 0) break;
            //        // Пропустить оставшуюся часть этой итерации
            //        continue;
            //    }
            //    sb.Append(character);
            //}
            //return sb.ToString();

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < text.Length; i++)
            //{

            //    if(commentSymbols.Any(x=> x.Contains(text[i])))
            //    {
            //        sb.Remove(sb.Length-1, 1);
            //        for (int k = text[i]; text[i] != '\n' && i + 1 != text.Length; k++, i++){}
            //        if(i+1 != text.Length)
            //        {
            //            sb.Remove(sb.Length - 1, 1);
            //            sb.Append('\n');
            //        }
            //        continue;
            //    }
            //    sb.Append(text[i]);
            //}
            //Console.WriteLine("len = " + sb.Length);
            //return sb.ToString();
        }

        public static string Add(string a, string b) // 4 kyu
        {
            return (BigInteger.Parse(a) + BigInteger.Parse(b)).ToString();
        }

        public static bool Sudoku(int[][] arr)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (arr[i][j] <= 0 || arr[i][j] > 9)
                        return false;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                bool[] uniq = new bool[10];

                for(int j = 0; j < 9; j++) 
                {
                    int t = arr[i][j]; // проверка columns
                    if (uniq[t] == true) // если 2 раз встретилось число
                    {
                        return false;
                    }
                    uniq[t] = true;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                bool[] uniq = new bool[10];

                for (int j = 0; j < 9; j++)
                {
                    int t = arr[j][i]; // проверка rows
                    if (uniq[t] == true) // если 2 раз встретилось число
                    {
                        return false;
                    }
                    uniq[t] = true;
                }
            }

            //проверка 3х3
            for (int i = 0; i < 9-2; i+=3)
            {
                for (int j = 0; j < 9-2; j+=3)
                {
                    bool[] uniq = new bool[10];
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            int x = i + k;
                            int y = j + l;
                            int t = arr[x][y];

                            if (uniq[t] == true)
                                return false;
                            uniq[t] = true;
                        }
                    }
                }
            }

            return true;

            /*return Enumerable
              .Range(0, 9)
              .SelectMany(i => new[]
              {
                  board[i].Sum(),
                  board.Sum(b => b[i]),
                  board.Skip(3 * (i / 3)).Take(3).SelectMany(r => r.Skip(3 * (i % 3)).Take(3)).Sum()
              })
              .All(i => i == 45);*/
        }
        static void Main()
        {

            //double[] res = Tribonacci(new double[] { 8, 2, 18, }, 2);
            //Assert.AreEqual(new double[] { 1, 1, 1, 3, 5, 9, 17, 31, 57, 105 }, variabonacci.Tribonacci(new double[] { 1, 1, 1 }, 10));
            //Assert.AreEqual(new double[] { 0, 0, 1, 1, 2, 4, 7, 13, 24, 44 }, variabonacci.Tribonacci(new double[] { 0, 0, 1 }, 10));
            //Assert.AreEqual(new double[] { 0, 1, 1, 2, 4, 7, 13, 24, 44, 81 }, variabonacci.Tribonacci(new double[] { 0, 1, 1 }, 10));
            //var str = OpenOrSenior(new[] { new[] { 45, 12 }, new[] { 55, 21 }, new[] { 19, 2 }, new[] { 104, 20 } });
            //Console.WriteLine(Encrypt("5168755456421726"));
            //Console.WriteLine(ShortWord("hello worldik"));
            //Dict("hello world");
            //string str = ROT13("abcdefghijklmnopqrstuvwxyz");
            //Console.WriteLine(orCompSame(new[] { 121, 144, 19, 161, 19, 144, 19, 11 }, new[] { 121, 14641, 20736, 361, 25921, 361, 20736, 361 }));
            //Console.WriteLine(CountSmileys(new[] { ":)", ";(", ";}", ":-D" }));
            //int[] res = MoveZeroes(new[] { 1, 2, 0, 1, 0, 1, 0, 3, 0, 1 });
            //Console.WriteLine(CreatePhoneNumber(new[] {1,2,3,4,5,6,7,8,9,0}));
            //int[] res = DelNRepeat(new[] { 1, 2, 3, 1, 2, 1, 2, 3 }, 2);
            //char ch = MissingLetter("ac");
            //Console.WriteLine(IP("10.0.0.0", "10.0.2.0"));
            //Console.WriteLine(CheckString("PassW0rd"));
            //Console.WriteLine(IsValidIp("1.1.1.1"));
            //Console.WriteLine(MaxSubarraySum(new[] { -5, 0, -1, -4 }));
            //Console.WriteLine(StrCount("Hello", "o"));
            //Console.WriteLine(StripComments("яблоки, груши # и бананы\ngrapes\nbananas !apples", new[] { "#", "!" }));
            //Console.WriteLine(Add("1111111111111111111111", "1111111111111111111111"));
        }
    }
}