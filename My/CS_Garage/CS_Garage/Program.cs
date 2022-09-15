namespace CS_Garage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int CurrentSize = 0;
            int MaxSize = 20;
            Cars[] car = new Cars[MaxSize];
            for (int i = 0; i < 10; i++)
            {
                car[i] = new Cars();
            }
            //System.Threading.Thread.Sleep(1000); // пауза на секунду
            while (true)
            {
                int number;
                Console.WriteLine("Welcome to Garage menu.");
                Console.WriteLine("Press 1 to add a new car.");
                Console.WriteLine("Press 2 to view all cars");
                Console.WriteLine("Press 3 to delete car by ID");
                Console.WriteLine("Press 4 to view car info by ID");
                Console.WriteLine("Press 5 to save car's.");
                Console.WriteLine("Press 6 to load car's.");
                Console.WriteLine("Press 7 to exit.");
                string? ch = Console.ReadLine();
                switch (ch)
                {
                    case "1":
                    {
                        car[CurrentSize++].EnterCar();
                        Console.Clear();
                        break;
                    }
                    case "2":
                    {
                        for (int i = 0; i < CurrentSize; i++)
                        {
                            car[i].PrintCarList();
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case "3":
                    {

                        Console.WriteLine("Enter ID: ");
                        int del;
                        for (int i = 0; i < CurrentSize; i++)
                        {
                            del = car[i].Delete();
                            if(del == 1)
                            {
                                for (int k = i; k < CurrentSize; k++)
                                {
                                    car[k] = car[k + 1];
                                }
                                CurrentSize--;
                                Console.WriteLine("Car deleted!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case "4":
                    {

                        Console.WriteLine("Enter ID: ");
                        for (int i = 0; i < CurrentSize; i++)
                        {
                            car[i].Search();
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case "5":
                    {
                        for (int i = 0; i < CurrentSize; i++)
                        {
                            car[i].Save();
                        }
                            Console.WriteLine("Cars saved!");
                            Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case "6":
                    {
                        CurrentSize = 0;
                        int currentLoadSize = car[0].LoadSize();
                        if(currentLoadSize < MaxSize)
                        {
                            for (int i = 0; i < currentLoadSize; i++)
                            {
                                car[i].Load(i);
                                    CurrentSize++;
                            }
                        }
                       
                        Console.WriteLine("Cars loaded!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    default:
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "7":
                    {
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }
    }
}