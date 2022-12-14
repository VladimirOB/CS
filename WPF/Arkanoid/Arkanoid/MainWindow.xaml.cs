using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Arkanoid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer;
        double deltaX = 4, deltaY = 4;
        //Необходимы для отслеживания столкновений.
        Rect playerBox, ballBox;
        Rect[,] blockBoxArr = new Rect[4, 4];
        Rectangle[,] blockArr = new Rectangle[4,4];
        bool motionKeyboard;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(10);
            gameTimer.Tick += GameTimer_Tick;
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < blockArr.GetLength(0); i++)
            {
                for (int j = 0; j < blockArr.GetLength(1); j++)
                {
                    blockArr[i, j] = new Rectangle();
                    blockArr[i, j].Width = 100;
                    blockArr[i, j].Height = 20;
                    blockArr[i, j].Fill = Brushes.OrangeRed;
                    Canvas.SetLeft(blockArr[i, j], j  * blockArr[i, j].Width  * 2);
                    Canvas.SetTop(blockArr[i, j], i  * blockArr[i, j].Height * 2);
                    mainCanvas.Children.Add(blockArr[i, j]);
                    blockBoxArr[i,j] = new Rect(Canvas.GetLeft(blockArr[i, j]), Canvas.GetTop(blockArr[i, j]), blockArr[i, j].Width, blockArr[i, j].Height);
                }
            }
        }

        private void CheckHit()
        {
            for (int i = 0; i < blockArr.GetLength(0); i++)
            {
                for (int j = 0; j < blockArr.GetLength(1); j++)
                {
                    if (ballBox.IntersectsWith(blockBoxArr[i, j]) &&
                        Canvas.GetTop(ball) -10 < Canvas.GetTop(blockArr[i, j]))
                    {
                        deltaY *= -1;
                        blockBoxArr[i,j] = new Rect(0, 0, 0, 0);
                        mainCanvas.Children.Remove(blockArr[i, j]);
                    }
                }
            }
        }

        private void test(object sender, RoutedEventArgs e)
        {
            if (motionKeyboard)
                motionKeyboard = false;
            else
                motionKeyboard = true;
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!motionKeyboard)
            {
                Point mousePos = e.GetPosition(mainCanvas);

                if (!gameTimer.IsEnabled)
                    Canvas.SetLeft(ball, mousePos.X + 50);
                Canvas.SetLeft(playerRect, mousePos.X - playerRect.Width/2);
            }
        }

        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas.SetLeft(ball, Canvas.GetLeft(playerRect) + 140);
            Canvas.SetTop(ball, Canvas.GetTop(playerRect) - 35);
            gameTimer.Start();
            e.Handled = true;
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            // Переместить шарик в новые координаты
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) - deltaX);
            Canvas.SetTop(ball, Canvas.GetTop(ball) - deltaY);

            playerBox = new Rect(Canvas.GetLeft(playerRect), Canvas.GetTop(playerRect), playerRect.Width, playerRect.Height);
            ballBox = new Rect(Canvas.GetLeft(ball), Canvas.GetTop(ball), ball.Width, ball.Height);
          
            CheckHit();
            

            //мяч коснулся игрока
            if (playerBox.IntersectsWith(ballBox) && Canvas.GetTop(ball) + 20 < Canvas.GetTop(playerRect))
            {
                if (Canvas.GetLeft(playerRect) + 50 > Canvas.GetLeft(ball))
                {
                    deltaX += 3;
                }
                else
                    deltaX -= 3;
                deltaY *= -1;

                if (deltaX > 10)
                    deltaX = 10;
            }

            if(Canvas.GetTop(ball) > Height - ball.Height)
            {
                gameTimer.Stop();
                MessageBox.Show("Press Enter to restart", "GAME OVER");
            }

            if (Canvas.GetLeft(ball) < 0 || Canvas.GetLeft(ball) + ball.Width > Width)
            {
                deltaX *= -1;
            }
            if (Canvas.GetTop(ball) < 0)
            {
                deltaY *= -1;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Canvas.SetLeft(ball, Canvas.GetLeft(playerRect) + 140);
                Canvas.SetTop(ball, Canvas.GetTop(playerRect) - 35);
                gameTimer.Start();
            }
            switch(e.Key)
            {
                case Key.Enter:
                    gameTimer.Start();
                break;

                case Key.Left:
                    if(motionKeyboard)
                    {
                        if (!gameTimer.IsEnabled)
                            Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 15);
                        Canvas.SetLeft(playerRect, Canvas.GetLeft(playerRect) - 15);
                    }
                    
                    break;

                case Key.Right:
                    if (motionKeyboard)
                    {
                        if (!gameTimer.IsEnabled)
                            Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 15);
                        Canvas.SetLeft(playerRect, Canvas.GetLeft(playerRect) + 15);
                    }
                    break;
            }
            
        }
    }
}
