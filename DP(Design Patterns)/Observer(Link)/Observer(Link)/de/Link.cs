using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_Link_.de
{

    class Link  // Link = Server = Subject; // "ConcreteObserver" - конкретный наблюдатель
    {
        private Node node1, node2;
        int x1, y1, x2, y2;

        void EraseLink()
        {
            Console.WriteLine("Erase link {0},{1} - {2},{3}", x1, y1, x2, y2);
        }
        void DrawLink()
        {
            Console.WriteLine("Draw link {0},{1} - {2},{3}", x1, y1, x2, y2);
        }

        public Link(Node n1, Node n2)
        {
            node1 = n1;
            node2 = n2;

            //подписка на сообщения от узлов
            n1.Attach(Update);
            n2.Attach(Update);

            x1 = n1.x; y1 = n1.y;
            x2 = n2.x; y2 = n2.y;
        }

        public void Update(int x, int y)
        {
            EraseLink();
            x1 = node1.x;
            x2 = node2.x;
            y1 = node1.y;
            y2 = node2.y;
            DrawLink();
        }
      
    }
}
