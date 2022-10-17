using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc_Delegates_
{
    class Menu
    {
        List<MenuItem> items = new List<MenuItem>();

        public void Add(char letter, string title, Deleg method)
        {
            MenuItem item = new MenuItem(letter, title, method);
            items.Add(item);
        }

        public void Print()
        {
            foreach (var item in items)
            {
               item.Print();
            }
        }


        public void Run()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Choose an operation:");
                Print();
                var input = Console.ReadKey();
                char letter = input.KeyChar;
                foreach (MenuItem current in items)
                {
                    if(current.letter.Equals(letter))
                    {
                        // обработчик нажатия текущего пункта меню
                        Deleg m1 = current.handler;
                        // запуск метода класса Application, на который указывает указатель handler
                        m1();
                        break;
                    }
                }

            }
        }
    }
}
