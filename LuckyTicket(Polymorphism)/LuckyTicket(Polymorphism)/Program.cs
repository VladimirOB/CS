using System;
using System.Net.Sockets;

namespace LuckyTicket_Polymorphism_
{
    class Program
    {
        /*2. Задача о счастливых билетах
        Все билеты шестизначные
        Номера начинаются с 111111 и заканчиваются 999999
        Счастливыми являются следующие паттерны:
        123006 - сумма первых 3 чисел равна сумме последних 3 чисел
        123456 - номера идут в порядке возрастания
        987654 - номера идут в порядке убывания
        111111 - все цифры одинаковые
        202020 - цифры чередуются

        Задание:
        - определить сколько всего счастливых билетов
        - определить "счастливость" для введенного номера билета
        - сохранить все номера билетов в файл
        - при помощи полиморфизма дать возможность пользователям класса добавлять новые паттерны счастливых билетов
        - использовать абстрактные классы*/

        static void Main(string[] args)
        {
            HashSet<Ticket> lucky = new HashSet<Ticket>();
            List<LuckyCriteria> criterias = new List<LuckyCriteria>();
            criterias.Add(new LuckySums());
            criterias.Add(new Alternation());
            criterias.Add(new Ascending());
            criterias.Add(new Descending());
            criterias.Add(new Equal());

            Console.WriteLine("Enter number of ticket or press enter to continue to check all ticket.");
            string? str = Console.ReadLine();
            if(str != "")
            {
                int num;
                if(Int32.TryParse(str, out num))
                {
                    if(num <111111 || num > 999999)
                    {
                        Console.WriteLine("Invalid number of ticket. Number of ticket = 111111");
                        num = 111111;
                    }
                    Ticket ticket = new Ticket(num);
                    if (ticket.isLucky(criterias))
                        lucky.Add(ticket);
                }
            }
            else
            {
                for (int i = 111111; i < 1000000; i++)
                {
                    Ticket ticket = new Ticket(i);
                    if (ticket.isLucky(criterias))
                        lucky.Add(ticket);
                    else
                        ticket = null;
                }
                
            }

            StreamWriter sw = new StreamWriter("../../../../result.txt");
            sw.WriteLine($"Count of lucky ticket = {lucky.Count}");
            foreach (var ticket in lucky)
            {
                sw.WriteLine(ticket.Number.ToString());
            }
            sw.Close();
            Console.WriteLine(lucky.Count);
        }
    }
}