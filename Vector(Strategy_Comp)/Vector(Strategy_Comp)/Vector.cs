using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Strategy_Comp_
{
    //class Comp : IComparer<int>
    //{
    //    public bool Compare(int x, int y)
    //    {
    //        return x < y;
    //    }
    //}



    abstract class SortStrategy
    {
        public abstract void Sort(ref int[] num);
    }

    class QuickSort : SortStrategy
    {
        public override void Sort(ref int[] num)
        {
            QSort(num, 0, num.Length-1);
        }
        void QSort(int[] num, int start, int end)
        {
            int temp;
            if (start >= end) return;
            int i = start, j = end;

            //середина массива
            int baseElementIndex = i + (j - i) / 2;
            // пока индекс левой части меньше правой
            while (i < j)
            {
                //значение погран элем.
                int value = num[baseElementIndex];

                //перемещаем индекс левой части вперёд, пока не встретится большой элемент
                while (i < baseElementIndex && (num[i] <= value)) i++;

                //перемещаем индекс правой части массива назад пока не встретится маленький элем
                while (j > baseElementIndex && (num[j] >= value)) j--;

                //i, j - индексы эл., которые нужно swap
                //если индексы правильные(есть смысл замены)
                if (i < j)
                {
                    temp = num[i];
                    num[i] = num[j];
                    num[j] = temp;

                    //корректировка базового элемента
                    if (i == baseElementIndex) baseElementIndex = j;
                    else if (j == baseElementIndex) baseElementIndex = i;
                }
            }
            QSort(num, start, baseElementIndex);
            QSort(num, baseElementIndex + 1, end);
        }
    }

    class BubbleSort : SortStrategy
    {
        public override void Sort(ref int[] num)
        {
            int temp;
            for (int i = 1; i < num.Length; i++)
            {
                // проход массива от конца в начало со "всплыванием" одного элемента
                for (int j = num.Length - 1; j >= i; j--)
                {
                    if (num[j - 1] > num[j])
                    {
                        temp = num[j - 1];
                        num[j - 1] = num[j];
                        num[j] = temp;
                    }
                }
            }
        }
    }

    class InsertionSort : SortStrategy
    {
        public override void Sort(ref int[] num)
        {
            int i, j, k, temp;

            for (i = 1; i < num.Length; i++)
            {
                // текущий элемент, для которого ищем позицию
                temp = num[i];

                //ищем правильную позицию
                for (j = 0; j < i; j++)
                {
                    if (num[j] > temp) break;
                }
                // если элемент уже на своём месте, продолжить
                if (j == i) continue;

                //смещение эл-ов слева-направо
                for (k = i; k > j; k--)
                {
                    num[k] = num[k - 1];
                }
                // ставим эл на своё место
                num[j] = temp;
            }
        }
    }

    class Vector
    {
        SortStrategy _strategy;
        public int[] vector = new int[10];

        public SortStrategy Strategy
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        public int this[int pos]
        {
            get { if (pos >= 0) return vector[pos]; else throw new Exception("Incorrect index!"); }
            set { if (pos >= 0) vector[pos] = value; else throw new Exception("Incorrect index!"); }
        }

        public Vector(SortStrategy strategy)
        {
            Random rand = new Random();
            _strategy = strategy;
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = rand.Next(0,100);
            }
        }


        public void Sort()
        {
            _strategy.Sort(ref vector);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < vector.Length; i++)
            {
                sb.Append(vector[i] + " ");
            }
            sb.Append('\n');
            return sb.ToString();
        }

        public void Print()
        {
            foreach (var item in vector)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}
