using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace _DoubleLinkedList
{
    class Program
    {
        public const string CppFunctionsDLL = @"..\..\..\..\x64\Debug\CppFunction.dll"; // вернулись назад к основной папке и зашли в дебаг
        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddNumbers(int a, int b);

        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Add(char ch);

        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern char Pop();

        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Print();


        public static void CPPCode()
        {
            //int input1, input2;
            //Console.WriteLine("Input number 1: ");
            //if (!int.TryParse(Console.ReadLine(), out input1))
            //{
            //    Console.WriteLine("Invalid input! input1 = 5");
            //    input1 = 5;
            //}
            //Console.WriteLine("Input number 2: ");
            //if (!int.TryParse(Console.ReadLine(), out input2))
            //{
            //    Console.WriteLine("Invalid input! input2 = 7");
            //    input1 = 7;
            //}

            //int output = AddNumbers(input1, input2);

            //Console.WriteLine($"Output is {output}");
            //Console.ReadKey();

            char ch = '1';
            
            Add(ch);
            ch = '2';
            Add(ch);
            Print();
        }

        static void Main()
        {

            CPPCode();

            //DoubleLinkedList<string> dll = new DoubleLinkedList<string>();
            //dll.Add("Hello");
            //dll.Add("Vladimir");
            //dll.Insert_Index("Balaban", 1);
            //dll.Print();
            //try
            //{
            //    BinaryFormatter bf = new BinaryFormatter();
            //    using (FileStream fs = new FileStream("dll.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            //    {
            //        bf.Serialize(fs, dll);
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine(ex.Message);
            //}
            //Console.WriteLine();
            //try
            //{
            //    FileStream fs2 = File.OpenRead("dll.dat");
            //    BinaryFormatter bf2 = new BinaryFormatter();
            //    DoubleLinkedList<string> dll2 = (DoubleLinkedList<string>)bf2.Deserialize(fs2);
            //    fs2.Close();
            //    dll2.PrintBack();
            //}
            //catch(Exception ex)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine(ex.Message);
            //}
          
        }
    }
}