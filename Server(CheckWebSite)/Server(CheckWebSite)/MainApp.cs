using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Server_CheckWebSite_
{
    class MainApp
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.StartServer();
            Console.ReadLine();
        }
    }
}