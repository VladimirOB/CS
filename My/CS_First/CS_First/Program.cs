namespace CS_First
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Command line arguments: ");

            //int index = 0;
            //foreach (var param in args)
            //{
            //    Console.WriteLine($"{index++}. {param}");
            //}

            //Console.WriteLine("Tell me your name, please: ");
            //string? name = Console.ReadLine();
            //Console.WriteLine($"Hello, {name}!");

            string? str1 = Console.ReadLine();
            string? str2 = Console.ReadLine();

            int a = Convert.ToInt32(str1);
            int b = 0;
            if(Int32.TryParse(str2, out b))
            {
                Console.WriteLine(a + b);
            }
            else
            {
                Console.WriteLine("Error");
            }

            
            
        }
    }
}