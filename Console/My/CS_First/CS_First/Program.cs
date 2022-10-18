namespace CS_First
{
    internal class Program
    {

        public static int Divisors(int n)
        {
            int cnt = 0;
            List<int> v = new List<int>();

            while (n % 2 == 0)
            {
                cnt++;
                n = n / 2;
            }
            if (cnt != 0)
                v.Add(cnt);
            for (int i = 3; i < Math.Sqrt(n); i += 2)
            {
                cnt = 0;
                while (n % i == 0)
                {
                    cnt++;
                    n /= i;
                }
                if (cnt != 0)
                    v.Add(cnt);
            }
            if (n > 1)
            {
                v.Add(1);
            }
            int ret = 1;
            for (int i = 0; i < v.Count; i++)

                ret = ret * (v[i] + 1);
            ret = ret - v.Count;
            return ret;

        }
        static int factorial(int n)
        {
            //len([i for i in range(1, n + 1) if n % i == 0])
            int cnt = 0;       
            for (int i = 1; i < n+1; i++)
            {
                if (n % i == 0)
                    cnt++;
            }
            return cnt;
        }

        static void Main(string[] args)
        {

            //Console.WriteLine(Divisors(30));
            Console.WriteLine(factorial(12));

            //Console.WriteLine("Command line arguments: ");

            //int index = 0;
            //foreach (var param in args)
            //{
            //    Console.WriteLine($"{index++}. {param}");
            //}

            //Console.WriteLine("Tell me your name, please: ");
            //string? name = Console.ReadLine();
            //Console.WriteLine($"Hello, {name}!");

            //string? str1 = Console.ReadLine();
            //string? str2 = Console.ReadLine();

            //int a = Convert.ToInt32(str1);
            //int b = 0;
            //if(Int32.TryParse(str2, out b))
            //{
            //    Console.WriteLine(a + b);
            //}
            //else
            //{
            //    Console.WriteLine("Error");
            //}

            
            
        }
    }
}