using System.ComponentModel;
using System;
using System.Text;
using System.Xml.Linq;
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

            if(smileys.Length == 0)
            return 0;
            else
            {
                int cnt = 0;
                for (int i = 0; i < smileys.Length; i++)
                {
                    string temp = smileys[i];
                    if(temp.Length == 2)
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
        }
    }
}