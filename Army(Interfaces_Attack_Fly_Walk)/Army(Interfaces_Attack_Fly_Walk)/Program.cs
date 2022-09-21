namespace Army_Interfaces_Attack_Fly_Walk_
{
    internal class Program
    {
        /*1. Создать интерфейсы: 
        IAttack { Move, Attack(x, y), Retreat, Stop, Defend}
        IFly
        IWalk { Stand, Walk, Stop, Run, GetCoords}

        Реализовать эти интерфейсы в классах (минимум 5 классов)

        Добавить класс Army - коллекция различных родов войск
        Интерфейсы для Army:
        - IAttack, IFly, IWalk*/

        static void Main(string[] args)
        {
            List<IFly> airForces = new List<IFly>();
            Army air = new Army();
            airForces.Add(air);
            foreach (object item in airForces)
            {
                (item as IFly)?.TakeOff();
            }
            foreach (IFly item in airForces)
            {
                item.TakeOff();
            }
            Human human = new Human();
            human.Run();
            human.Stop();
            (human as IFly).Move(3, 2, 1);
            ((IFly)human).Move(1, 2, 3);
            IFly fly = human;
            fly.Move(2, 1, 3);
            if (fly is IFly)
            {
                ((IFly)fly).Move(3,1,2);
            }

            Army soldier = new Army();
            soldier.Fire(new AK47());

        }
    }
}