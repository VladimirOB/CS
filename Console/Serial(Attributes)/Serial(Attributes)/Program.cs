using System.Reflection;

namespace Serial_Attributes_
{
    class Car
    {

    }
    class Program
    {
        /*1. Реализовать свою систему сериализации, которая позволяет сериализовать в файл поля класса, имеюшие типы:
        - Int32
        - Double
        - String
        - Boolean

        У сериализуемых полей должен быть атрибут [Storable]

        Пример программы:

        Student man = new Student("Alex", "Petrov", 23, "Donetsk", "Lenina", 3);

        try
        {               
	        // бинарная сериализация
	        MySerializer bf = new MySerializer();
	        FileStream fstream = new FileStream("student.dat", FileMode.Create, FileAccess.Write, FileShare.None);
	        bf.Serialize(fstream, man);
	        fstream.Close();
        }
        catch (Exception ex)
        {
	        Console.WriteLine(ex.Message);
        }

        // десериализация одиночного объекта
        FileStream fstream2 = File.OpenRead("student.dat");
        MySerializer bf2 = new MySerializer();
        Student man2 = (Student)bf2.Deserialize(fstream2);
        fstream2.Close();

        man2.Print();*/
        static void Main(string[] args)
        {
            //Student student = new Student("Zohan", 1.19, 53, true, 22);

            //FileStream? fs = null;
            //try
            //{
            //    MySerializer ms = new MySerializer();
            //    fs = new FileStream("../../../../student.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            //    ms.Serialize(fs, student);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //} 
            //finally
            //{
            //    fs.Close();
            //}

            //// десериализация одиночного объекта
            FileStream fstream2 = File.OpenRead("../../../../student.txt");
            MySerializer ms2 = new MySerializer();
            Student student2 = (Student)ms2.Deserialize(fstream2, typeof(Student));
            fstream2.Close();
            student2.Print();


            //Student student2 = (Student)Activator.CreateInstance(Type.GetType("Serial_Attributes_.Student"))ms2.Deserialize(fstream2);
            //Student obj = (Student)Assembly.GetExecutingAssembly().CreateInstance("Serial_Attributes_.Student");
        }
    }
}