

/*Пример программы очень прост. Она рассчитывает и выводит отчет об
оплате клиентом услуг в магазине видеопроката. Программе сооб-
щается, какие фильмы брал в прокате клиент и на какой срок. После
этого она рассчитывает сумму платежа исходя из продолжительности
проката и типа фильма. Фильмы бывают трех типов: обычные, дет-
ские и новинки. Помимо расчета суммы оплаты начисляются бонусы
в зависимости от того, является ли фильм новым.*/
namespace Refactoring_Fauler
{
    class Program
    {
        static void Main()
        {
            Movie Turtles = new Movie("Turtles", 2);
            Movie Aladdin = new Movie("Aladdin", 1);
            Movie FightClub = new Movie("Fight Club", 0);

            Customer Valerka = new Customer("Valerka"); // 19 бонус 3
            Valerka.addRental(new Rental(Aladdin, 4));
            Valerka.addRental(new Rental(FightClub, 4));

            Customer Grisha = new Customer("Grisha"); // 16 бонус 2
            Grisha.addRental(new Rental(Turtles, 7));
            Grisha.addRental(new Rental(FightClub, 5));

            Console.WriteLine(Valerka.statement());
            Console.WriteLine();
            Console.WriteLine(Grisha.statement());
        }
    }
}
