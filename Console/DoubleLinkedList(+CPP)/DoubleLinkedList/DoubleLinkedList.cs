using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

        /*2. Реализовать класс DoubleLinkedList, имеющий следующие методы:
        - констр
        - void Add(string str) - добавление элемента в список
        - void Insert(int pos, string str) - вставка в список
        - void Print() - печать списка
        - PrintBack() - печать списка в обратную сторону
        - RemoveByPos(int pos) - удаление элемента по индексу*/

namespace _DoubleLinkedList
{
    [Serializable]
    public class Element<T>
    {
        public T str;
        Element<T> next;
        Element<T> prev;


        public Element(T str)
        {
            this.str = str;
        }

        public Element<T> Next
        {
            get { return next; }
            set { next = value; }
        }

        public Element<T> Prev
        {
            get { return prev; }
            set { prev = value; }
        }

    }

    [Serializable]
    class DoubleLinkedList<T>
    {
        Element<T> Current;
        Element<T> First;
        Element<T> Last;
        uint size;

        public DoubleLinkedList()
        {
            size = 0;
            First = Last = Current = null;
        }

        public bool isEmpty
        {
            get { return size == 0; }
        }

        public void Add(T str)
        {
            Element<T> newElem = new Element<T>(str);
            if(First == null)
            {
                First = Last = newElem;
            }
            else
            {
                Last.Next = newElem; // Добавить вновь созданную ячейку списка в конец списка.
                newElem.Prev = Last; // добавить в новую ячейку адрес предыдущего элемента (пока он последний)
                Last = newElem;      // Исправить указатель на последнюю ячейку.(Переместить в него адрес вновь созданной посл. ячейки).
            }
            Count++;
        }

        public void Insert_Index(T newElement, uint index)
        {
            if(index < 1 || index > size) // ошибка, если неправильный индекс
            {
                throw new InvalidOperationException();
            }
            else if (index == 1) // если начало
            {
                Push_Front(newElement);
            }
            else if (index == size) // если конец
            {
                Push_Back(newElement);
            }
            else // иначе ищем элем с таким индексом
            {
                uint count = 1;
                Current = First;
                while(Current != null && count != index)
                {
                    Current = Current.Next;
                    count++;
                }
                Element<T> newElem = new Element<T>(newElement); // создаём новый объект
                Current.Prev.Next = newElem; // для предыдущего след будет новый
                newElem.Prev = Current.Prev; // для нового предыдущий будет предыдущий старого
                Current.Prev = newElem; // старый пред. равен новому
                newElem.Next = Current; // след. нового равен старому.
            }
        }

        public void Push_Front(T newElement)
        {
            Element<T> newElem = new Element<T>(newElement);

            if(First == null) // если список пустой
            {
                First = Last = newElem; 
            }
            else
            {
                newElem.Next = First;
                First = newElem; // First и new указывают на один и тот же объект
                newElem.Next.Prev = First;
                Count++;
            }
        }

        public Element<T> Pop_Front()
        {
            if (First == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                Element<T> temp = First;
                if (First.Next != null) // если в списке больше 1 эл.
                {
                    First.Next.Prev = null;
                }
                First = First.Next; //смещение вперёд
                Count--;
                return temp;
            }
        }

        public void Push_Back(T newElement)
        {
            Element<T> newElem = new Element<T>(newElement);

            if(First == null)
            {
                First = Last = newElem;
            }    
            else
            {
                Last.Next = newElem;
                newElem.Prev = Last;
                Last = newElem;
            }
            Count++;
        }

        public Element<T> Pop_Back()
        {
            if(Last == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                Element<T> temp = Last;
                if(Last.Prev != null)
                {
                    Last.Prev.Next = null;
                }
                Last = Last.Prev; // смещение назад
                Count--;
                return temp;
            }
        }

        public void ClearList() // очистка
        {
            while(!isEmpty)
            {
                Pop_Front();
            }
        }

        public uint Count // свойство для size
        {
            get { return size; }
            set { size = value; }
        }

        public void Print()
        {
            if(First == null)
            {
                Console.WriteLine("DLL is empty");
                return;
            }
            Current = First;
            uint count = 1;
            while(Current != null)
            {
                Console.WriteLine("Element " + count.ToString() + " : " + Current.str);
                count++;
                Current = Current.Next;
            }
        }

        public void PrintBack()
        {
            if(Last == null)
            {
                Console.WriteLine("DLL is empty");
                return;
            }
            Current = Last;
            uint count = 1;
            while (Current != null)
            {
                Console.WriteLine("Element " + count.ToString() + " : " + Current.str);
                count++;
                Current = Current.Prev;
            }
        }

        public void Delete(uint index)
        {
            if(index < 1 || index > size)
            {
                throw new InvalidOperationException();
            }
            else if (index == 1)
            {
                Pop_Front();
            }
            else if (index == size)
            {
                Pop_Back();
            }
            else
            {
                uint count = 1;
                Current = First;
                while(Current != null && count != index)
                {
                    Current = Current.Next;
                    count++;
                }
                Current.Prev.Next = Current.Next; // смещаем
                Current.Next.Prev = Current.Prev;
            }
        }
    }
}
