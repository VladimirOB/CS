using System.Text;

namespace CS_String
{
    internal class Program
    {

        static void Calc()
        {
            //Задача
            //Пользователь вводит строку вида 123 + 2344.Программа вычисляет результат выражения
            int result = 0, first = 0, flag = 0;
            string[] res;

            while (true)
            {
                Console.WriteLine("Enter something. (Exit - 0)");
                string num = Console.ReadLine();
                if (num.Contains("+")) flag = 1;
                if (num.Contains("-")) flag = 2;
                if (num.Contains("*")) flag = 3;
                if (num.Contains("/")) flag = 4;
                if (num.Contains("0")) flag = 0;
                switch (flag)
                {
                    case 1:
                        {
                            res = num.Split("+");
                            first = Convert.ToInt32(res[0]);
                            result = Convert.ToInt32(res[1]);
                            result += first;
                            break;
                        }
                    case 2:
                        {
                            res = num.Split("-");
                            first = Convert.ToInt32(res[0]);
                            result = Convert.ToInt32(res[1]);
                            result = first - result;
                            break;
                        }
                    case 3:
                        {
                            res = num.Split("*");
                            first = Convert.ToInt32(res[0]);
                            result = Convert.ToInt32(res[1]);
                            result *= first;
                            break;
                        }
                    case 4:
                        {
                            res = num.Split("/");
                            first = Convert.ToInt32(res[0]);
                            result = Convert.ToInt32(res[1]);
                            result = first / result;
                            break;
                        }
                    case 0:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        Console.WriteLine("Error");
                        break;

                }
                Console.WriteLine($"result = {result} ");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void Builder()
        {
            //string str = "Hello";
            //str = str.Insert(3, "!!!");
            //str = str + "???";
            //Console.WriteLine(str);

            //StringBuilder builder = new StringBuilder("Hello");
            //builder.Insert(0, "_");
            //builder.Remove(0, 1);
            //builder.Append("!!!");
            //builder.Replace("e", "a");

            //Console.WriteLine(builder.ToString());

            //string[] words = "Hello big world !!!".Split("l");
            //foreach (string word in words)
            //    Console.WriteLine(word);

            //string res = string.Join(".", words);
            //Console.WriteLine(res);
        }

        static void Palindrome()
        {
            //Пользователь вводит строку.Программа проверяет, является ли строка палиндромом
            //string str = "а роза упала на лапу азора";
            //bool flag = false;
            //while (str.Contains(" ")) { str = str.Replace(" ", ""); }
            //for (int i = 0; i < str.Length; i++)
            //{
            //    if (!str[i].Equals(str[str.Length - i - 1]))
            //    {
            //        flag = true;
            //        break;
            //    }
            //    else
            //        continue;
            //}
            //if(flag)
            //    Console.WriteLine("No!");
            //else
            //    Console.WriteLine("Yes!");
        }

        static void Main(string[] args)
        {
            String str = new String("He23llo  11 wo23rld");
            str.Print();
            str.RemoveDig();
            //str.Set("wwwwww ahaha");
            //str.Load();
            str.Print();

           


        }
    }
}