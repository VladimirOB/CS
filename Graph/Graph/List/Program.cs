namespace GraphList
{
    class MainApp
    {
        /*1. Для класса Graph добавить следующие методы:
        - RenameVertex(string oldName, string newName) - переименование вершины
        - PrintLinks(string title) - выводит названия и веса связанных с заданной вершин
        - Print() - выводит все вершины и связи между ними
        - DeleteLink(string vertex1, string vertex2) - удаление ребра между вершинами
        - DeleteVertex(string title) - удаление вершины со всеми связями
        - MinWeight() - нахождение минимального веса среди всех
        - VertexConnection(string vertex1, string vertex2) - проверить связаны ли вершины*/

        /*2. Для матричного класса Graph добавить следующие методы:
        - конструктор копирования
        - operator =
        - operator == (для сравнения двух графов)
        - Save(string filename) - сохранение графа в файл
        - Load(string filename) - загрузка графа из файла
        - GetRoute(string from, string to) - нахождение кратчайшего пути на графе по волновому алгоритму*/
        static void GraphList()
        {
            Graph<string> graph = new Graph<string>("test");
            //graph.AddVertex("A", 10);
            //graph.AddVertex("B", 20);
            //graph.AddVertex("C", 30);
            //graph.AddVertex("D", 25);
            //graph.AddVertex("E", 21);
            //graph.AddLink("A", "B", "link1", 12);
            //graph.AddLink("B", "C", "link2", 23);
            //graph.AddLink("D", "E", "link3", 2);
            //graph.AddLink("C", "D", "link4", 21);
            //graph.AddLink("B", "E", "link4", 21);
            //graph.GetRoute("A", "E");

            graph.AddVertex("One", 34);
            graph.AddVertex("Two", 45);
            graph.AddVertex("Three", 23);
            graph.AddVertex("Four", 78);
            graph.AddVertex("Five", 11);
            graph.AddVertex("Six", 21);
            graph.AddLink("One", "Two", "", 7);
            graph.AddLink("One", "Three", "", 9);
            graph.AddLink("One", "Six", "", 14);
            graph.AddLink("Two", "Three", "", 10);
            graph.AddLink("Two", "Four", "", 15);
            graph.AddLink("Three", "Four", "", 11);
            graph.AddLink("Three", "Six", "", 2);
            graph.AddLink("Four", "Five", "", 6);
            graph.AddLink("Five", "Six", "", 9);

            Graph<string> graph2 = new Graph<string>(graph);
            //graph2.AddVertex("One", 34);
            //graph2.AddVertex("Two", 45);
            //graph2.AddVertex("Three", 23);
            //graph2.AddVertex("Four", 78);
            //graph2.AddVertex("Five", 11);
            //graph2.AddVertex("Seven", 21);

            //graph2.AddLink("One", "Seven", "", 55);
            //graph2.AddLink("One", "Three", "", 9);
            //graph2.AddLink("One", "Six", "", 14);
            //graph2.AddLink("Two", "Three", "", 10);
            //graph2.AddLink("Two", "Four", "", 15);
            //graph2.AddLink("Three", "Four", "", 11);
            //graph2.AddLink("Three", "Six", "", 2);
            //graph2.AddLink("Four", "Five", "", 6);
            //graph2.AddLink("Five", "Six", "", 9);

            if (graph == graph2)
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
            //graph.VertexConnection("One", "Six");
            //graph.DeleteLink("Two", "Three");
            //graph.PrintLinks("Two");
            //graph.GetRouteDijkstra("One", "Six");

            //graph.AddVertex(1, 34);
            //graph.AddVertex(2, 45);
            //graph.AddVertex(3, 23);
            //graph.AddVertex(4, 78);
            //graph.AddVertex(5, 11);
            //graph.AddVertex(6, 21);
            //graph.AddLink(1, 2, "", 7);
            //graph.AddLink(1, 3, "", 9);
            //graph.AddLink(1, 6, "", 14);
            //graph.AddLink(2, 3, "", 10);
            //graph.AddLink(2, 4, "", 15);
            //graph.AddLink(3, 4, "", 11);
            //graph.AddLink(3, 6, "", 2);
            //graph.AddLink(4, 5, "", 6);
            //graph.AddLink(5, 6, "", 9);

            //graph.GetRouteDijkstra(1, 6);

        }

        static void Main(string[] args)
        {
            GraphList();
        }
    }
}