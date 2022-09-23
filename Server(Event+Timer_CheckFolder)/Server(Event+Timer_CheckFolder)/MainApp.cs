namespace Server_Event_Timer_CheckFolder_
{
    class MainApp
    {
        static void Main()
        {
            Client1 client = new Client1();
            Client2 client2 = new Client2();
            Client3 client3 = new Client3();
            Server server = new Server("V:\\temp");
            server.Add(client.ShowInfo);
            server.Add(client2.DeleteImage);
            server.Add(client3.Log);

            server.Start();
            Console.ReadLine();
        }
    }
}