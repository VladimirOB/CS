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
            //Decorator
            Label label = new Label();
            Border border = new Border(label);
            ScrollBar scrollBar = new ScrollBar(border);

            //Factory Method
            UIFactory factory = new UIFactory2(); // AbstractFactory
            GeneralUI general = new GeneralUI(factory);
            ProductsContainer pc = general.CreateUI();

            FileManager fm = FileManager.Instance(OperationMenu.Instance(), scrollBar, pc);
            fm.Draw();
            fm.Run();
            Console.Read();
        }
    }
}