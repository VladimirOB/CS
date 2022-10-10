using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoUpdate
{
    class SalesProspect<T,T2,T3>
    {
        private T _name;
        private T2 _phone;
        private T3 _budget;

        public T Name
        {
            get { return _name; }
            set { _name = value; Console.WriteLine("Name: " + _name); ; }
        }

        public T2 Phone
        {
            get { return _phone; }
            set { _phone = value; Console.WriteLine("Phone: " + _phone); ; }
        }

        public T3 Budget
        {
            get { return _budget; }
            set { _budget = value; Console.WriteLine($"Budget: {_budget}"); }
        }

        //Выполнение back - up
        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state\n");
            return new Memento(_name, _phone, _budget);
        }

        //Восстановление данных из back - up
        public void RestoreMemento(Memento memento)
        {
            Console.WriteLine("\nRestoring state\n");
            Name = (T)memento.Name;
            Phone = (T2)memento.Phone;
            Budget = (T3)memento.Budget;
        }

    }
}
