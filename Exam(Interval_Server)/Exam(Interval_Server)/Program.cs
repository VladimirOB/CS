using System.Text.Json;
namespace Exam_Interval_Server_
{
        class MainApp
        {
        static void Interval()
        {
            Intervals intervals = new Intervals();
            IntervalsDaughter intervals2 = new IntervalsDaughter();
            intervals2.Add(new Inter(7.5, 8.2));
            intervals2.Add(new Inter(1, 3));
            intervals2.Add(new Inter(1, 10));

            Console.WriteLine("Has holes:" + intervals2.HasHoles);
            foreach (var item in intervals2)
            {
                Console.WriteLine(item);
            }


            //int explic = (int)intervals2;
            //Console.WriteLine(explic);
            
        }

        static void Serv()
        {
            Client1 c1 = new Client1();
            Client2 c2 = new Client2();
            Server server = new Server("V:\\temp");
            server.Add(c1);
            server.Add(c2);
            server.Start();
            Console.ReadLine();
            
        }

        static void Main(string[] args)
        {
            Interval();

            //Serv();
        }
    }
}