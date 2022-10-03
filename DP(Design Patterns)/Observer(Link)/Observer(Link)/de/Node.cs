using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_Link_.de
{
    // делегат для наблюдателей
    delegate void ObserverDelegate(int x, int y);// "ConcreteSubject" - конкретный наблюдаемый объект

    class Node
    {
        public int x, y;
        event ObserverDelegate observers;

        public int X 
        {
            get { return x; }
            set
            {
                x = value;
                Notify(x, y);
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                Notify(x, y);
            }
        }

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Attach(ObserverDelegate ev)
        {
            observers += ev;
        }

        void Notify(int x, int y)
        {
            observers.Invoke(x, y);
        }
        
        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
            Notify(x, y);
        }
    }
}
