using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_File_Manager_
{
    // "Decorator" - базовый класс для всех декораторов
    abstract class Decorator : Component
    {
        // Декорируемый элемент управления
        protected Component component = null;

        //изменение декорируемого элемента
        public void SetComponent(Component component)
        {
            this.component = component;
        }

        public override void Draw()
        {
            // Если декорируемый компонент не равен 0 - то отрисовать его
            if (component != null)
            {
                // отрисовка объекта-хозяина (компонент или другой декоратор)
                component.Draw();
            }
        }
    }

    // "ConcreteDecoratorA"
    class Border : Decorator
    {
        private bool bold;

        public Border(Component component)
        {
            this.component = component;
        }

        public override void Draw()
        {
            // вызов отрисовки базового класса, которая вызывает отрисовку "хозяина"
            base.Draw();

            bold = true;
            Console.WriteLine("Border.Draw()");
        }
    }

    // "ConcreteDecoratorB"
    class ScrollBar : Decorator
    {
        public ScrollBar(Component component)
        {
            this.component = component;
        }

        public override void Draw()
        {
            // вызов отрисовки базового класса, которая вызывает отрисовку "хозяина"
            base.Draw();

            Console.WriteLine("ScrollBar.Draw()");
        }
    }

    // "Component" - базовый класс для всех элементов управления
    public abstract class Component
    {
        // метод, позволяющий компоненту отрисовать самого себя
        public abstract void Draw();
    }

    // "ConcreteComponent" - класс обычного элемента управления
    public class Label : Component
    {
        public override void Draw()
        {
            Console.WriteLine("Label.Draw()");
        }
    }

}
