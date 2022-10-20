namespace State_ATM_
{
    class Program
    {
        static void Main()
        {
            ATM atm = new ATM(new WaitForCard());
            atm.Start();

            Console.Read();
        }
    }
}