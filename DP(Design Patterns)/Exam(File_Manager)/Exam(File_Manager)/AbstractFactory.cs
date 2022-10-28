using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_File_Manager_
{
    //"Product"
    abstract class UIObject
    {
        public abstract void Operation();
    }

    //Контейнер с продуктами
    class ProductsContainer
    {
        //Лист продуктов
        List<UIObject> lst = new List<UIObject>();

        public void Add(UIObject obj)
        {
            lst.Add(obj);
        }

        //Вызов операции у всех продуктов
        public void Run()
        {
            foreach (UIObject obj in lst)
            {
                obj.Operation();
            }
        }
    }
    //"Concrete product"
    class Button : UIObject
    {
        string name;

        public Button(string n)
        {
            name = n;
        }
        public override void Operation()
        {
            Console.WriteLine($"Button: {name}");
        }
    }
    //"Concrete product"
    class Font : UIObject
    {
        string name;

        public Font(string n)
        {
            name = n;
        }
        public override void Operation()
        {
            Console.WriteLine($"Font: {name}");
        }
    }
    //"Concrete product"
    class ToolBar : UIObject
    {
        string name;

        public ToolBar(string n)
        {
            name = n;
        }
        public override void Operation()
        {
            Console.WriteLine($"ToolBar: {name}");
        }
    }

    //"AbstractFactory"
    abstract class UIFactory
    {
        public abstract Button CreateButton(string name);
        public abstract Font CreateFont(string name);
        public abstract ToolBar CreateToolBar(string name);

    }

    // "Creator"
    class GeneralUI
    {
        UIFactory factory;

        public GeneralUI(UIFactory factory)
        {
            this.factory = factory;
        }

        //создание контейнера с продуктами
        public ProductsContainer CreateUI()
        {
            ProductsContainer pc = new ProductsContainer();
            UIObject obj = factory.CreateButton("Button 0");
            pc.Add(obj);
            obj = factory.CreateFont("Arielo");
            pc.Add(obj);
            obj = factory.CreateToolBar("Tool Bar");
            pc.Add(obj);
            return pc;
        }
    }

    //"Concrete Factory"
    class UIFactory1 : UIFactory
    {
        public override Button CreateButton(string n)
        {
            return new Button("Button 1");
        }

        public override Font CreateFont(string n)
        {
            return new Font("Ariel");
        }

        public override ToolBar CreateToolBar(string n)
        {
            return new ToolBar("Piv bar");
        }
    }

    //"Concrete Factory"
    class UIFactory2 : UIFactory
    {
        public override Button CreateButton(string n)
        {
            return new Button("Button 2");
        }

        public override Font CreateFont(string n)
        {
            return new Font("Corbel");
        }

        public override ToolBar CreateToolBar(string n)
        {
            return new ToolBar("Troll bar");
        }
    }
}
