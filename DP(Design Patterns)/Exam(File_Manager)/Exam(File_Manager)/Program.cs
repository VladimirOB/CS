namespace Exam_File_Manager_
{
    class Program
    {
        //абстрактная фабрика или фабричный метод
        //декоратор
        //прокси
        //Singleton
        //State

        //Паттерны должны быть обоснованы
        //Наличие комментариев
        static void Main()
        {
            FileManager fm = FileManager.Instance(OperationMenu.Instance());
            fm.Run();
            Console.Read();
        }
    }
}