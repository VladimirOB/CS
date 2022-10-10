using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoUpdate
{
    [Serializable]
    class Memento
    {
        private object _name;
        private object _phone;
        private object _budget;
        public Memento(object name, object phone, object bugdet)
        {
            _name = name;
            _phone = phone;
            _budget = bugdet;
        }

        public object Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public object Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public object Budget
        {
            get { return _budget; }
            set { _budget = value; }
        }

    }
}
