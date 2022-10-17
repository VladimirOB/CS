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
           
            LuckyDeterminationSystem lds = new LuckyDeterminationSystem();
            lds.Add(new LuckySums());
            lds.Add(new Ascending());
            lds.Add(new Descending());
            lds.Add(new Alternation());
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
                    if (lds.isLucky(ticket))
                        lds.AddLucky(ticket);
                }
            }
            else
            {
                for (int i = 111111; i < 1000000; i++)
                {
                    Ticket ticket = new Ticket(i);
                    if (lds.isLucky(ticket))
                        lds.AddLucky(ticket);
                    else
                        ticket = null;
                }
                
            }
            lds.SaveLuckyTicket("../../../../result.txt");
        }
    }
}