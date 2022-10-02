using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphList
{
    class Vertex<T>
    {
        public List<Link<T>> links = new List<Link<T>>(); // рёбра, подходящие к данной вершине

        T title;                // имя вершины
        int id;                 // номер
        public int weight;      // вес
        public int label;       // метка вершины, нужна для работы алгоритмов поиска (волновой и Дейкстры)
        public int distance;    // мин расстояние до вершины (Дейкстра)

        // предыдущая вершина при движении по кратчайшему пути в алгоритме Дейкстры
        public Vertex<T> prev;



        public int Count { get { return links.Count; } }

        public int Id { get { return id; } }

        public T Title { get { return title; } set { title = value; } }

        public Vertex(int id, T title, int weight)
        {
            this.title = title;
            this.id = id;
            this.weight = weight;
        }

        //удаление ребра по ID без удаления их в соседней вершине
        public bool RemoveLink(int linkId)
        {
            foreach (var item in links)
            {
                if(item.Id == linkId)
                {
                    links.Remove(item);
                    return true;
                }
            }
            return false;
        }

        public void Print()
        {
            Console.WriteLine($"Vertex: {title}");
            foreach (var item in links)
            {
                item.Print();
            }
        }
    }
}
