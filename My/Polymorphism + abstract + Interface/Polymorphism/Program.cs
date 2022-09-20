namespace Polymorphism
{
    internal class Program
    {

        interface IHasInfo
        {
            void ShowInfo();
        }

        interface IWeapon 
        {
            int Damage { get; }
            void Fire();
        }

        abstract class Weapon : IHasInfo, IWeapon // множественное наследование интерфейсов
        {
            public abstract int Damage { get; }
            public abstract void Fire(); // абстрактные методы могут быть только внутри абст. классов.

            public void ShowInfo()
            {
                //Fire();
                //Console.WriteLine(GetType().Name); // метод GetType унаследован от Object

                Console.WriteLine($"{GetType().Name} Damage: {Damage}");
            }
        }

        class Gun : Weapon // наследник абстр. класса обязан реализовать абстр. метод родителя
        {
            public override int Damage { get {return 5;} } 

            public override void Fire() // override работает и с virtual и с abstract
            {
                //throw new NotImplementedException(); 
                Console.WriteLine("тра та та та та та");
            }
        }

        class LaserGun : Weapon // наследник абстр. класса обязан реализовать абстр. метод родителя
        {
            public override int Damage => 10;
            public override void Fire() // override работает и с virtual и с abstract
            {
                //throw new NotImplementedException(); 
                Console.WriteLine("пиу");
            }
        }

        class Bow : Weapon // наследник абстр. класса обязан реализовать абстр. метод родителя
        {
            public override int Damage => 3; // лямбда выражение

            public override void Fire()
            {
                //throw new NotImplementedException(); 
                Console.WriteLine("пуньк");
            }
        }

        class Player
        {
            //public void Fire(Weapon weapon)
            public void Fire(IWeapon weapon)
            {
                weapon.Fire();
            }

            public void ChechInfo(IHasInfo hasInfo)
            {
                hasInfo.ShowInfo();
            }
        }

        class Box : IHasInfo
        {
            public void ShowInfo()
            {
                Console.WriteLine("Коробка");
            }
        }


        class Car
        {
            protected virtual void StartEngine() // protected можно использовать в наследнике
            {
                Console.WriteLine("Двигатель запущен!");
            }
            public virtual void Drive()
            {
                StartEngine();
                Console.WriteLine("Я машина, я еду!");
            }
        }

        class SportCar : Car
        {
            protected override void StartEngine()
            {
                Console.WriteLine("Спорт двигатель запущен!");
            }

            public override void Drive() // override - переопределение базового класса
            {
                //base.Drive(); // вызов базового метода 
                StartEngine();
                Console.WriteLine("Я еду очень быстро");
            }
        }

        class Person
        {
            public void Drive(Car car)
            {
                car.Drive();
            }
        }

        static void Main(string[] args)
        {
            Player player = new Player();
            //Gun gun = new Gun();
            //player.Fire(gun);

            Weapon[] inventory = { new Gun(), new LaserGun(), new Bow() }; // массив со всеми видами

            foreach (var weapon in inventory)
            {
                player.ChechInfo(weapon);
                player.Fire(weapon);
                Console.WriteLine();
            }
            player.ChechInfo(new Box()); // передали коробку
           

            //Person person = new Person();
            //person.Drive(new SportCar());
        }
    }
}