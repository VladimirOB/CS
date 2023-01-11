using System;
using System.Collections.Generic;
using System.Linq;
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

namespace tetris
{
    enum Figure
    {
        figure_1 = 0,
        figure_2 = 1
    }


    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        //временная фигура, которая падает.
        Rectangle[] tempFigure;
        //карта, для отслеживания фигур
        int[,] map;
        Rectangle[,] mapRect;
        bool fastFalling, blockRotate;
        Figure currentFigure;
        byte currentPos = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void InitFigure()
        {
            for (int i = 0; i < tempFigure.Length; i++)
            {
                tempFigure[i].Width = 75;
                tempFigure[i].Height = 50;
                tempFigure[i].Fill = Brushes.Red;
                mainGrid.Children.Add(tempFigure[i]);
            }
           
        }

        private void CreateFigure()
        {
            currentPos = 0;
            currentFigure = Figure.figure_1;
            switch(currentFigure)
            {
                case Figure.figure_1:
                    tempFigure = new Rectangle[4];
                    for (int i = 0; i < tempFigure.Length; i++)
                    {
                        tempFigure[i] = new Rectangle();
                    }
                    InitFigure();
                    Grid.SetRow(tempFigure[0], 0);
                    Grid.SetColumn(tempFigure[0], 5);
                    Grid.SetRow(tempFigure[1], 1);
                    Grid.SetColumn(tempFigure[1], 4);
                    Grid.SetRow(tempFigure[2], 1);
                    Grid.SetColumn(tempFigure[2], 5);
                    Grid.SetRow(tempFigure[3], 1);
                    Grid.SetColumn(tempFigure[3], 6);
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gameTimer.Interval = TimeSpan.FromMilliseconds(500);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
            CreateFigure();
            InitMap();
        }

        private void InitMap()
        {
            map = new int[23,10];
            mapRect = new Rectangle[23, 10];
        }

        private void CheckTetris()
        {
            int cnt = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                cnt = 0;
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 1)
                        cnt++;
                    if (cnt == 10)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            map[i, k] = 0;
                            mainGrid.Children.Remove(mapRect[i, k]);
                        }
                        for (int l = i-1; l > 0; l--) // i - заполненная строка.
                        {
                            for (int h = 9; h >= 0; h--)
                            {
                                if (map[l, h] == 1)
                                {
                                    map[l, h] = 0;
                                    map[l + 1, h] = 1;
                                    Grid.SetRow(mapRect[l, h], l + 1);
                                }
                            }
                        }
                        return;
                    }
                }
            }
        }

        private void FallingFigure_1()
        {
            int column_0 = Grid.GetColumn(tempFigure[0]);
            int row_0 = Grid.GetRow(tempFigure[0]);
            switch (currentPos) // положение фигуры
            {
                case 0:
                    if (Grid.GetRow(tempFigure[1]) == 22)
                    {
                        map[21, column_0] = 1;
                        map[22, column_0 - 1] = 1;
                        map[22, column_0] = 1;
                        map[22, column_0 + 1] = 1;
                        mapRect[21, column_0] = tempFigure[0];
                        mapRect[22, column_0 - 1] = tempFigure[1];
                        mapRect[22, column_0] = tempFigure[2];
                        mapRect[22, column_0 + 1] = tempFigure[3];
                        CreateFigure();
                        return;
                    }
                    else if (map[row_0 + 2, column_0 - 1] == 1 ||
                             map[row_0 + 2, column_0] == 1 ||
                             map[row_0 + 2, column_0 + 1] == 1)
                    {
                        for (int i = 0; i < tempFigure.Length; i++)
                        {
                            map[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i])] = 1;
                            mapRect[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i])] = tempFigure[i];
                        }
                        CreateFigure();
                        return;
                    }
                    break;

                case 1: // Right
                    if (Grid.GetRow(tempFigure[3]) == 22)
                    {
                        map[21, column_0] = 1;
                        map[20, column_0 - 1] = 1;
                        map[21, column_0 - 1] = 1;
                        map[22, column_0 - 1] = 1;
                        mapRect[21, column_0] = tempFigure[0];
                        mapRect[20, column_0 - 1] = tempFigure[1];
                        mapRect[21, column_0 - 1] = tempFigure[2];
                        mapRect[22, column_0 - 1] = tempFigure[3];
                        CreateFigure();
                        return;
                    }
                    else if (map[row_0 + 1, column_0] == 1 ||
                             map[row_0 + 2, column_0 -1] == 1)
                    {
                        for (int i = 0; i < tempFigure.Length; i++)
                        {
                            map[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i])] = 1;
                            mapRect[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i])] = tempFigure[i];
                        }
                        CreateFigure();
                        return;
                    }
                    break;
                case 2: // Down
                    if (Grid.GetRow(tempFigure[0]) == 22)
                    {
                        map[22, column_0] = 1;
                        map[21, column_0 - 1] = 1;
                        map[21, column_0] = 1;
                        map[21, column_0 + 1] = 1;
                        mapRect[22, column_0] = tempFigure[0];
                        mapRect[21, column_0 - 1] = tempFigure[1];
                        mapRect[21, column_0] = tempFigure[2];
                        mapRect[21, column_0 + 1] = tempFigure[3];
                        CreateFigure();
                        return;
                    }
                    else if (map[row_0 + 1, column_0] == 1 ||
                             map[row_0, column_0 - 1] == 1 ||
                             map[row_0, column_0 + 1] == 1)
                    {

                        for (int i = 0; i < tempFigure.Length; i++)
                        {
                            map[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i])] = 1;
                            mapRect[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i])] = tempFigure[i];
                        }
                        CreateFigure();
                        return;
                    }
                    break;

                case 3: // Left
                    if (Grid.GetRow(tempFigure[1]) == 22)
                    {
                        map[21, column_0] = 1;
                        map[20, column_0 + 1] = 1;
                        map[21, column_0 + 1] = 1;
                        map[22, column_0 + 1] = 1;
                        mapRect[21, column_0] = tempFigure[0];
                        mapRect[20, column_0 + 1] = tempFigure[1];
                        mapRect[21, column_0 + 1] = tempFigure[2];
                        mapRect[22, column_0 + 1] = tempFigure[3];
                        CreateFigure();
                        return;
                    }
                    else if (map[row_0 + 1, column_0] == 1 ||
                             map[row_0 + 2, column_0 + 1] == 1)
                    {
                        for (int i = 0; i < tempFigure.Length; i++)
                        {
                            map[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i])] = 1;
                            mapRect[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i])] = tempFigure[i];
                        }
                        CreateFigure();
                        return;
                    }
                    break;
            }
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            if(fastFalling)
            {
                gameTimer.Interval = TimeSpan.FromMilliseconds(50);
            }
            else
            {
                gameTimer.Interval = TimeSpan.FromMilliseconds(500);
            }

            CheckTetris();

            switch(currentFigure) //созданная фигура
            {
                case Figure.figure_1:
                    FallingFigure_1();
                    break;
            }
            Step();
        }

        private void Step()
        {
            for (int i = 0; i < tempFigure.Length; i++)
            {
                Grid.SetRow(tempFigure[i], Grid.GetRow(tempFigure[i]) + 1);
            }
        }

        private void CheckBorder(int n)
        {
            if(n == - 1)
            {
                switch(currentFigure)
                {
                    case Figure.figure_1:
                        switch(currentPos)
                        {
                            case 1:
                                if (map[Grid.GetRow(tempFigure[0]), Grid.GetColumn(tempFigure[0]) + 1] == 1 ||
                                    map[Grid.GetRow(tempFigure[1]), Grid.GetColumn(tempFigure[1]) - 1] == 1 ||
                                    map[Grid.GetRow(tempFigure[3]), Grid.GetColumn(tempFigure[3]) - 1] == 1)
                                {
                                    blockRotate = true;
                                }
                                break;
                        }
                        break;
                }
                for (int i = 0; i < tempFigure.Length; i++) // выборосить цикл, вставить 2 switch
                {
                    if (map[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i]) + 1] == 1 &&
                        map[Grid.GetRow(tempFigure[i]), Grid.GetColumn(tempFigure[i]) - 1] == 1)
                    {
                        blockRotate = true;
                    }
                    
                }
            }
            else
            {
                if (Grid.GetColumn(tempFigure[1]) == 0)
                {
                    for (int i = 0; i < tempFigure.Length; i++)
                    {
                        Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) + 1);
                    }
                }
                else if (map[Grid.GetRow(tempFigure[1]), Grid.GetColumn(tempFigure[1]) - 1] == 1)
                {
                    for (int i = 0; i < tempFigure.Length; i++)
                    {
                        Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) + 1);
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.Left:
                    switch (currentFigure) //созданная фигура
                    {
                        case Figure.figure_1:
                            switch (currentPos) // положение фигуры
                            {
                                case 0: // изначальное положение
                                    if (Grid.GetColumn(tempFigure[1]) > 0 && 
                                        map[Grid.GetRow(tempFigure[1]), Grid.GetColumn(tempFigure[1]) - 1] != 1)
                                    {
                                        for (int i = 0; i < tempFigure.Length; i++)
                                        {
                                            Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) - 1);
                                        }
                                    }    
                                    break;
                                case 1: //right
                                    if (Grid.GetColumn(tempFigure[1]) > 0 &&
                                        map[Grid.GetRow(tempFigure[1]), Grid.GetColumn(tempFigure[1]) - 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[2]), Grid.GetColumn(tempFigure[2]) - 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[3]), Grid.GetColumn(tempFigure[3]) - 1] != 1)
                                    {
                                        for (int i = 0; i < tempFigure.Length; i++)
                                        {
                                            Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) - 1);
                                        }
                                    }
                                    break;
                                case 2: //down
                                    if (Grid.GetColumn(tempFigure[3]) > 0 &&
                                        map[Grid.GetRow(tempFigure[3]), Grid.GetColumn(tempFigure[3]) - 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[0]), Grid.GetColumn(tempFigure[0]) - 1] != 1)
                                    {
                                        for (int i = 0; i < tempFigure.Length; i++)
                                        {
                                            Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) - 1);
                                        }
                                    }
                                    break;
                                case 3: //left
                                    if (Grid.GetColumn(tempFigure[0]) > 0 &&
                                        map[Grid.GetRow(tempFigure[0]), Grid.GetColumn(tempFigure[0]) - 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[1]), Grid.GetColumn(tempFigure[1]) - 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[3]), Grid.GetColumn(tempFigure[3]) - 1] != 1)
                                    {
                                        for (int i = 0; i < tempFigure.Length; i++)
                                        {
                                            Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) - 1);
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                    break;

                case Key.Right:
                    switch (currentFigure) //созданная фигура
                    {
                        case Figure.figure_1:
                            switch (currentPos) // положение фигуры
                            {
                                case 0: // изначальное положение
                                    if (Grid.GetColumn(tempFigure[3]) < 9 &&
                                        map[Grid.GetRow(tempFigure[3]), Grid.GetColumn(tempFigure[3]) + 1] != 1)
                                    {
                                        for (int i = 0; i < tempFigure.Length; i++)
                                        {
                                            Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) + 1);
                                        }
                                    }
                                    break;

                                case 1: //right
                                    if (Grid.GetColumn(tempFigure[0]) < 9 &&
                                        map[Grid.GetRow(tempFigure[0]), Grid.GetColumn(tempFigure[0]) + 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[1]), Grid.GetColumn(tempFigure[1]) + 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[3]), Grid.GetColumn(tempFigure[3]) + 1] != 1)
                                    {
                                        for (int i = 0; i < tempFigure.Length; i++)
                                        {
                                            Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) + 1);
                                        }
                                    }
                                    break;
                                case 2: //down
                                    if (Grid.GetColumn(tempFigure[1]) < 9 &&
                                        map[Grid.GetRow(tempFigure[1]), Grid.GetColumn(tempFigure[1]) + 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[0]), Grid.GetColumn(tempFigure[0]) + 1] != 1)
                                    {
                                        for (int i = 0; i < tempFigure.Length; i++)
                                        {
                                            Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) + 1);
                                        }
                                    }
                                    break;
                                case 3: //left
                                    if (Grid.GetColumn(tempFigure[1]) < 9 &&
                                        map[Grid.GetRow(tempFigure[1]), Grid.GetColumn(tempFigure[1]) + 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[2]), Grid.GetColumn(tempFigure[2]) + 1] != 1 &&
                                        map[Grid.GetRow(tempFigure[3]), Grid.GetColumn(tempFigure[3]) + 1] != 1)
                                    {
                                        for (int i = 0; i < tempFigure.Length; i++)
                                        {
                                            Grid.SetColumn(tempFigure[i], Grid.GetColumn(tempFigure[i]) + 1);
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                    break;

                case Key.Down:
                    fastFalling = true;
                    break;

                case Key.Up:
                    blockRotate = false;
                    CheckBorder(-1);
                    switch (currentFigure)
                    {
                        case Figure.figure_1:
                            currentPos++;
                            if (currentPos > 3)
                                currentPos = 0;
                            switch(currentPos)
                            {
                                case 0:
                                    Grid.SetColumn(tempFigure[0], Grid.GetColumn(tempFigure[0]) + 1);
                                    Grid.SetRow(tempFigure[0], Grid.GetRow(tempFigure[0]) - 1);

                                    Grid.SetColumn(tempFigure[1], Grid.GetColumn(tempFigure[1]) - 1);
                                    Grid.SetRow(tempFigure[1], Grid.GetRow(tempFigure[1]) - 1);

                                    Grid.SetColumn(tempFigure[3], Grid.GetColumn(tempFigure[3]) + 1);
                                    Grid.SetRow(tempFigure[3], Grid.GetRow(tempFigure[3]) + 1);
                                    break;

                                case 1:
                                    Grid.SetColumn(tempFigure[0], Grid.GetColumn(tempFigure[0]) + 1);

                                    Grid.SetColumn(tempFigure[1], Grid.GetColumn(tempFigure[1]) + 1);
                                    Grid.SetRow(tempFigure[1], Grid.GetRow(tempFigure[1]) - 2);

                                    Grid.SetRow(tempFigure[2], Grid.GetRow(tempFigure[2]) - 1);

                                    Grid.SetColumn(tempFigure[3], Grid.GetColumn(tempFigure[3]) - 1);
                                    break;

                                case 2:
                                    Grid.SetColumn(tempFigure[0], Grid.GetColumn(tempFigure[0]) - 1);
                                    Grid.SetRow(tempFigure[0], Grid.GetRow(tempFigure[0]) + 1);

                                    Grid.SetColumn(tempFigure[1], Grid.GetColumn(tempFigure[1]) + 1);
                                    Grid.SetRow(tempFigure[1], Grid.GetRow(tempFigure[1]) + 1);

                                    Grid.SetColumn(tempFigure[3], Grid.GetColumn(tempFigure[3]) - 1);
                                    Grid.SetRow(tempFigure[3], Grid.GetRow(tempFigure[3]) - 1);
                                    break;

                                case 3:
                                    Grid.SetColumn(tempFigure[0], Grid.GetColumn(tempFigure[0]) - 1);
                                    Grid.SetRow(tempFigure[0], Grid.GetRow(tempFigure[0]) - 1);

                                    Grid.SetColumn(tempFigure[1], Grid.GetColumn(tempFigure[1]) - 1);
                                    Grid.SetRow(tempFigure[1], Grid.GetRow(tempFigure[1]) + 1);

                                    Grid.SetColumn(tempFigure[3], Grid.GetColumn(tempFigure[3]) + 1);
                                    Grid.SetRow(tempFigure[3], Grid.GetRow(tempFigure[3]) - 1);
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    fastFalling = false;
                    break;
            }
        }
    }
}


//foreach (var item in checkWin)
//{
//    if(item.Value == 10)
//    {
//        for (int i = 0; i < 10; i++)
//        {
//            map[item.Key, i] = 0;
//            mainGrid.Children.Remove(map2[item.Key, i]);
//            //for (int k = 0; k < mainGrid.Children.Count; k++)
//            //{
//            //    Rectangle rect = mainGrid.Children[k] as Rectangle;
//            //    if (Grid.GetRow(rect) == item.Key)
//            //    {
//            //        mainGrid.Children.Remove(rect);
//            //    }
//            //}
//        }
//        for (int i = item.Key; i > 0; i--) // CheckWin[i] - смещение
//        {
//            for (int j = 9; j >= 0; j--)
//            {
//                if (map[i,j] == 1)
//                {
//                    map[i, j] = 0;
//                    map[i + 1, j] = 1;
//                    Grid.SetRow(map2[i, j], i + 1);
//                    //for (int k = 0; k < mainGrid.Children.Count; k++)
//                    //{
//                    //    Rectangle rect = mainGrid.Children[k] as Rectangle;
//                    //    if (Grid.GetRow(rect) == i)
//                    //    {
//                    //        Grid.SetRow(rect, Grid.GetRow(rect) + 1);
//                    //    }
//                    //}
//                }
//            }
//        }
//        checkWin[item.Key] = 0;
//        return;
//    }
//}