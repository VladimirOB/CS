using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Analizer_Reflection_
{
    /*1. Метод Analize(object obj) класса Analizer принимает ссылка на
        анализируемый объект какого-то класса и запускает все методы этого класса.
        Методы класса могут быть полностью без параметров или содержать параметры следующих типов: Int32, Double, String, Bool.
        При передаче параметров в запускаемые методы они заполняются случайными данными.*/

    class Button
    {
        public int x, y;
        public double wigth, height;
        public string title = "Button";
        public string text = "text";
        bool isButtonUp = false;

        public void Print()
        {
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"x = {x}, y = {y}");
            Console.WriteLine($"height = {height}, width {wigth}");
            Console.WriteLine($"Text: {text}");
            Console.WriteLine($"Is button up = {isButtonUp}");
        }
        protected static void TextExt(Button button, string newText)
        {
            Console.WriteLine("TextExt");
            button.text = "123";
        }

        void SetText(string newText)
        {
            TextExt(this, newText);
        }


        void set(int a, int b)
        {
            Console.WriteLine("set x,y");
            x = a;
            y = b;
        }

        void SetTitle(string str)
        {
            Console.WriteLine("Set title");
            title = str;
        }

        protected void setSize(double a, double b)
        {
            Console.WriteLine("Set size!");
            wigth = a;
            height = b;
        }

        private void PushButton(bool flag)
        {
            Console.WriteLine("push button");
            isButtonUp = flag;
        }

        void test(int a, double b, string c)
        {
            Console.WriteLine("qwert");
            x = a;
            height = b;
            Console.WriteLine($"x = {x}");
            Console.WriteLine($"height = {height}");
            title = c;
        }

    }


    class Analizer
    {
        Type? t;
        MethodInfo[]? mi;
        Random random;
        object[] arg;
        string[] randStr = { "hello", "world", "qwerty" };

        public Analizer()
        {
            random = new Random();
        }

        public void Analize(object obj)
        {
            t = obj.GetType();
            Console.WriteLine("Name of type: {0}:", t.Name);

            if(t.IsClass && !t.IsEnum)
            {
                Console.WriteLine("\nNon public methods:");
                mi = t.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (MethodInfo m in mi)
                {
                    ParameterInfo[] pi = m.GetParameters();
                    arg = new object[pi.Length];
                    if (pi.Length == 0) // если метод без параметров, пошел пошел.
                    m.Invoke(obj, null);
                    else
                    {
                        for (int i = 0; i < pi.Length; i++)
                        {
                            ParameterInfo p = pi[i];
                            if(p.ParameterType.Name == "String")
                            {
                                arg[i] = randStr[random.Next(0, randStr.Length)];
                            }
                            if (p.ParameterType.Name == "Double")
                            {
                                arg[i] = random.NextDouble() * 100;
                            }
                            if (p.ParameterType.Name == "Int32")
                            {
                                arg[i] = random.Next(0,100);
                            }
                            if (p.ParameterType.Name == "Boolean")
                            {
                                int flag = random.Next(0, 2);
                                if (flag == 1)
                                    arg[i] = true;
                                else
                                    arg[i] = false;
                            }
                        }
                        m.Invoke(obj, arg);
                    }
                }

                Console.WriteLine("\nPublic methods:");
                mi = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (MethodInfo m in mi)
                {
                    ParameterInfo[] pi = m.GetParameters();
                    arg = new object[pi.Length];
                    if (pi.Length == 0) // если метод без параметров, пошел пошел.
                        m.Invoke(obj, null);
                    else
                    {
                        for (int i = 0; i < pi.Length; i++)
                        {
                            ParameterInfo p = pi[i];
                            if (p.ParameterType.Name == "String")
                            {
                                arg[i] = randStr[random.Next(0, randStr.Length)];
                            }
                            if (p.ParameterType.Name == "Double")
                            {
                                arg[i] = random.NextDouble() * 100;
                            }
                            if (p.ParameterType.Name == "Int32")
                            {
                                arg[i] = random.Next(0, 100);
                            }
                            if (p.ParameterType.Name == "Boolean")
                            {
                                int flag = random.Next(0, 2);
                                if (flag == 1)
                                    arg[i] = true;
                                else
                                    arg[i] = false;
                            }
                        }
                        m.Invoke(obj, arg);
                    }
                }
            }
        }
    }
}
