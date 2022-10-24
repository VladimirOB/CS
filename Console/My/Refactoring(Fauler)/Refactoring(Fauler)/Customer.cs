using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_Before
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
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            string result = "Учет аренды для " + getName() + '\n';
            foreach (var each in _rentals)
            {
                double thisAmount = 0;

                // определить сумму для каждой строки
                switch (each.getMovie().getPriceCode())
                {
                    case Movie.REGUAL:
                        thisAmount += 2;
                        if (each.getDaysRented() > 2)
                            thisAmount += (each.getDaysRented() - 2 * 1.5);
                        break;

                    case Movie.NEW_RELEASE:
                        thisAmount += each.getDaysRented() * 3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (each.getDaysRented() > 3)
                            thisAmount += (each.getDaysRented() - 3) * 1.5;
                        break;
                }
                //добавить очки для активного арендатор
                frequentRenterPoints++;
                //бонус за аренду новинки на два и более дня
                if ((each.getMovie().getPriceCode() == Movie.NEW_RELEASE) &&
                   each.getDaysRented() > 1) frequentRenterPoints++;
                totalAmount += thisAmount;
            }
                
                //показать результаты
                result += "Сумма задолженности составляет " + totalAmount + "\n";
                result += "Вы заработали " + frequentRenterPoints + " очков за активность.";
                return result;

        }
    }
}

