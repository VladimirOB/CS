using System.Text;
using System.Text.RegularExpressions;

namespace RegExp_string_calc_
{
    internal class Program
    {

        static void Task1()
        {
            /*1. Программа просит пользователя ввести строку и проверяет является ли введенная строка правильным:
            - номером телефона (xxx) xxx-xx-xx
            - датой xx августа xxxx
            - числом типа double -xxxx.xx, +xxx, xxx, xxxx.xx
            - IP-адресом 213.123.0.45*/
            //string[] months = { "января", "февраля"};

            
            string date = @"^([12]\d|[0][1-9]|[3][01])\s*(января|февраля|марта|апреля|мая|июня|июля|августа|сентября|октября|ноября|декабря)\s*([12]\d{3}|[1-9]\d{2}|[1-9]\d|[1-9])$";
            string PhoneNumber = @"^(\(\d{3}\))\s*(\d{3})-\d{2}-\d{2}$";
            string doub = @"^(\d+\.\d+|[+-]\d+\.\d+|\.\d+|[+-]\d+|\d+)$";
            string ip = @"^((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])[\.]){3}(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])$";
            string[] check = { date, PhoneNumber, doub, ip };
            while(true)
            {
                string? str = Console.ReadLine();
                if (str == "exit")
                    break;
                for (int i = 0; i < check.Length; i++)
                {
                    if(str != null)
                    {
                        MatchCollection m = Regex.Matches(str, check[i]);
                        if (m.Count > 0)
                        {
                            Console.WriteLine("Yes. Count: {0}", m.Count);
                            foreach (Match match in m)
                            {
                                Console.WriteLine("Value: {0}, Start: {1}, Length: {2}", match.Value, match.Index, match.Length);
                            }
                        }
                    }
                   
                }
            }
            
        }

        static void Task2()
        {
            /*2. Пользователь вводит имя файла, в котором встречаются примеры вида 
             * /d+ [+-/*] /d+
             Программа вычисляет примеры и записывает их ответы на старые места. 
            Результат записывается в другой файл.*/
            try
            {
                string str = File.ReadAllText("V:/temp/dz.txt");
                StringBuilder sb = new StringBuilder();
                sb.Append(str);
                string[] temp;
                double x, y, result;
                int stack = 0;
                MatchCollection m = Regex.Matches(str, @"(([\d]+\s*|-[\d]+\s*)(\+{1}|\-{1}|\*{1}|/{1})(\s*[\d]+))");
                if (m.Count > 0)
                {
                   
                    Console.WriteLine("Yes. Count: {0}", m.Count);
                    foreach (Match match in m)
                    {
                        Console.WriteLine("Value: {0}, Start: {1}, Length: {2}", match.Value, match.Index, match.Length);

                        if (match.Value.Contains('+'))
                        {
                            temp = match.Value.Split('+');
                            x = Convert.ToInt32(temp[0]);
                            y = Convert.ToInt32(temp[1]);
                            result = Calculate(x, y, Operation.Add);
                            Console.WriteLine($"Res = {result}");
                            sb.Remove(match.Index - stack, match.Length);
                            sb.Insert(match.Index - stack, result.ToString());
                            stack += match.Length - result.ToString().Length;
                        }
                        if (match.Value.Contains('-'))
                        {
                            temp = match.Value.Split('-');
                            x = Convert.ToInt32(temp[0]);
                            y = Convert.ToInt32(temp[1]);
                            result = Calculate(x, y, Operation.Subtract);
                            Console.WriteLine($"Res = {result}");
                            sb.Remove(match.Index - stack, match.Length);
                            sb.Insert(match.Index - stack, result.ToString());
                            stack += match.Length - result.ToString().Length;
                        }
                        if (match.Value.Contains('*'))
                        {
                            temp = match.Value.Split('*');
                            x = Convert.ToInt32(temp[0]);
                            y = Convert.ToInt32(temp[1]);
                            result = Calculate(x, y, Operation.Multiply);
                            Console.WriteLine($"Res = {result}");
                            sb.Remove(match.Index - stack, match.Length);
                            sb.Insert(match.Index - stack, result.ToString());
                            stack += match.Length - result.ToString().Length;
                        }
                        if (match.Value.Contains('/'))
                        {
                            temp = match.Value.Split('/');
                            x = Convert.ToInt32(temp[0]);
                            y = Convert.ToInt32(temp[1]);
                            result = Calculate(x, y, Operation.Divide);
                            if(result == -1)
                            {
                                sb.Remove(match.Index - stack, match.Length);
                                stack += match.Length;
                            }
                            else
                            {
                                Console.WriteLine($"Res = {result}");
                                sb.Remove(match.Index - stack, match.Length);
                                sb.Insert(match.Index - stack, result.ToString());
                                stack += match.Length - result.ToString().Length;
                            }
                           
                        }

                    }
                }
                else Console.WriteLine("No");
                File.WriteAllText("V:/temp/result.txt", sb.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        enum Operation
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }

        static double Calculate(double x, double y, Operation op)
        {
            switch (op)
            {
                case Operation.Add:
                    return x + y;
                case Operation.Subtract:
                    return x - y;
                case Operation.Multiply:
                    return x * y;
                case Operation.Divide:
                    {
                        if(x == 0 || y == 0)
                            return -1;
                        return x / y;
                    }
                    
                default:
                    return 1;
            }
        }

        static void Main(string[] args)
        {
            Task1();
            //Task2();
        }
    }
}