using System.ComponentModel;
using System;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Collections;

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

        static void Main()
        {
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

        }
    }
}