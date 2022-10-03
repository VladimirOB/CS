namespace Analizer_Reflection_
{
    class Program
    {
        static void Main(string[] args)
        {
            Button b = new Button();
            Analizer analizer = new Analizer();

            analizer.Analize(b);
        }
    }
}