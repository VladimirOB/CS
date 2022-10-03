﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_Link_.de
{
    class Program
    {
        static void Main()
        {
            // создание наблюдаемых объектов
            Node node1 = new Node(23, 4);
            Node node2 = new Node(2, 14);
            Node node3 = new Node(20, 30);

            // создание наблюдателей
            Link link1 = new Link(node1, node2);
            Link link2 = new Link(node2, node3);

            node2.Move(3, 5);

            //node2.X = 34;
            //node2.Y = 54;

            // Wait for user 
            Console.Read();

        }
    }
}
