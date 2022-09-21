namespace Vector_Interfaces_Strategy_
{
    internal class Program
    {
        /*1. Реализовать паттерн Strategy на примере класса Vector и стратегий сортировки применить интерфейсы для реализации паттерна Strategy
        Класс Vector содержит числа, сортировки по-настоящему сортируют массив
        
        2. Стратегии сортировки поддерживают компараторы для насторойки направления сортировки*/


        static void Main(string[] args)
        {
            Vector v = new Vector(5, new QuickSort(new DescendingSort()));
            v.Sort();
            Console.WriteLine(v);
            Console.ReadKey();
        }
    }
}