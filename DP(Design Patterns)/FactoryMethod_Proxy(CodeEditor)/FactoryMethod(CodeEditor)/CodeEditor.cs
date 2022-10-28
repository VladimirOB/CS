using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod_CodeEditor_
{
    //Subject - предок для заменяемого объекта и прокси (заместителя), задаёт правила использования
    //Product
    abstract class EditorObject
    {
        public abstract void Operation();
    }

    //Контейнер для продуктов
    class ProductsContainer
    {
        string name;
        List<EditorObject> lst = new List<EditorObject>();

        public ProductsContainer(string name)
        {
            this.name = name;
        }

        public void Add(EditorObject obj)
        {
            lst.Add(obj);
        }

        public void Print()
        {
            foreach (EditorObject obj in lst)
            {
                obj.Operation();
            }
        }
    }

    //Concrete product - генерируется фабричным методом.
    class BackgroundColor : EditorObject
    {
        ConsoleColor color;
        public BackgroundColor(ConsoleColor color)
        {
            this.color = color;
        }

        public override void Operation()
        {
            Console.BackgroundColor = color;
        }
    }


    class HighlightingCode : EditorObject
    {
        ConsoleColor color;
        public HighlightingCode(ConsoleColor color)
        {
            this.color = color;
        }

       

        public override void Operation()
        {
            Console.ForegroundColor = color;
        }
    }

    class ControlPanel : EditorObject
    {
        string name;
        public ControlPanel(string n)
        {
            name = n;
        }

        public override void Operation()
        {
            Console.WriteLine($"{name} Control_Panel");
        }
    }

    class ControlPanelProxyLog : EditorObject
    {
        // Ссылка на класс заменяемого объекта, т.к. часть работы будет выполнена им
        ControlPanel realSubject;

        string name;

        public ControlPanelProxyLog(string n)
        {
            name = n;
        }

        public override void Operation()
        {
            if (realSubject == null)
            {
                realSubject = new ControlPanel(name);
            }
            Console.WriteLine("Proxy");
            string str = name + " " + DateTime.Now.ToString() + "\n";
            File.AppendAllText("../../../../ControlPanel.log", str);
            realSubject.Operation(); // операция обычной панели
        }
    }

    //Creator - создатель, в котором объявлены фабр. методы
    abstract class GenaralEditor
    {
        public ProductsContainer CreateEditor()
        {
            ProductsContainer pc = new ProductsContainer("PC");

            EditorObject obj = null;
            obj = CreateBackColor(ConsoleColor.Red);
            pc.Add(obj);
            obj = CreatePanel("Panel'ka");
            pc.Add(obj);
            obj = CreateHighlightingCode(ConsoleColor.Green);
            pc.Add(obj);
            return pc;
        }
        public abstract EditorObject CreateBackColor(ConsoleColor color);
        public abstract EditorObject CreatePanel(string name);
        public abstract EditorObject CreateHighlightingCode(ConsoleColor color);
    }

    //ConcreteCreator - конкретный создатель для конкретных продуктов.
    class CodeEditor : GenaralEditor
    {
        public override BackgroundColor CreateBackColor(ConsoleColor color)
        {
            return new BackgroundColor(ConsoleColor.DarkGray);
        }

        public override HighlightingCode CreateHighlightingCode(ConsoleColor color)
        {
            return new HighlightingCode(ConsoleColor.Black);
        }

        public override ControlPanelProxyLog CreatePanel(string name)
        {
            return new ControlPanelProxyLog("Panel'ka");
        }
    }
}
