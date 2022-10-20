using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_ATM_
{
    //"State" - абстр. класс состояний
    abstract class State
    {
        public abstract void Handle(ATM context);
    }

    //Состояние ожидания карты
    class WaitForCard : State
    {
        public override void Handle(ATM context)
        {
            Console.WriteLine("Insert card and press enter");
            Console.ReadLine();
            Console.WriteLine("Enter PIN:");
            int pin = Convert.ToInt32(Console.ReadLine());

            if (pin != 1111)
            {
                context.State = new ErrorState();
                return;
            }
            Console.WriteLine("Operation succesfull!");
            context.balanceCard = 1000;
            context.State = new OperationMenu();
        }
    }

    class OperationMenu : State
    {
        public override void Handle(ATM context)
        {
            Console.Clear();
            Console.WriteLine("Make a choise: ");
            Console.WriteLine("Press 1 to view the balance on the card");
            Console.WriteLine("Press 2 to transfer to another card");
            Console.WriteLine("Press 3 to dispense cash");
            Console.WriteLine("Press 0 to exit");
            int ch;
            if(Int32.TryParse(Console.ReadLine(), out ch))
            {
                switch(ch)
                {
                    case 1:
                        {
                            context.State = new BalanceOnCard();
                            break;
                        }
                    case 2:
                        {
                            context.State = new TransferMoney();
                            break;
                        }
                    case 3:
                        {
                            context.State = new DispenseCash();
                            break;
                        }
                    case 0:
                        {
                            context.State = new WaitForCard();
                            break;
                        }
                }    
            }
        }
    }

    class BalanceOnCard : State
    {
        public override void Handle(ATM context)
        {
            Console.WriteLine($"Balance on the card: " + context.balanceCard);
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            context.State = new OperationMenu();
        }
    }

    class TransferMoney : State
    {
        public override void Handle(ATM context)
        {
            Console.WriteLine("You have successfully transferred all cash to Vladimir");
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            context.State = new OperationMenu();
        }
    }
    class DispenseCash : State
    {
        public override void Handle(ATM context)
        {

            Console.WriteLine($"Enter the amount you want to withdraw (Balance on ATM: {context.balanceATM})");
            int sum;
            if (Int32.TryParse(Console.ReadLine(), out sum))
            {
                if(sum < context.balanceATM)
                {
                    if (sum < context.balanceCard)
                    {
                        context.balanceCard -= sum;
                        context.balanceATM -= sum;
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadLine();
                        context.State = new OperationMenu();
                    }
                    else
                        Console.WriteLine("Insufficient funds on the card!");
                }
                else
                    Console.WriteLine("Sorry, ATM ran out of money. Please, try again later!");
            }
        }
    }

    class ErrorState : State
    {
        public override void Handle(ATM context)
        {
            Console.WriteLine("Incorrect pin!");

            context.State = new WaitForCard();
        }
    }

}
