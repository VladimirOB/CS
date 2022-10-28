using System;
using System.Collections.Generic;
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
    }
}
