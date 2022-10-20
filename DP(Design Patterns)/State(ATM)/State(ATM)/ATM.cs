using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_ATM_
{
    //"Context" - банкомат, класс системы, основанной на состояниях
    class ATM
    {
        //Текущее состояние системы
        private State currentState;

        public int balanceCard;

        public int balanceATM = 100000;

        public ATM(State state)
        {
            this.State = state;
        }

        //Property - текущее состояние системы
        public State State
        {
            get { return currentState; }
            set
            {
                currentState = value;
                Console.WriteLine("State: " + currentState.GetType().Name);
            }
        }

        public void Start()
        {
            while(true)
            {
                Request();
                Thread.Sleep(999);
            }
        }

        // Запрос на изменение состояния
        void Request()
        {
            currentState.Handle(this);
        }
    }
}
