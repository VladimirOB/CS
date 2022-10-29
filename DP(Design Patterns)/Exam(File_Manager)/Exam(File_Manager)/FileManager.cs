using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_File_Manager_
{

    //"Context" - класс системы, основанной на состояниях
    // потокобезопасный синглтон
    sealed class FileManager : Component
    {
        private static readonly FileManager instance = new FileManager();

        //ссылка на декоратор
        Component comp;

        //ссылка на продуктовый контейнер
        ProductsContainer pc;

        private FileManager() { }

        //Текущее состояние системы
        private State currentState;

        public static FileManager Instance(State state, Component component, ProductsContainer pc)
        {
            instance.comp = component;
            instance.pc = pc;
            instance.State = state;
            pc.Run();
            return instance;
        }

        //Property для изменения состояний
        public State State
        {
            get { return currentState; }
            set
            {
                currentState = value;
                Console.WriteLine("State: " + currentState.GetType().Name);
            }
        }

        public void Run()
        {
            while (true)
            {
                Request();
                Thread.Sleep(999);
            }
        }
        // Запрос на изменение состояния
        private void Request()
        {
            currentState.Handle(this);
        }

        public override void Draw()
        {
            comp.Draw();
        }
    }
}
