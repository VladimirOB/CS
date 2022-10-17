namespace Recursion
{
    internal class Program
    {

        /*Рекурсивная функция принимает число a и 
            выводит на экран все простые числа в диапазоне[2; a]*/

        static bool Func(int num, int pos, int i)
        {
            if (num == 2)
                return true;
            if (num < 2 || num % 2 == 0)
                return false;
            while(i*i <= num)
            {
                if (num % i == 0)
                    return false;
                return Func(num, pos + 1, i += 2);
            }
            return true;
        }

        static void Main(string[] args)
        {

            //string[] columns = str.Split(' ');
            //Random rnd = new Random();
            //int num = rnd.Next(0, 100); // рандом число в диапазоне от 0 до 100 (если 1 число, оно начальное) последнее число не включительно
            int n = 2, i = 3;
            string num = Console.ReadLine();
            int cnt = Convert.ToInt32(num);
            for (; n <= cnt; n++)
            {
                if(Func(n,0,i))
                    Console.WriteLine($"{n} ");
            }
            
        }
    }
}