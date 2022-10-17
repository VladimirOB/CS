using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc_Delegates_
{
    class MenuItem
    {
        //название пункта меню
        public string title;

        //буква пункта меню
        public char letter;

        //указатель на метод-обработчик
        public Deleg handler;

        public MenuItem(char letter, string title, Deleg method)
        {
            this.title = title;
            this.letter = letter;
            handler = method;   // переменная типа делегат
        }

        public void Print()
        {
            Console.WriteLine($"{letter}. {title}");
        }
    }
}
