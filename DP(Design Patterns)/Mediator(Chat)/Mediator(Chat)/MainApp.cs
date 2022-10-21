namespace Mediator_Chat_
{
    class MainApp
    {
        /*1.Реализовать паттерн "Mediator" на примере программы-чата между различными видами войск в пределах компьютерной игры.
            Сервер реализовать при помощи events с возможностью логирования всех сообщений. 
            Клиенты при посылке сообщений должны указывать само сообщение и
            адресата сообщения. При получении сообщения каждый адресат получает 
            информацию о времени получения и об отправителе сообщения.*/
        static void Main()
        {
            Server serv = new Server();
            Aviation c1 = new Aviation(serv);
            HeavyArtillery c2 = new HeavyArtillery(serv);

            c1.Send("Hello", c2);
            c2.Send("World", c1);

            Console.Read();
        }
    }
}