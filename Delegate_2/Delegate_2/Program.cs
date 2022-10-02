namespace Delegate_2
{
    class MainApp
    {
        delegate void Subscriber();

        class Server
        {
            event Subscriber subscriber;

            private List<ConcreteObserver> observers = new List<ConcreteObserver>();
            private string subjectState;

            // Property 
            public string SubjectState
            {
                get { return subjectState; }
                set { subjectState = value; }
            }

            // Добавление наблюдателя
            public void Attach(ConcreteObserver observer)
            {
                observers.Add(observer);
            }

            // Удаление наблюдателя
            public void Detach(ConcreteObserver observer)
            {
                observers.Remove(observer);
            }

            // Перебор и уведомление всех наблюдателей
            public void Notify()
            {
                foreach (ConcreteObserver o in observers)
                {
                    o.Update();
                }
            }
        }

        class ConcreteObserver
        {
            // Конкретные свойства наблюдателей
            private string name;
            private string observerState;

            // ссылка на наблюдаемый объект
            private Server subject;


            // Constructor 
            public ConcreteObserver(Server subject, string name)
            {
                this.subject = subject;
                this.name = name;
            }

            public void Update()
            {
                observerState = subject.SubjectState;
                Console.WriteLine("Observer {0}'s new state is {1}",
                  name, observerState);
            }

            // Property 
            public Server Subject
            {
                get { return subject; }
                set { subject = value; }
            }
        }

        static void Main()
        {


            // Создание наблюдаемого объекта
            Server s = new Server();

            // Добавление наблюдателей
            s.Attach(new ConcreteObserver(s, "X"));
            s.Attach(new ConcreteObserver(s, "Y"));
            s.Attach(new ConcreteObserver(s, "Z"));

            // Изменение свойства наблюдаемого объекта
            s.SubjectState = "ABC";

            // Уведомление наблюдателей об изменении свойств
            s.Notify();

            // Wait for user 
            Console.Read();
        }
    }
}