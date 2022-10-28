using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_Fauler
{
    /*Customer – класс, представляющий клиента магазина.
     * Как и предыдущие классы,
     * он содержит данные и методы для доступа к ним:*/
    class Customer
    {
        private string _name;
        private List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            _name = name;
        }

        public void addRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string getName()
        {
            return _name;
        }

        public string statement()
        {
            string result = "Учет аренды для " + getName() + '\n';
            foreach (var each in _rentals)
            {
                //показать результаты для этой аренды
                result += "\t" + each.getMovie().getTitle() + " " + each.getChange() + "\n"; // each.getChange - "Замена временной переменной вызовом метода"(Replace Temp with Query, 133)
            }
            //показать результаты
            result += "Сумма задолженности составляет " + getTotalChange() + "\n";
            result += "Вы заработали " + getTotalFrequentRenterPoints() + " очков за активность.";
            return result;
        }

        //добавить очки для активного арендатора
        private int getTotalFrequentRenterPoints()
        {
            int result = 0;
            foreach (var each in _rentals)
            {
                result += each.getFrequentRenterPoints(); //Выделение начисления бонусов в метод (Extract Method, 124)
            }
            return result;
        }

        private double getTotalChange() // замена локальной переменной вызовом метода (Replace Temp with Query, 133)
        {
            double result = 0;
            foreach (var each in _rentals)
            {
                result += each.getChange();
            }
            return result;
        }
    }
}

