using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game15
{
    static class Game
    {
        static Font btnFont = new Font("Courier New", 26, System.Drawing.FontStyle.Bold); // < Bold = Жирный шрифт
        static FormGame form;
        public static int size = 4;
        const int CELLSIZE = 150;
        static Button[,] buttons;
        public static int[,] map;
        static Stack<int> stack;
        
        public static void Init(FormGame current, int new_size)
        {
            size = new_size;
            buttons = new Button[size, size];
            map = new int[size, size];
            form = current;
            ConfigMapSize(current);
            InitStack();
            InitMap();
            InitButtons(current);
        }


        public static void Load(FormGame current, string file_name)
        {
            form = current;
            StreamReader sr = new StreamReader(file_name);
            size = Convert.ToInt32(sr.ReadLine());
            current.Load();
            buttons = new Button[size, size];
            map = new int[size, size];
            ConfigMapSize(current);
            string temp = sr.ReadToEnd();
            sr.Close();
            string[] arr = temp.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int cnt = 0;
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i,j] = Convert.ToInt32(arr[cnt++]);
                }
            }
            InitButtons(current);
        }

        static void ConfigMapSize(FormGame current)
        {
            current.Width = size * CELLSIZE + 20;
            current.Height = (size  * CELLSIZE)+70;
        }

        static Button InitButton(int i, int j)
        {
            Button button = new Button();
            button.Location = new Point(j * CELLSIZE, (i * CELLSIZE) + 30); // < i - j инверсия
            button.Size = new Size(CELLSIZE, CELLSIZE);
            button.Text = map[i, j].ToString();
            button.ForeColor = Color.Orange;
            button.Font = btnFont;
            button.MouseUp += new MouseEventHandler(OnButtonPressedMouse);
            if (button.Text == "0")
            {
                button.Text = "";
                button.Enabled = false;
            }
            return button;
        }

        static void InitButtons(FormGame current)
        {
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button button = InitButton(i,j);
                    current.Controls.Add(button);
                    buttons[i, j] = button;
                }
            }
        }

        private static void OnButtonPressedMouse(object sender, MouseEventArgs e)
        {
            Button pressedButton = sender as Button;
            int rowButton = pressedButton.Location.Y / CELLSIZE;
            int colButton = pressedButton.Location.X / CELLSIZE;
            if (CheckWin())
            {
                MessageBox.Show("You won!", "Congratulations");
                form.Reset();
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j] == 0) // если здесь пусто
                    {
                        //горизонт
                        for (int k = j - 1; k < j + 2; k+=2)
                        {
                            if (!IsInBorder(i, k)) // если не в границах
                                continue;
                            if(map[i, k] == map[rowButton,colButton])
                            {
                                int t = map[rowButton, colButton];
                                map[rowButton, colButton] = map[i, j];
                                map[i, j] = t;
                                SwapButtons(buttons[rowButton, colButton], buttons[i, j]);
                                return;
                            }
                        }
                        //вертикаль
                        for (int l = i - 1; l < i + 2; l += 2)
                        {
                            if (!IsInBorder(l, j))
                                continue;
                            if (map[l, j] == map[rowButton, colButton])
                            {
                                int t = map[rowButton, colButton];
                                map[rowButton, colButton] = map[i, j];
                                map[i, j] = t;
                                SwapButtons(buttons[rowButton, colButton], buttons[i, j]);
                                return;
                            }
                        }
                    }
                }
            }
        }

        static void SwapButtons(Button source, Button dest)
        {
            dest.Text = source.Text;
            dest.Enabled = true;
            source.Text = "";
            source.Enabled = false;
        }

        static bool CheckWin()
        {
            int temp = 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (temp == size*size-1)
                        return true;

                    if (map[i, j] != temp++)
                        return false;
                }
            }
            return true;
        }

        static void InitStack()
        {
            //stack = new Stack<int>(SIZE * SIZE);
            //stack.Push(0);
            //for (int i = SIZE * SIZE - 1; i > 0; i--)
            //{
            //    stack.Push(i);
            //}

            Random rand = new Random();
            stack = new Stack<int>(size * size);
            int[] numbers = new int[size * size]; // закидываем сюда рандомки, пока не заполним
            while (stack.Count != (size * size))
            {
                int t = rand.Next(0, size * size);
                if (numbers[t] != 1) // если новое число отсутсвует в списке, закидываем.
                {
                    numbers[t] = 1;
                    stack.Push(t); // таким образом получается рандом генерация.
                }
            }
        }

        static void InitMap()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = stack.Pop();
                }
            }
        }

        //Находятся ли координаты в пределах карты
        private static bool IsInBorder(int i, int j)
        {
            if (i < 0 || j < 0 || j > size - 1 || i > size - 1)
                return false;
            return true;
        }
    }
}
