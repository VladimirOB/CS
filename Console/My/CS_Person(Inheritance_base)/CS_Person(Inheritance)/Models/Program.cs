namespace CS_Person_Inheritance_.Models
{

    class Program
    {
        static void Main(string[] args)
        {
            //Teacher teacher = new Teacher { FirstName = "Alla", LastName = "Alo" };
            //Student student = new Student { FirstName = "Vovan", LastName = "Balaban" };
            //teacher.Salary = 100;
            //student.Learn();
            //Person[] people = { teacher, student };
            //PrintPeoples(people);


            Point3D point = new Point3D(1, 2, 3);
            point.Print3D();

        }

        static void PrintPeoples(Person[] people)
        {
            foreach(var person in people)
            {
                person.PrintFullName();
            }
        }
    }
}