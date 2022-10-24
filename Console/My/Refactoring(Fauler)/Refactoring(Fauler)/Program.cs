

/*Пример программы очень прост. Она рассчитывает и выводит отчет об
оплате клиентом услуг в магазине видеопроката. Программе сооб
щается, какие фильмы брал в прокате клиент и на какой срок. После
этого она рассчитывает сумму платежа исходя из продолжительности
проката и типа фильма. Фильмы бывают трех типов: обычные, дет
ские и новинки. Помимо расчета суммы оплаты начисляются бонусы
в зависимости от того, является ли фильм новым.*/
namespace Refactoring_Before
{
    class Program
    {
        static void Main()
        {
            Movie Turtles = new Movie("Turtles", 2);
            Movie Aladdin = new Movie("Aladdin", 1);
            Movie FightClub = new Movie("Fight Club", 0);

            Customer Valerka = new Customer("Valerka"); // 11
            Valerka.addRental(new Rental(Aladdin, 3));
            Valerka.addRental(new Rental(FightClub, 3));

            Customer Grisha = new Customer("Grisha"); // 12
            Grisha.addRental(new Rental(Turtles, 8));
            Grisha.addRental(new Rental(FightClub, 4));

            Console.WriteLine(Valerka.statement());
            Console.WriteLine();
            Console.WriteLine(Grisha.statement());
        }
    }
}

//namespace Refactoring_After
//{
//    class Program
//    {
//        static void Main()
//        {
//            
//        }
//    }
//}
