namespace FactoryMethod_CodeEditor_
{
    class MainApp
    {
        static void Main()
        {
            //создание конкретного создателя
            GenaralEditor editor = new CodeEditor();
            //Генерация продуктов
            ProductsContainer pc = editor.CreateEditor();

            pc.Print();

        }
    }
}