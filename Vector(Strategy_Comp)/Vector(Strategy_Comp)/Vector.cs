using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Strategy_Comp_
{
    abstract class Comparator
    {
        public abstract bool Comp(int a, int b);
    }
    
    class DescendingSort : Comparator
    {
        public override bool Comp(int a, int b)
        {
            if (a > b)
                return true;
            else
                return false;
        }
    }

    class AscendingSort : Comparator
    {
        public override bool Comp(int a, int b)
        {
            if (a < b)
                return true;
            else
            return false;
        }
    }

    abstract class SortStrategy
    {
        public abstract void Sort(ref int[] num, Comparator comp);
    }

    class QuickSort : SortStrategy
    {
        public Comparator? comp;
        public override void Sort(ref int[] num, Comparator comp)
        {
            this.comp = comp;
            QSort(num, 0, num.Length - 1);
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
                while (i < baseElementIndex && (comp.Comp(num[i], value))) i++;

                //перемещаем индекс правой части массива назад пока не встретится маленький элем
                while (j > baseElementIndex && (!comp.Comp(num[j],value))) j--;

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
        public override void Sort(ref int[] num, Comparator comp)
        {
            int temp;
            for (int i = 1; i < num.Length; i++)
            {
                // проход массива от конца в начало со "всплыванием" одного элемента
                for (int j = num.Length - 1; j >= i; j--)
                {
                    if (comp.Comp(num[j - 1], num[j]))
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
        public override void Sort(ref int[] num, Comparator comp)
        {
            
            int i, j, k, temp;

            for (i = 1; i < num.Length; i++)
            {
                // текущий элемент, для которого ищем позицию
                temp = num[i];

                //ищем правильную позицию
                for (j = 0; j < i; j++)
                {
                    if (comp.Comp(num[j], temp)) break;
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
        Comparator comp;
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

        public Vector(SortStrategy strategy, Comparator comp) // комп в стратегию
        {
            Random rand = new Random();
            _strategy = strategy;
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = rand.Next(0,100);
            }
            this.comp = comp;
        }


        public void Sort()
        {
            _strategy.Sort(ref vector, comp);
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
