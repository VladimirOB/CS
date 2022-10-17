using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Verb_Adjective_nouns
{
    /*1. Разработать класс Vector, который имеет следующие члены:
- конструктор(int size)
- индексатор
- свойство Size - длина массива для чтения
- Print() - печать вектора
- ToString() - печать в Console.WriteLine
- Save()
- Load()
- Sum()*/
    internal class Vector
    {
        int[] vector;
        int size;
        public Vector(int size)
        {
            if(size>0)
            vector = new int[size];
            this.size = size;
            for (int i = 0; i < size; i++)
            {
                vector[i] = i;
            }
        }

        public int Size {  get { return size; } }

        public int this[int pos]
        {
            get { if (pos >= 0) return vector[pos]; else throw new Exception("Incorrect index!"); }
            set { if(pos >= 0) vector[pos] = value; else throw new Exception("Incorrect index!"); }
        }

        public void Print()
        {
            for (int i = 0; i < vector.Length; i++)
            {
                Console.Write(vector[i] + " ");
            }
        }

        public override string ToString()
        {
            string v = "";
            for (int i = 0; i < vector.Length; i++)
            {
                v += vector[i] + " ";
            }
            return $"{v}";
        }

        public int Sum()
        {
            int sum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i];
            }
            return sum;
        }

        public void Save()
        {
            try
            {
                StreamWriter sw = new StreamWriter("1.txt");
                sw.WriteLine(size);
                for (int i = 0; i < vector.Length; i++)
                {
                    sw.Write(vector[i] + " ");
                }
                
                sw.Close();
            }
           catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = 0;
            }
        }

        public void Load()
        {
            try
            {
                StreamReader sr = new StreamReader("1.txt");

                string temp = sr.ReadLine();

                int new_size = Convert.ToInt32(temp);
                temp = sr.ReadLine();
                string[] new_v = temp.Split(' '); 
                if ( new_size > size)
                {
                    vector = new int[new_size];
                    size = new_size;
                    for (int i = 0; i < size; i++)
                    {
                        vector[i] = Convert.ToInt32(new_v[i]);
                    }
                }
                else
                {
                    size = new_size;
                    for (int i = 0; i < size; i++)
                    {
                        vector[i] = Convert.ToInt32(new_v[i]);
                    }

                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
