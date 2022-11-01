using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Antivirus_
{
    // делегат для наблюдателей
    // "ConcreteSubject" - конкретный наблюдаемый объект
    //delegate void ObserverDelegate();

    class Client
    {
        //список наблюдателей
        //event ObserverDelegate observers;
        List<Observer> observers = new List<Observer>();

        //добавить наблюдателя
        public void Attach(Observer ev)
        {
            observers.Add(ev);
        }

        public void Notify(HashSet<FileInfo> viruses)
        {
            foreach (Observer item in observers)
            {
                item.Update(viruses);
            }
        }
    }

    // "Observer" - абстрактный наблюдатель, задаёт поведение всех наблюдателей
    abstract class Observer
    {
        // Метод, который вызывается наблюдаемыми объектами
        public abstract void Update(HashSet<FileInfo> viruses);
    }


    class ConcreteObserver : Observer
    {
        private Client client1;

        public ConcreteObserver(Client c1)
        {
            client1 = c1;

            //подписка на сообщения от клиентов(узлов)
            client1.Attach(this);
        }

        public override void Update(HashSet<FileInfo> viruses)
        {
            Console.WriteLine("Viruses found: {0}", viruses.Count);
            int cnt = 1;
            foreach (var item in viruses)
            {
                Console.Write(cnt++ + ") ");
                Console.WriteLine(item);
            }
        }
    }
}
