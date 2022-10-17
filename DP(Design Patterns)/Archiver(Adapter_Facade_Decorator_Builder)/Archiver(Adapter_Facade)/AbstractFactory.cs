using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    //Product
    abstract class UIObject
    {
        public abstract void Operation();
    }

    class ProductsContainer
    {
        List<UIObject> lst = new List<UIObject>();

        public void Add(UIObject obj)
        {
            lst.Add(obj);
        }

        public void Run()
        {
            foreach (UIObject obj in lst)
            {
                obj.Operation();
            }
        }
    }

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

    //Creator
    class GeneralUI
    {
        UIFactory factory;
        public GeneralUI(UIFactory factory)
        {
            this.factory = factory;
        }

        public ProductsContainer CreateUI()
        {
            ProductsContainer pc = new ProductsContainer();
            UIObject obj = factory.CreateButton("Buttonchik");
            pc.Add(obj);
            obj = factory.CreateFont("Georgia");
            pc.Add(obj);
            obj = factory.CreateToolBar("Tool Bar");
            pc.Add(obj);
            return pc;
        }
    }

    //абстрактная фабрика
    abstract class UIFactory
    {
        public abstract Button CreateButton(string n);
        public abstract Font CreateFont(string n);
        public abstract ToolBar CreateToolBar(string n);
    }

    //Конкретная фабрика
    class ConcreteUIFactory1 : UIFactory
    {
        public override Button CreateButton(string n)
        {
            return new Button("Button 1");
        }

        public override Font CreateFont(string n)
        {
            return new Font("Impact");
        }

        public override ToolBar CreateToolBar(string n)
        {
            return new ToolBar("Piv bar");
        }
    }

    class ConcreteUIFactory2 : UIFactory
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
            return new ToolBar("Kakoi - to bar");
        }
    }



}
