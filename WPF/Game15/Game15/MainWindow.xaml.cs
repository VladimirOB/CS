using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Threading;
using System.Xml.Linq;

namespace Game15
{
    //2. Реализовать игру пятнашки на WPF при помощи Grid.
    //Для пятнашек добавить меню, таблицу рекордов в новом окне. Сделать игру по таймеру с подсчётом количеством шагов.
    //Добавить DockPanel

    public partial class MainWindow : Window
    {
        string name;
        DispatcherTimer gameTimer = new DispatcherTimer();
        const int SIZE = 4;
        const int CELLSIZE = 100;
        Button[,] buttons;
        int[,] map;
        Stack<int> stack;
        int steps = 0, time = 0;

        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Tick += GameTimer_Tick;
            Init();
            if(!File.Exists("records.txt"))
            {
                File.Create("records.txt");
            }
        }

        void Init()
        {
            time = 0;
            steps = 0;
            txtTimer.Content = "          Time: 0";
            txtSteps.Content = "          Steps: 0";
            buttons = new Button[SIZE, SIZE];
            map = new int[SIZE, SIZE];
            InitStack();
            InitMap();
            InitButtons();
            gameTimer.Start();
        }

        private void EnterName()
        {
            NameWindow nameWindow = new NameWindow();
            nameWindow.ChangeName += ChangeName;

            nameWindow.ShowDialog();
            if(name == null)
            {
                name = "Guest";
            }
        }

        void ChangeName(string n)
        {
            name = n;
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            if (time == 0)
                EnterName();
            time++;
            txtTimer.Content = "          Time: " + time;
          
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
            button.FontSize = 24;
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
                File.AppendAllText("records.txt", name + " " + time + " " + steps + "\n");
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
            steps++;
            txtSteps.Content = "          Steps: " + steps;
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

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            gameTimer.Stop();
            Init();
        }

        private void Records_Click(object sender, RoutedEventArgs e)
        {

            RecordsWindow records = new RecordsWindow();
            records.Owner = this;

            records.ShowDialog();
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
