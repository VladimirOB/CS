namespace Visitor_Archiver_
{
    class Program
    {
        static void Main()
        {
            // Setup structure 
            Conveyor conveyor = new Conveyor();
            conveyor.Attach(new PackFile());
            //conveyor.Attach(new UnpackFile());

            // Create visitor objects 
            VisitorMin vis1 = new VisitorMin();
            VisitorMax vis2 = new VisitorMax();

            // Structure accepting visitors 
            conveyor.Accept(vis1);


            //conveyor.Accept(vis2);

            // Wait for user 
            Console.Read();
        }
    }
}