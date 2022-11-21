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

namespace EndlessRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

        bool jumping;
        bool gameOver;

        int force = 20;
        int speed = 5;

        Random rand = new Random();

        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int[] obstaclePosition =  {320,310,300,305,315 };

        int score = 0;

        
        public MainWindow()
        {
            InitializeComponent();

            myCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //pack://application:,,,/ не работает
            backgroundSprite.ImageSource = new BitmapImage(new Uri("../../../images/background.gif", UriKind.RelativeOrAbsolute));
          

            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            StartGame();

        }

        private void MoveBackground()
        {
            //движение backgrund
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);

            //если изображение докрутилось, смещаем его на текущее положение 2-го + длина 2-го
            if (Canvas.GetLeft(background) < -1262)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }

            if (Canvas.GetLeft(background2) < -1262)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }
        }

        private void GameEngine(object? sender, EventArgs e)
        {
            MoveBackground();

            // если герой не на земле, он падает с + speed.
            Canvas.SetTop(player, Canvas.GetTop(player) + speed);

            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle)-12);

            scoreText.Content = "Score: " + score;

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            //если игрок столкнулся с землей
            if(playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;

                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);

                jumping = false;

                spriteIndex += .5;

                if(spriteIndex > 11)
                {
                    spriteIndex = 1;
                }

                RunSprite(spriteIndex);
            }

            //если прыгнул, сила прыжка постепенно падает
            if(jumping)
            {
                speed = -9;
                force -= 1;
            }
            else
            {
                speed = 12;
            }

            if(force < 0)
            {
                jumping = false;
            }

            if(Canvas.GetLeft(obstacle) < -50)
            {
                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, obstaclePosition[rand.Next(0, obstaclePosition.Length)]);

                score += 1;
            }

            if(playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;
                gameTimer.Stop();
            }

            //покрасить контуры при поражении
            if(gameOver)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;

                scoreText.Content = "Score: " + score + "\n\n\n\n\t\t  Game Over!\n\t\t  Press enter to Restart.";
            }
            else
            {
                player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space && jumping == false && Canvas.GetTop(player) > 260)
            {
                jumping = true;
                force = 15;
                speed = -12;

                playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/jump.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);

            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 140);

            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("../../../images/obstacle.png", UriKind.RelativeOrAbsolute));
            obstacle.Fill = obstacleSprite;

            jumping = false;
            gameOver = false;
            score = 0;

            scoreText.Content = "Score: " + score;

            gameTimer.Start();
        }

        private void RunSprite(double i)
        {
            switch(i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run1.png", UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run2.png", UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run3.png", UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run4.png", UriKind.RelativeOrAbsolute));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run5.png", UriKind.RelativeOrAbsolute));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run6.png", UriKind.RelativeOrAbsolute));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run7.png", UriKind.RelativeOrAbsolute));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run8.png", UriKind.RelativeOrAbsolute));
                    break;
                case 9:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run9.png", UriKind.RelativeOrAbsolute));
                    break;
                case 10:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run10.png", UriKind.RelativeOrAbsolute));
                    break;
                case 11:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/run11.png", UriKind.RelativeOrAbsolute));
                    break;
               
            }
            player.Fill = playerSprite;
        }
    }
}
