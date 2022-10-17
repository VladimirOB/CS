using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MementoUpdate
{
    class Memento
    {
        public Dictionary<string, object> dict; // 1 - имя поля. 2 - значение.
        public Memento()
        {
            dict = new Dictionary<string, object>();
        }
        public void Add(string key, object value)
        {
            dict.Add(key, value);
        }
    }
}
