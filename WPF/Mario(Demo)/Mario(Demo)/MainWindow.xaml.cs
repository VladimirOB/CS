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
using static System.Formats.Asn1.AsnWriter;

namespace Mario_Demo_
{

    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer gameOverTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

        bool jumping;
        bool gameOver;

        bool goLeft, goRight, idle, testL, testR;

        int force = 20;
        int speed = 5;
        int runSpeed = 7;

        Random rand = new Random();

        double spriteRunIndex = 0;
        double spriteIdleIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int time = 30;

        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        private void GameTimerTick(object? sender, EventArgs e)
        {
            time--;
        }

        private void Init()
        {
            myCanvas.Focus();

            gameOverTimer.Tick += GameTimerTick;
            gameOverTimer.Interval = TimeSpan.FromSeconds(1);

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //pack://application:,,,/ не работает
            backgroundSprite.ImageSource = new BitmapImage(new Uri("../../../images/background.gif", UriKind.Relative));


            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            StartGame();
        }

        private void MoveBackground()
        {
            if(goRight && Canvas.GetLeft(player) < 730)
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
            if (goLeft && Canvas.GetLeft(player) > 50)
            {
                //движение backgrund
                Canvas.SetLeft(background, Canvas.GetLeft(background) + 3);
                Canvas.SetLeft(background2, Canvas.GetLeft(background2) + 3);

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

        }


        private void GameEngine(object? sender, EventArgs e)
        {
            MoveBackground();

            // если герой не на земле, он падает с + speed.
            Canvas.SetTop(player, Canvas.GetTop(player) + speed);

            //Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);

            scoreText.Content = "Time: " + time;

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            //если игрок столкнулся с землей
            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;

                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);

                jumping = false;

                //если герой движется
                if(goLeft || goRight)
                {
                    spriteRunIndex += .5;
                    if (spriteRunIndex > 11)
                    {
                        spriteRunIndex = 1;
                    }
                    RunSprite(spriteRunIndex);
                }
            }

            if (idle)
            {
                spriteIdleIndex += .5;

                if (spriteIdleIndex > 6)
                {
                    spriteIdleIndex = 1;
                }
                IdleSprite(spriteIdleIndex);
            }

            if (goRight && Canvas.GetLeft(player) <730)
            {
                ScaleTransform flipTrans = new ScaleTransform();
                flipTrans.ScaleX = 1;
                player.RenderTransform = flipTrans;

                if (testL)
                {
                    Canvas.SetLeft(player, Canvas.GetLeft(player) - 50);
                    testL = false;
                }

                Canvas.SetLeft(player, Canvas.GetLeft(player) + runSpeed);
            }

            if (goLeft && Canvas.GetLeft(player) > 50)
            {
                ScaleTransform flipTrans = new ScaleTransform();
                flipTrans.ScaleX = -1;
                player.RenderTransform = flipTrans;

                if(testR)
                {
                    Canvas.SetLeft(player, Canvas.GetLeft(player) + 50);
                    testR = false;
                }    
                    

                Canvas.SetLeft(player, Canvas.GetLeft(player) - runSpeed);
            }

            //если прыгнул, время прыжка постепенно падает
            if (jumping)
            {
                speed = -9;
                force -= 1;
            }
            else
            {
                speed = 12;
            }

            if (force < 0)
            {
                if (!goLeft && !goRight)
                    idle = true;
                jumping = false;
            }


            if (playerHitBox.IntersectsWith(obstacleHitBox) || time == 0)
            {
                gameOver = true;
                gameTimer.Stop();
                GameOver();
            }
           
        }

        private void GameOver()
        {
            //покрасить контуры при поражении
            if (gameOver)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;

                scoreText.Content = "Time: " + time + "\n\n\n\n\t\t  Game Over!\n\t\t  Press enter to Restart.";
            }
            else
            {
                player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }

            if (e.Key == Key.Left)
            {
                goLeft = true;
                idle = false;
                testL = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
                idle = false;
                testR = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && !jumping && Canvas.GetTop(player) > 260)
            {
                idle = false;
                jumping = true;
                force = 15;
                speed = -12;

                playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/jump.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.Left)
            {
                goLeft = false;
                idle = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
                idle = true;
            }
        }

        private void StartGame()
        {
            Canvas.SetLeft(background, -600);
            Canvas.SetLeft(background2, 662);

            Canvas.SetLeft(player, 100);
            Canvas.SetTop(player, 140);

            Canvas.SetLeft(obstacle, 450);
            Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("../../../images/obstacle.png", UriKind.RelativeOrAbsolute));
            obstacle.Fill = obstacleSprite;

            jumping = false;
            gameOver = false;
            idle = true;
            time = 30;

            scoreText.Content = "Time: " + time;

            gameTimer.Start();
            gameOverTimer.Start();
        }

        private void RunSprite(double i)
        {
            switch (i)
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

        private void IdleSprite(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/idle1.png", UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/idle2.png", UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/idle3.png", UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/idle4.png", UriKind.RelativeOrAbsolute));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/idle5.png", UriKind.RelativeOrAbsolute));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("../../../images/idle6.png", UriKind.RelativeOrAbsolute));
                    break;
            }
            player.Fill = playerSprite;
        }
    }
}
