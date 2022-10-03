using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_Link_
{
    delegate void Subscriber(Subject sub);
    class Subject 
    {
        event Subscriber subscribers;
        Node node1Temp = new Node(0,0);
        Node node2Temp = new Node(0,0);
        Node node1;
        Node node2;
        public Subject(Node node1, Node node2)
        {
            this.node1 = node1;
            this.node2 = node2;
            node1Temp = node1.Clone();
            node2Temp = node2.Clone();
            Attach(node1.Update);
            Attach(node2.Update);
            subscribers.Invoke(this);
        }
        // добавление наблюдателя
        public void Attach(Subscriber ev)
        {
            subscribers += ev;
        }

        public void Detach(Subscriber ev)
        {
            subscribers -= ev;
        }

        public void StartEvent()
        {
            if(node1Temp.X != node1.X || node1Temp.Y != node1.Y ||
               node2Temp.X != node2.X || node2Temp.X != node2.X)
            {
                Console.WriteLine("Edge redrawn");
                Console.WriteLine($"Old edge: node_1: ({node1Temp.X}, {node1Temp.Y}) node_2: ({node2Temp.X}, {node2Temp.Y})");
                Console.WriteLine($"New edge: node_1: ({node1.X}, {node1.Y}) node_2: ({node2.X}, {node2.Y})");
                node1Temp.X = node1.X;
                node1Temp.Y = node1.Y;
            }
            Console.WriteLine();
        }
    }
}
