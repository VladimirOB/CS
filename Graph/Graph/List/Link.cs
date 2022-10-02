using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphList
{
    class Link<T>
    {
        // ссылка на вершину, из которой идёт ребро
        public Vertex<T> from;
        // ссылка на вершину, в которую идёт ребро
        public Vertex<T> to;

        //название ребра
        string title;

        //номер ребра
        int id;

        //вес ребра
        public int weight;

        public Link(int id, string title, int weight, Vertex<T> from, Vertex<T> to)
        {
            this.from = from;
            this.to = to;
            this.title = title;
            this.id = id;
            this.weight = weight;
        }

        public string Title { get { return title; } } // возвратить имя ребра

        public int Id { get { return id; } } // возвратить Id ребра

        public void Print()
        {
            Console.WriteLine($"Link: {title}, From:{from.Title}, To: {to.Title}. weight: {weight}");
        }
    }
}
