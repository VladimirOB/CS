using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_File_Manager_
{

    //"Context" - класс системы, основанной на состояниях
    // потокобезопасный синглтон
    sealed class FileManager
    {
        private static readonly FileManager instance = new FileManager();

        private FileManager() { }

        //Текущее состояние системы
        private State currentState;

        public static FileManager Instance(State state)
        {
            instance.State = state;
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

        private void InitDecorator()
        {
            Label label = new Label();
            Border border = new Border(label);
            ScrollBar scrollBar = new ScrollBar(border);
            scrollBar.Draw();
        }

        private void InitFactoryMethod()
        {
            //Factory Method
            UIFactory factory = new UIFactory2(); // AbstractFactory
            GeneralUI general = new GeneralUI(factory);
            ProductsContainer pc = general.CreateUI();
            pc.Run();
        }

        public void Run()
        {
            InitDecorator();
            InitFactoryMethod();

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
    }
}
