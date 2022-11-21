using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Game15
{
    //2. Реализовать игру пятнашки на WPF при помощи Grid.

    public partial class MainWindow : Window
    {
        const int SIZE = 4;
        const int CELLSIZE = 100;
        Button[,] buttons;
        int[,] map;
        Stack<int> stack;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            buttons = new Button[SIZE, SIZE];
            map = new int[SIZE, SIZE];
            InitStack();
            InitMap();
            InitButtons();
        }

        void InitStack()
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
        void InitMap()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    map[i, j] = stack.Pop();
                }
            }
        }

        void InitButtons()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Button button = InitButton(i, j);
                    myGrid.Children.Add(button);
                    buttons[i, j] = button;
                }
            }
        }

       

        Button InitButton(int i, int j)
        {
            Button button = new Button();
            button.Content = map[i, j].ToString();
            Thickness thickness = new Thickness(0, 0, 0, 0);
            button.FontSize = 36;
            button.Margin = thickness;
            button.Click += Button_Click;
            Grid.SetColumn(button, j);
            Grid.SetRow(button, i);

            if (button.Content.Equals("0"))
            {
                button.Content = "";
                button.IsEnabled = false;
            }
            
            return button;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = sender as Button;

            int colButton = Grid.GetColumn(pressedButton);
            int rowButton = Grid.GetRow(pressedButton);

            if (CheckWin())
            {
                MessageBox.Show("You won!", "Congratulations");
            }

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (map[i, j] == 0) // если здесь пусто
                    {
                        //горизонт
                        for (int k = j - 1; k < j + 2; k += 2)
                        {
                            if (!IsInBorder(i, k)) // если не в границах
                                continue;
                            if (map[i, k] == map[rowButton, colButton])
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

        void SwapButtons(Button source, Button dest)
        {
            dest.Content = source.Content;
            dest.IsEnabled = true;
            source.Content = "";
            source.IsEnabled = false;
        }


        bool CheckWin()
        {
            int temp = 1;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (temp == SIZE * SIZE - 1)
                        return true;

                    if (map[i, j] != temp++)
                        return false;
                }
            }
            return true;
        }

        //Находятся ли координаты в пределах карты
        private bool IsInBorder(int i, int j)
        {
            if (i < 0 || j < 0 || j > SIZE - 1 || i > SIZE - 1)
                return false;
            return true;
        }
    }
}
