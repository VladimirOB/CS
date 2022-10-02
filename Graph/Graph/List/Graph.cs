using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphList
{
    class Graph<T>
    {
        string title;

        int vertexId = 0;// номер след вершины, который будет создана
        int vertexCount = 0; // кол-во вершин
        int linkId = 0; // номер след ребра, который будет создан
        int linkCount = 0; // кол-во рёбер

        Dictionary<T, Vertex<T>> vertices;

        List<Vertex<T>> FoundVertices;
        public Graph(string title)
        {
            this.title = title;
            vertices = new Dictionary<T, Vertex<T>>();
            FoundVertices = new List<Vertex<T>>();
        }

        public Graph(Graph<T> source)
        {
            vertexCount = source.vertexCount;
            vertexId = source.vertexId;
            linkCount = source.linkCount;
            linkId = source.linkId;
            title = source.title;
            vertices = new Dictionary<T, Vertex<T>>(source.vertices);
            FoundVertices = new List<Vertex<T>>(source.FoundVertices);
        }

        public Vertex<T> FindVertex(T title) // поиск по названию
        {
            if(vertices.ContainsKey(title))
                return vertices[title];
            return null;
        }

        public int AddVertex(T title, int weight)
        {
            if(FindVertex(title) == null)
            {
                Vertex<T> vertex = new Vertex<T>(vertexId++, title, weight);
                if(vertex != null)
                {
                    vertices.Add(title, vertex);
                    vertexCount++;

                    //возврат Id созданной вершины
                    return vertexId - 1;
                }
            }
            return -1;
        }

        public int AddLink(T from, T to, string title, int weight)
        {
            Vertex<T> vertexFrom = FindVertex(from);
            Vertex<T> vertexTo = FindVertex(to);

            if(vertexFrom != null && vertexTo != null)
            {
                Link<T> link = new Link<T>(linkId++, title, weight, vertexFrom, vertexTo);
                if(link != null)
                {
                    vertexFrom.links.Add(link);
                    vertexTo.links.Add(link);

                    linkCount++;// кол-во рёбер +1

                    return (int)linkId - 1;
                }
            }
            return -1;
        }

        public bool RenameVertex(T oldName, T newName) // RenameVertex(string oldName, string newName)
        {
            if(vertices.ContainsKey(oldName) && !vertices.ContainsKey(newName))
            {
                vertices[oldName].Title = newName;
                return true;
            }
            else
            {
                Console.WriteLine($"The vertex with name: {oldName} not found. Or vertex with name: {newName} already exists");
                return false;
            }
        }

        public bool PrintLinks(T title) //PrintLinks(string title)- выводит названия и веса связанных с заданной вершин
        {
            if (vertices.ContainsKey(title))
            {
                foreach (var item in vertices)
                {
                    if(item.Key.Equals(title))
                    {
                        item.Value.Print();
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine($"The vertex with name: {title} not found.");
                return false;
            }
        }

        public bool DeleteLink(T vertex1, T vertex2)  //- удаление ребра между вершинами
        {
            if(vertices.ContainsKey(vertex1) && vertices.ContainsKey(vertex2))
            {
                bool flag = false;
                foreach (var item in vertices[vertex1].links)
                {
                    foreach (var item2 in (vertices[vertex2].links))
                    {
                        if(item.Id == item2.Id)
                        {
                            vertices[vertex1].links.Remove(item);
                            vertices[vertex2].links.Remove(item2);
                            linkCount--;
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                        break;
                }
                return true;
            }
            else
            {
                Console.WriteLine($"The vertex with name: {vertex1} or {vertex2} not found.");
                return false;
            }
        }
        public int MinWeightVertex() //- нахождение минимального веса среди всех вершин
        {
            int min = int.MaxValue;
            foreach (var item in vertices)
            {
                if(item.Value.weight < min)
                    min = item.Value.weight;
            }
            return min;
        }
        public int MinWeightLinks() //- нахождение минимального веса среди всех рёбер
        {
            int min = int.MaxValue;
            foreach (var vert in vertices.Values)
            {
                foreach (var link in vert.links)
                {
                    if (link.weight < min)
                        min = link.weight;
                }
            }
            return min;
        }
        
        public bool VertexConnection(T vertex1, T vertex2)  //проверить связаны ли вершины
        {
            if (vertices.ContainsKey(vertex1) && vertices.ContainsKey(vertex2))
            {
                foreach (var item in vertices[vertex1].links)
                {
                    foreach (var item2 in (vertices[vertex2].links))
                    {
                        if (item.Id == item2.Id)
                        {
                            Console.WriteLine("Vertex connection!");
                            return true;
                        }
                    }
                }
                Console.WriteLine("Vertex not connection!");
                return false;
            }
            else
            {
                Console.WriteLine($"The vertex with name: {vertex1} or {vertex2} not found.");
                return false;
            }
        }

        public static bool operator ==(Graph<T> graph, Graph<T> graph2)
        {
            int cnt = 0, cntTemp = 0;
            if (graph.vertexCount == graph2.vertexCount && graph.linkCount == graph2.linkCount
            && graph.linkId == graph2.linkId && graph.title == graph2.title)
            {
                foreach (var item in graph.vertices)
                {
                    cntTemp = 0;
                    foreach (var item2 in graph2.vertices)
                    {
                        if(cntTemp == cnt)
                        {
                            if (!item.Value.Title.Equals(item2.Value.Title)) // или id
                            {
                                return false;
                            }
                            cnt++;
                            break;
                        }
                        cntTemp++;
                    }
                }
                return true;
            }
            else
                return false;
        }

        public static bool operator !=(Graph<T> graph, Graph<T> graph2)
        {
            if (graph.vertexCount != graph2.vertexCount && graph.linkCount != graph2.linkCount
            && graph.linkId != graph2.linkId && graph.title != graph2.title)
            {
                return true;
            }
            else
                return false;
        }

        public bool RemoveVertex(int vertexId)
        {
            foreach (var current in vertices)
            {
                Vertex<T> currentVertex = current.Value;

                //если найдена нужная вершина
                if(currentVertex.Id == vertexId)
                {
                    // из общего количества рёбер графа вычесть количество рёбер, подходящее к удаляемой вершине
                    linkCount -= currentVertex.Count;

                    vertexCount--; // кол-во вершин -1

                    vertices.Remove(current.Key);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveVertex(T title)
        {
            foreach (var current in vertices)
            {
                Vertex<T> currentVertex = current.Value;
                if(currentVertex.Title.Equals(title))
                {
                    // из общего количества рёбер графа вычесть количество рёбер, подходящее к удаляемой вершине
                    linkCount -= currentVertex.Count;

                    vertexCount--; // кол-во вершин -1

                    vertices.Remove(current.Key);
                    return true;
                }
            }
            return false;
        }

        public void Print()
        {
            Console.WriteLine($"Graph: {title}");
            Console.WriteLine($"Vertices: {vertexCount} Links: {linkCount}");
            foreach (var current in vertices)
            {
                current.Value.Print();
            }
        }

        public void GetRoute(T from, T to)
        {
            FoundVertices.Clear();
            List<Vertex<T>> oldFront = new List<Vertex<T>>();  // Список текущих просмотренных вершин, обрабатываемых на текущей итерации
            List<Vertex<T>> newFront = new List<Vertex<T>>();// Список текущих помеченных вершин, обрабатываемых на следующей итерации

            int t = 0; // номер итерации
            Vertex<T> vertexFrom = FindVertex(from);
            Vertex<T> vertexTo = FindVertex(to);

            if (vertexFrom != null && vertexTo != null)
            {
                foreach (var item in vertices)
                {
                    item.Value.label = -1; // помечаем как непосещённые
                }
                // Пометить начальную вершину как посещённую и поместить в список для текущей обработки
                vertexFrom.label = 0;
                oldFront.Add(vertexFrom);
            }
            // Пока не закончились помеченные вершины с необработанными соседями
            while(oldFront.Count > 0)
            {
                // Перебрать все помеченные вершины с необработанными соседями
                foreach (var vertex in oldFront)
                {
                    // Для текущей вершины перебрать множество соседних вершин
                    foreach (var link in vertex.links)
                    {
                        Vertex<T> current = FindVertex(link.to.Title);

                        //Если текущая вершина оказалась искомой
                        if(current == vertexTo)
                        {
                            current.label = t + 1;
                            Vertex<T> reverse = current;

                            //Цикл по вершинам для возврата
                            while(reverse.label > 0)
                            {
                                FoundVertices.Add(reverse);
                                foreach (Link<T> revLink in reverse.links)
                                {
                                    if (revLink.from.label == reverse.label - 1)
                                    {
                                        reverse = revLink.from;
                                        break;
                                    }
                                }
                            }
                            FoundVertices.Add(vertexFrom);
                            FoundVertices.Reverse();
                            Console.WriteLine("Path found!");
                            foreach (var item in FoundVertices)
                            {
                                Console.Write($"{item.Title} ");
                            }
                            return; //The end
                        }
                        if(current.label == -1)
                        {
                            current.label = t + 1;
                            newFront.Add(current);
                        }
                    }
                }
                oldFront.Clear();
                foreach (var item in newFront)
                {
                    oldFront.Add(item);
                }
                newFront.Clear();
                t++;
            }
        }

        public void GetRouteDijkstra(T from, T to)
        {
            FoundVertices.Clear();
            Vertex<T> vertexFrom = FindVertex(from);
            Vertex<T> vertexTo = FindVertex(to);

            if (vertexFrom != null && vertexTo != null)
            {
                // Перебрать множество вершин и пометить их как непосещённые, текущее расстояние до вершмн - бесконечность
                foreach (var vertex in vertices)
                {
                    vertex.Value.label = 0;
                    vertex.Value.distance = int.MaxValue;
                    vertex.Value.prev = null;
                }

                // Пометить стартовую вершину как посещённую, расстояние до неё равно 0
                vertexFrom.distance = 0;

                while(true)
                {
                    Vertex<T> minVertex = null; // вершина с мин расстоянием от текущей

                    // Перебрать множество соседних вершин для вершины с минимальным расстоянием
                    foreach (var item in vertices)
                    {
                        if (minVertex == null && item.Value.label == 0)
                            minVertex = item.Value;
                        if (minVertex != null && item.Value.label == 0 && minVertex.distance > item.Value.distance)
                            minVertex = item.Value;
                    }
                    // Если дистанция до вершины с минимальным расстоянием равна бесконечности - решения нет
                    if (minVertex == null || minVertex.distance == uint.MaxValue)
                        return; // решения нет

                    // Если вершина с минимальным расстоянием является искомой
                    if(minVertex == vertexTo)
                    {
                        // Двигаться по вершинам в обратном направлении и добавлять их названия в массив результатов
                        Vertex<T> v = minVertex;
                        while(v != null)
                        {
                            FoundVertices.Add(v);
                            v = v.prev;
                        }
                        FoundVertices.Reverse();

                        Console.WriteLine("Path found!");
                        foreach (var item in FoundVertices)
                        {
                            Console.Write($"{item.Title} ");
                        }
                        return;
                    }
                    minVertex.label = 1;

                    // Для минимальной вершины перебрать множество соседних вершин
                    foreach (var link in minVertex.links)
                    {
                        Vertex<T> currentVertex = (link.from == minVertex) ? link.to : link.from;

                        // Если соседняя вершина не посещена и отмеченная дистанция до неё больше дистанции
                        // до минимальной вершины + вес связующего ребра
                        if(currentVertex.label == 0 && currentVertex.distance > (minVertex.distance + link.weight))
                        {
                            // поменять дистанцию к соседней вершине
                            currentVertex.distance = minVertex.distance + link.weight;

                            //Указать мин вершину как предыдущую на пути к соседней
                            currentVertex.prev = minVertex;
                        }

                    }
                }

            }
        }

        public T MaxLinks()
        {
            int MaxLinks = 0;
            T? Title = default(T);

            foreach (var item in vertices)
            {
                if(MaxLinks < item.Value.Count)
                {
                    MaxLinks = item.Value.Count;
                    Title = item.Value.Title;
                }
            }
            return Title;
        }
    }
}
