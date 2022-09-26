using System.Text.Json;
namespace Exam_Interval_Server_
{
    class MainApp
    {
        static void Main(string[] args)
        {

            Intervals intervals = new Intervals();
            intervals.Load("../../../../db.txt");
            Inter inter = new Inter(4.5, 5.5);
            intervals = intervals + inter;
            intervals += inter;
            //Intervals intervals2 =  new Intervals(intervals);

            foreach (var current in intervals)
            {
                Console.WriteLine(current);
            }
            //Interval? longest = intervals.GetLongest();
            //Console.WriteLine(longest.Start + " : " + longest.End);
        }
    }
}