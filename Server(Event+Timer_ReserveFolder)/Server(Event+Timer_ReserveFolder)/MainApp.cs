namespace Server_Event_Timer_ReserveFolder_
{
    class MainApp
    {
        /*2. Сервер работает по таймеру и следит за определённой папкой и делает резервные копии этой папки в папке BackUp
        Подписчики:
        1) выводит информацию о файле и о действиях сервера на экран
        2) останавливает сервер после 10 сек. работы
        3) пишет в файл .log*/

        static void Main()
        {
            Client1 client = new Client1();
            Client2 client2 = new Client2();
            Client3 client3 = new Client3();
            Server server = new Server("V:\\temp");
            server.Add(client.ShowInfo);
            server.Add(client2.shutDownServer);
            server.Add(client3.Log);

            server.Start();
            Console.ReadLine();
        }
    }
}