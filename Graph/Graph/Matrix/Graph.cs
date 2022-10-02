using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Matrix
{
    class Graph
    {
        int maxVertexCount;
        int currentVertexCount;
        int deleteIndexCount;
        double[,] links;
        Dictionary<string, int> vertices;
        Dictionary<int, string> rev_vertices;
        Dictionary<string, int> labels;
        List<string> linksList;
        List<string> foundVertices;

        public Graph(int maxVertexCount)
        {
            this.maxVertexCount = maxVertexCount;
            currentVertexCount = 0;
            deleteIndexCount = 0;
            //словарь вершин графа
            vertices = new Dictionary<string, int>();
            rev_vertices = new Dictionary<int, string>();
            labels = new Dictionary<string, int>();
            links = new double[maxVertexCount, maxVertexCount]; // двумерный массив связей между вершинами
            for (int i = 0; i < maxVertexCount; i++)
            {
                for (int k = 0; k < maxVertexCount; k++)
                {
                    // нет никах связей между любыми вершинами
                    links[i,k] = 0;
                }
            }
        }

        public bool AddVertex(string title)
        {
            //если вершина с таким именем в графе отсутсвует
            if(!vertices.ContainsKey(title))
            {
                vertices.Add(title, currentVertexCount);
                rev_vertices.Add(currentVertexCount++, title);
                labels.Add(title, 0);
                return true;
            }
            else
            {
                Console.WriteLine($"The vertex with name: {title} already exists!");
                return false;
            }
        }
        public bool RenameVertex(string oldName, string newName)
        {
            if(vertices.ContainsKey(oldName) && !vertices.ContainsKey(newName))
            {
                int vertexIndex = vertices[oldName];
                vertices.Remove(oldName);
                vertices.Add(newName, vertexIndex);
                return true;
            }
            else
            {
                Console.WriteLine($"The vertex with name: {oldName} not found. Or vertex with name: {newName} already exists");
                return false;
            }
        }

        public bool AddLink(string title1, string title2, double weight)
        {
            if(!vertices.ContainsKey(title1) || !vertices.ContainsKey(title2))
            {
                Console.WriteLine("Invalid vertex name");
                return false;
            }

            int vertexIndex1 = vertices[title1];
            int vertexIndex2 = vertices[title2];

            links[vertexIndex1, vertexIndex2] = weight;
            links[vertexIndex2, vertexIndex1] = weight;
            return true;
        }

        public bool DeleteLink(string vertex1, string vertex2)
        {
            if (!vertices.ContainsKey(vertex1) || !vertices.ContainsKey(vertex2))
            {
                Console.WriteLine("Invalid vertex name");
                return false;
            }
            int vertexIndex1 = vertices[vertex1];
            int vertexIndex2 = vertices[vertex2];

            links[vertexIndex1, vertexIndex2] = 0;
            links[vertexIndex2, vertexIndex1] = 0;
            return true;
        }
        
        public bool DeleteVertex(string title)
        {
            if(vertices.ContainsKey(title))
            {
                int vertexIndex = vertices[title];
                vertices.Remove(title);

                for (int i = 0; i < currentVertexCount; i++)
                {
                    for (int k = vertexIndex; k < currentVertexCount-1; k++)
                    {
                        links[i, k] = links[i, k + 1];
                    }
                }

                for (int i = 0; i < currentVertexCount; i++)
                {
                    links[i, currentVertexCount - 1] = 0;
                }

                for (int i = vertexIndex; i < currentVertexCount-1; i++)
                {
                    for (int k = 0; k < currentVertexCount; k++)
                    {
                        links[i, k] = links[i + 1, k];
                    }
                }

                for (int i = 0; i < currentVertexCount; i++)
                {
                    links[currentVertexCount - 1, i] = 0;
                }
                currentVertexCount--;
                return true; // не дописал
            }
            return false;
        }
    }
}
