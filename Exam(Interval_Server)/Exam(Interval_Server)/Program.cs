using System.Text.Json;
namespace Exam_Interval_Server_
{
  
        class MainApp
        {
        static void Interval()
        {
            Intervals intervals = new Intervals();
            intervals.Load("../../../../db.txt");
            //intervals.Add(new Inter(1, 2));
            //intervals.Add(new Inter(3, 2));

            //intervals = intervals + inter;
            //intervals += inter;
            //Intervals intervals2 =  new Intervals(intervals);

            foreach (var current in intervals)
            {
                Console.WriteLine(current);
            }
            //Interval? longest = intervals.GetLongest();
            //Console.WriteLine(longest.Start + " : " + longest.End);
        }

        static void Serv()
        {
            using (Client1 c1 = new Client1())
            {
                Client2 c2 = new Client2();
                Server server = new Server("V:\\temp");

                server.Add(c1);
                server.Add(c2);
                server.Start();
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            //Interval();

            Serv();
        }
    }
}