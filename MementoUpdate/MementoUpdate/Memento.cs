using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoUpdate
{
    class Memento
    {
        public Dictionary<string, object> dict; // 1 - имя поля. 2 - значение.
        public Memento(object name, object phone, object budget)
        {
            dict = new Dictionary<string, object>();
            dict.Add("name", name);
            dict.Add("phone", phone);
            dict.Add("budget", budget);
        }
    }
}
