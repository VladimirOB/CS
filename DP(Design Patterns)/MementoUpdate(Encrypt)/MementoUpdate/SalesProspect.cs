using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoUpdate
{
    class SalesProspect
    {
        private string? _name;
        private string? _phone;
        private double _budget;
        public string Name
        {
            get { return _name; }
            set { _name = value;}
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value;}
        }

        public double Budget
        {
            get { return _budget; }
            set { _budget = value;}
        }

        //Выполнение back - up
        public Memento SaveMemento()
        {
            Memento newMemento = new Memento();
            newMemento.Add("name", Name);
            newMemento.Add("phone", Phone);
            newMemento.Add("budget", Budget);
            return newMemento;
        }

        //Восстановление данных из back - up
        public void RestoreMemento(Memento memento)
        {
            if(memento != null)
            {
                Console.WriteLine("\nRestoring state\n");
                Name = (string)memento.dict["name"];
                Phone = (string)memento.dict["phone"];
                Budget = Convert.ToDouble(memento.dict["budget"]);
            }

        }

        public override string ToString()
        {
            return $"Name: {_name}\nPhone: {_phone}\nBudget: {_budget}";
        }

    }
}
