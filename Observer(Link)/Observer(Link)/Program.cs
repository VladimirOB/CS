﻿namespace Observer_Link_
{
    /*1. Реализовать паттерн "Наблюдатель" на примере схемы, содержащей узлы и рёбра. Узлы схемы являются наблюдаемыми объектами, а рёбра
	являются наблюдателями. В ответ на изменение координат узлов, рёбра перерисовывают себя по новым координатам, предварительно удалив себя
	по старым. Координаты вершин можно менять при помощи метода Move(x, y) или при помощи свойств.

	Пример метода Main:
		// создание наблюдаемых объектов
		Node node1 = new Node(23, 4);
		Node node2 = new Node(2, 14);
		Node node3 = new Node(20, 30);

		// создание наблюдателей
		Link link1 = new Link(node1, node2);
		Link link2 = new Link(node2, node3);

		node2.Move(3, 5);

		node2.X = 34;
		node2.Y = 54;

		// Wait for user 
		Console.Read();*/
    class MainApp
    {
        static void Main(string[] args)
        {
            Node node1 = new Node(23, 4);
            Node node2 = new Node(2, 14);
            Node node3 = new Node(20, 30);

            Subject link1 = new Subject(node1, node2);
            Subject link2 = new Subject(node2, node3);
            node2.Move(3, 5);

			//node2.X = 34;
            //node2.Y = 54;
        }
    }
}