﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game15
{
    static class Game
    {
        static FormGame form;
        const int SIZE = 5;
        const int CELLSIZE = 100;
        static Button[,] buttons = new Button[SIZE, SIZE];
        static int[,] map = new int[SIZE, SIZE];
        static Stack<int> stack;
        
        public static void Init(FormGame current)
        {
            form = current;
            ConfigMapSize(current);
            InitStack();
            InitMap();
            InitButtons(current);
        }

        static void ConfigMapSize(FormGame current)
        {
            current.Width = SIZE * CELLSIZE + 20;
            current.Height = (SIZE+1)  * CELLSIZE;
        }

        static void InitButtons(FormGame current)
        {
            Font btnFont =  new Font("Courier New", 22, System.Drawing.FontStyle.Bold); // < Bold = Жирный шрифт
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * CELLSIZE, i * CELLSIZE); // < i - j инверсия
                    button.Size = new Size(CELLSIZE, CELLSIZE);
                    button.Text = map[i, j].ToString();
                    button.ForeColor = Color.Orange;
                    button.Font = btnFont;
                    button.MouseUp += new MouseEventHandler(OnButtonPressedMouse);
                    current.Controls.Add(button);
                    buttons[i, j] = button;

                    if (map[i, j] == 0)
                    {
                        buttons[i, j].Text = "";
                        buttons[i, j].Enabled = false;
                    }
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
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
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
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (temp == SIZE*SIZE-1)
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
            stack = new Stack<int>(SIZE * SIZE);
            int[] numbers = new int[SIZE * SIZE]; // закидываем сюда рандомки, пока не заполним
            while (stack.Count != (SIZE * SIZE))
            {
                int t = rand.Next(0, SIZE * SIZE);
                if (numbers[t] != 1) // если новое число отсутсвует в списке, закидываем.
                {
                    numbers[t] = 1;
                    stack.Push(t); // таким образом получается рандом генерация.
                }
            }
        }

        static void InitMap()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    map[i, j] = stack.Pop();
                }
            }
        }

        //Находятся ли координаты в пределах карты
        private static bool IsInBorder(int i, int j)
        {
            if (i < 0 || j < 0 || j > SIZE - 1 || i > SIZE - 1)
                return false;
            return true;
        }
    }
}
