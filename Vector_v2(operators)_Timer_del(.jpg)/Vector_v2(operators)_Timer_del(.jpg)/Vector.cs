using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_v2_operators__Timer_del_.jpg_
{
    /*1. Разработать класс Vector, который имеет следующие члены:
    - конструктор(int size)
    - индексатор
    - свойство Size - длина массива для чтения
    - Print() - печать вектора
    - ToString() - печать в Console.WriteLine
    - Save()
    - Load()
    - Sum()

    - operator ==, !=
    - operator ++, --
    - operator V+n, V1+V2
    - operator V-n, V-V2
    - operator int()*/

    internal class Vector
    {
        int[] vector;
        int size;
        public Vector(int size)
        {
            Random random = new Random();
            if (size > 0)
            {
                vector = new int[size];
                this.size = size;
                for (int i = 0; i < size; i++)
                {
                    vector[i] = random.Next(1, 100);
                }
            }
            
        }

        public int Size { get { return size; } }

        public int this[int pos]
        {
            get { if (pos >= 0) return vector[pos]; else throw new Exception("Incorrect index!"); }
            set { if (pos >= 0) vector[pos] = value; else throw new Exception("Incorrect index!"); }
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
            catch (Exception ex)
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
                if (new_size > size)
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

        public static Vector operator++(Vector v)
        {
            Vector new_v = v;
            for (int i = 0; i < v.size; i++)
            {
                ++new_v[i];
            }
            return new_v;
        }

        public static Vector operator --(Vector v) 
        {
            Vector new_v = v;
            for (int i = 0; i < v.size; i++)
            {
                --new_v[i];
            }
            return new_v;
        }

        public static bool operator ==(Vector v, Vector v2)
        {
            if (v.size == v2.size)
            {
                for (int i = 0; i < v.size; i++)
                {
                    if (v[i] != v2[i])
                        return false;
                }
                return true;
            }
            else
                return false;
        }
        public static bool operator !=(Vector v, Vector v2)
        {
            if (v.size != v2.size)
            {
                for (int i = 0; i < v.size; i++)
                {
                    if (v[i] == v2[i])
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        public static Vector operator +(Vector v, Vector v2)
        {
            if(v2.size <= v.size)
            {
                for (int i = 0; i < v2.size; i++)
                {
                    v[i] += v2[i];
                }
                return v;
            }
            else
            { 
                Console.WriteLine("Error size");
                return v;
            }
        }

        public static Vector operator -(Vector v, Vector v2)
        {
            if (v2.size <= v.size)
            {
                for (int i = 0; i < v2.size; i++)
                {
                    v[i] -= v2[i];
                }
                return v;
            }
            else
            {
                Console.WriteLine("Error size");
                return v;
            }
        }

        public static Vector operator -(Vector v, int n)
        {
            Vector result = v;
            for (int i = 0; i < result.size; i++)
            {
                result[i] = v[i] - n;
            }
            return result;
        }

        public static Vector operator +(Vector v, int n)
        {
            Vector result = v;
            for (int i = 0; i < result.size; i++)
            {
                result[i] = v[i] + n;
            }
            return result;
        }

        public static explicit operator int(Vector v) 
        {
            return v.Sum();
        }

    }
}

