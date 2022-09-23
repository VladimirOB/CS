using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc_Delegates_
{
    public delegate void Deleg();

    class Application
    {
        Menu menu = new Menu();
        public static Deleg m1;
        double a;
        double b;
        double result;
        public Application()
        {
            menu.Add('1', "Plus", new Deleg(Plus));
            menu.Add('2', "Minus", Minus);
            m1 = Multiply;
            menu.Add('3', "Multiply", m1);
            m1 = Divide;
            menu.Add('4', "Divide", m1);
            menu.Add('5', "Exit", ()=>Environment.Exit(0));
        }

        
        void DataInput()
        {
            Console.Clear();
            Console.WriteLine("Please, enter a first number: ");
            string temp = Console.ReadLine();
            if (!double.TryParse(temp, out a))
            {
                Console.WriteLine("Invalid input. Enter a number again!");
                Console.ReadKey();
                DataInput();
            }

            Console.WriteLine("Please, enter a second number: ");
            temp = Console.ReadLine();
            if (!double.TryParse(temp, out b))
            {
                Console.WriteLine("Invalid input. Enter a number again!");
                Console.ReadKey();
                DataInput();
            }
        }

        public void Plus()
        {
            DataInput();
            result = a + b;
            Console.WriteLine($"{a} + {b} = {result}");
            Console.ReadKey();
        }

        public void Minus()
        {
            DataInput();
            result = a - b;
            Console.WriteLine($"{a} - {b} = {result}");
            Console.ReadKey();
        }

        public void Multiply()
        {
            DataInput();
            result = a * b;
            Console.WriteLine($"{a} * {b} = {result}");
            Console.ReadKey();
        }
        public void Divide()
        {
            DataInput();
            result = a / b;
            Console.WriteLine($"{a} / {b} = {result}");

            Console.ReadKey();
        }

        public void Run()
        {
            menu.Run();
        }

    }

}
