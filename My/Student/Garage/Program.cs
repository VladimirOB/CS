using Garage123;

namespace Garage
{
  

    class Program
    {
        static Student GetStudent()
        {
            Student student = new Student(); // var student = new Student();
            student.firstName = "Vova";
            student.lastName = "Balaban";
            student.middleName = "Olegovich";
            student.age = 28;
            student.id = Guid.NewGuid();
            student.group = "112";
            student.color = Col.Blue;
            return student;
        }
        //static void Print(Student student)
        //{
        //    Console.WriteLine("Information about student: ");
        //    Console.WriteLine($"Id: {student.id}");
        //    Console.WriteLine($"Last: {student.lastName}");
        //    Console.WriteLine($"Name: {student.firstName}");
        //    Console.WriteLine($"Mid: {student.middleName}");
        //    Console.WriteLine($"Age: {student.age}");
        //    Console.WriteLine($"Group: {student.group}");
        //}

        static void Main(string[] args)
        {
            var firstStudent = GetStudent();
            firstStudent.Print();

            string fullName = firstStudent.GetFullName();
        }
    }
}