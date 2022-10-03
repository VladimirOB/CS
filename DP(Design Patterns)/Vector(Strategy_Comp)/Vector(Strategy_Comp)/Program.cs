using System;
using System.Diagnostics;
using System.Xml.Linq;
namespace Vector_Strategy_Comp_
{
    class MainApp
    {
        /*1. Реализовать паттерн Strategy на примере класса Vector и стратегий сортировки
        Класс Vector содержит числа, сортировки по-настоящему сортируют массив

        2. Стратегии сортировки поддерживают компараторы для насторойки направления сортировки*/


        static void Main()
        {
            Vector v = new Vector(new QuickSort(new DescendingSort()));
            Console.WriteLine(v);
            v.Sort();
            Console.WriteLine(v);
        }
    }
}