using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Media.Animation;
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
        double deltaX = -15, deltaY = 10;
        //Необходимы для отслеживания столкновений.
        Rect playerBox, ballBox, fireballBox;
        Rect[][] blockBoxArr;
        Rectangle[][] blockArr;
        TextBlock txtPause;

        bool soundChecker = true, gamePause;
        byte currentLVL;
        byte life = 3;
        //Быстрый способ проверить остаток блоков на уровне. (Для перехода на след. уровень)
        byte currentBlocks;
        int highScore = 0, score = 0;
        float fireballTopPos = 0f;

        SoundPlayer bounce1 = new SoundPlayer();
        SoundPlayer bounce2 = new SoundPlayer();
        SoundPlayer reward = new SoundPlayer();
        SoundPlayer die = new SoundPlayer();
        MediaPlayer music = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            InitMenu();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(!System.IO.File.Exists("score.db"))
            {
                System.IO.File.Create("score.db");
            }
            else
            {
                highScore = Convert.ToInt32(System.IO.File.ReadAllText("score.db"));
                txtHighScore.Text = highScore.ToString();
            }
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(10);
            gameTimer.Tick += GameTimer_Tick;
            txtPause = new TextBlock();
            txtPause.Text = "Pause";
            txtPause.FontSize = 72;
            txtPause.Foreground = Brushes.Black;
            Canvas.SetLeft(txtPause, 700);
            Canvas.SetTop(txtPause, Height / 2);
            mainCanvas.Children.Add(txtPause);
            txtPause.Visibility = Visibility.Collapsed;
            InitSounds();
            if (soundChecker)
                music.Play();
        }

        private void InitSounds()
        {
            try
            {
                bounce1.SoundLocation = "bounce1.wav";
                bounce2.SoundLocation = "bounce2.wav";
                reward.SoundLocation = "reward.wav";
                die.SoundLocation = "gameover.wav";
                music.Open(new Uri("music.mp3", UriKind.Relative));
                music.MediaEnded += Music_MediaEnded;
                bounce1.Load();
                bounce2.Load();
                reward.Load();
                die.Load();
            }
            catch (System.IO.FileNotFoundException err)
            {
            }
            catch (FormatException err)
            {
            }
        }

        private void Music_MediaEnded(object? sender, EventArgs e)
        {
            music.Position = TimeSpan.Zero;
            music.Play();
        }

        private void InitMenu()
        {
            menuWindow menu = new menuWindow();
            menu.saveLoad += Load;
            menu.sound += SoundCheck;
            menu.WindowState = WindowState.Maximized;
            bool? res =  menu.ShowDialog();

            if(res == true)
            {
                if (soundChecker)
                    music.Play();
                Init();
            }
            else
            {
                Close();
            }
        }

        //event с меню
        private void SoundCheck(bool check)
        {
            soundChecker = check;
        }
        //event с меню
        private void Load(byte lv, byte lif)
        {
            currentLVL = lv;
            life = lif;
        }

        private void Init()
        {
            lvlBorder.Visibility = Visibility.Visible;
            currentBlocks = 0;
            Image img;
            switch (currentLVL)
            {
                case 0:
                    img = FindResource("background1") as Image;
                    mainCanvasImage.ImageSource = img.Source;
                    blockArr = null;
                    blockBoxArr = null;
                    blockBoxArr = new Rect[6][];
                    blockArr = new Rectangle[6][];
                    img = FindResource("block1") as Image;
                    for (int i = 0, l = 1; i < blockArr.Length; l++, i++)
                    {
                        blockArr[i] = new Rectangle[12];
                        blockBoxArr[i] = new Rect[12];
                        for (int j = 0, k = 1; j < blockArr[i].Length; k++, j++)
                        {
                            blockArr[i][j] = new Rectangle();
                            blockArr[i][j].Width = 100;
                            blockArr[i][j].Height = 20;
                            blockArr[i][j].Fill = new ImageBrush(img.Source);
                            Canvas.SetLeft(blockArr[i][j], k * 110);
                            Canvas.SetTop(blockArr[i][j], l * 50);
                            mainCanvas.Children.Add(blockArr[i][j]);
                            blockBoxArr[i][j] = new Rect(Canvas.GetLeft(blockArr[i][j]), Canvas.GetTop(blockArr[i][j]), blockArr[i][j].Width, blockArr[i][j].Height);
                            currentBlocks++;
                        }
                    }
                    labelLVL.Content = "Level: 1";
                    txtLVL.Text = "Level 1. Press Left mouse button or Enter to start";
                    break;

                case 1:
                    img = FindResource("background2") as Image;
                    mainCanvasImage.ImageSource = img.Source;
                    blockArr = null;
                    blockBoxArr = null;
                    blockBoxArr = new Rect[14][];
                    blockArr = new Rectangle[14][];
                    img = FindResource("block2") as Image;
                    for (int i = 0, l = 1; i < blockArr.Length; l++, i++)
                    {
                        blockArr[i] = new Rectangle[i+1];
                        blockBoxArr[i] = new Rect[i+1];
                        for (int j = 0, k = 0; j < blockArr[i].Length; k++, j++)
                        {
                            blockArr[i][j] = new Rectangle();
                            blockArr[i][j].Width = 100;
                            blockArr[i][j].Height = 20;
                            blockArr[i][j].Fill = new ImageBrush(img.Source);
                            Canvas.SetLeft(blockArr[i][j], 50 + k * 110);
                            Canvas.SetTop(blockArr[i][j], l * 50);
                            mainCanvas.Children.Add(blockArr[i][j]);
                            blockBoxArr[i][j] = new Rect(Canvas.GetLeft(blockArr[i][j]), Canvas.GetTop(blockArr[i][j]), blockArr[i][j].Width, blockArr[i][j].Height);
                            currentBlocks++;
                        }
                    }
                    labelLVL.Content = "Level: 2";
                    txtLVL.Text = "Level 2. Press Left mouse button or Enter to start";
                    break;

                case 2:
                    img = FindResource("background3") as Image;
                    mainCanvasImage.ImageSource = img.Source;
                    blockArr = null;
                    blockBoxArr = null;
                    blockBoxArr = new Rect[14][];
                    blockArr = new Rectangle[14][];

                    img = FindResource("block3") as Image;
                    for (int i = 0, l = 1; i < blockArr.Length; l++, i++)
                    {
                        blockArr[i] = new Rectangle[14];
                        blockBoxArr[i] = new Rect[14];
                        for (int j = 0, k = 0; j < blockArr[i].Length; k++, j++)
                        {
                            blockArr[i][j] = new Rectangle();
                            blockArr[i][j].Width = 100;
                            blockArr[i][j].Height = 20;
                            blockArr[i][j].Fill = new ImageBrush(img.Source);
                            Canvas.SetLeft(blockArr[i][j], 50 + k * 110);
                            Canvas.SetTop(blockArr[i][j], l * 50);
                            mainCanvas.Children.Add(blockArr[i][j]);
                            blockBoxArr[i][j] = new Rect(Canvas.GetLeft(blockArr[i][j]), Canvas.GetTop(blockArr[i][j]), blockArr[i][j].Width, blockArr[i][j].Height);
                            currentBlocks++;
                        }
                    }
                    labelLVL.Content = "Level: 3";
                    txtLVL.Text = "Level 3. Press Left mouse button or Enter to start";
                    break;
            }
        }

        private void CheckHit()
        {
            for (int i = 0; i < blockArr.Length; i++)
            {
                for (int j = 0; j < blockArr[i].Length; j++)
                {
                    if (ballBox.IntersectsWith(blockBoxArr[i][j]))
                    {

                        if (soundChecker)
                            bounce2.Play();
                        deltaY *= -1;
                        blockBoxArr[i][j] = new Rect(0, 0, 0, 0);
                        mainCanvas.Children.Remove(blockArr[i][j]);
                        currentBlocks--;
                        score += 100;
                        txtScore.Text = score.ToString();
                        if(score % 7000 == 0)
                        {
                            Storyboard sb = FindResource("fireballDrop") as Storyboard;
                            sb.Begin();
                            life++;
                            txtLife.Content = "x" + life;
                            if (soundChecker)
                                reward.Play();
                        }
                        return;
                    }
                }
            }
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(mainCanvas);
            if (mousePos.X > 100 && mousePos.X < 1520)
            {
                if(!gameTimer.IsEnabled && !gamePause)
                {
                    currentPosBall();
                    Canvas.SetLeft(playerRect, mousePos.X - playerRect.Width / 2);
                }
                
                if(gameTimer.IsEnabled && !gamePause)
                Canvas.SetLeft(playerRect, mousePos.X - playerRect.Width / 2);
            }
        }

        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!gameTimer.IsEnabled && !gamePause)
            {
                Start();
            }
            
            e.Handled = true;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if(gameTimer.IsEnabled)
            {
                gamePause = true;
                gameTimer.Stop();
                mainCanvas.Opacity = 0.5;
                txtPause.Visibility = Visibility.Visible;
                gradientBtnPause.Color = Colors.Lime;
                btnTxtPause.Margin = new Thickness(-20, 0, 0, 0);
                btnTxtPause.Text = "Continue";
            }
            else if(!gameTimer.IsEnabled && gamePause)
            {
                gamePause = false;
                mainCanvas.Opacity = 1;
                txtPause.Visibility = Visibility.Collapsed;
                btnTxtPause.Text = "PAUSE";
                gradientBtnPause.Color = Colors.DarkRed;
                btnTxtPause.Margin = new Thickness(0, 0, 0, 0);
                gameTimer.Start();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckFinish()
        {
            if(currentBlocks == 0)
            {
                if (score > highScore)
                {
                    highScore = score;
                    System.IO.File.WriteAllText("score.db", highScore.ToString());
                    txtHighScore.Text = "High Score: " + highScore;
                }

                currentLVL++;
                gameTimer.Stop();
                System.IO.File.WriteAllText("save.db", currentLVL + " " + life);
                Init();
                currentPosBall();
            }
        }

        private void currentPosBall()
        {
            Canvas.SetLeft(ball, Canvas.GetLeft(playerRect) + playerRect.Width / 2);
            Canvas.SetTop(ball, Canvas.GetTop(playerRect) - 30);
            deltaX = -15;
            deltaY = 10;
        }

        private void fireballDrop_Completed(object sender, EventArgs e)
        {
            fireballTopPos = 0;
            fireballBox = new Rect();
        }

        private void fireballDrop_CurrentTimeInt(object sender, EventArgs e)
        {
            fireballTopPos += 2.7f;
            fireballBox = new Rect(Canvas.GetLeft(fireballEllipse), Canvas.GetTop(fireballEllipse) + fireballTopPos, fireballEllipse.Width, fireballEllipse.Height);
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            // Переместить шарик в новые координаты
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) - deltaX);
            Canvas.SetTop(ball, Canvas.GetTop(ball) - deltaY);

            playerBox = new Rect(Canvas.GetLeft(playerRect), Canvas.GetTop(playerRect), playerRect.Width, playerRect.Height);
            ballBox = new Rect(Canvas.GetLeft(ball), Canvas.GetTop(ball), ball.Width, ball.Height);
            CheckHit();
            CheckFinish();

            //мяч коснулся игрока (Вторая проверка для того, чтоб сбоку платформы случайно не попал)
            if (playerBox.IntersectsWith(ballBox) && Canvas.GetTop(ball) + 20 < Canvas.GetTop(playerRect))
            {
                if (soundChecker)
                    bounce1.Play();
                if (Canvas.GetLeft(playerRect) + 50 > Canvas.GetLeft(ball))
                {
                    deltaX += 3;
                }
                else
                    deltaX -= 3;
                deltaY *= -1;

                if (deltaX > 15)
                    deltaX = 15;
                else 
                if (deltaX < -15)
                    deltaX = -15;
            }

            if(Canvas.GetTop(ball) > Height - ball.Height || playerBox.IntersectsWith(fireballBox))  //fireball коснулся игрока
            {
                if (soundChecker)
                    die.Play();
                life--;
                currentPosBall();
                if (life == 0)
                {
                    life = 3;
                    currentLVL = 0;
                   
                    for (int i = 0; i < blockArr.Length; i++)
                    {
                        for (int j = 0; j < blockArr[i].Length; j++)
                        {
                            mainCanvas.Children.Remove(blockArr[i][j]);
                        }
                    }
                    score = 0;
                    txtScore.Text = "Score: 0";
                    MessageBox.Show("Game Over!");  //сделать анимацию Game Over
                    System.IO.File.WriteAllText("save.db", "");
                    music.Stop();
                    InitMenu();
                }
                else
                {
                    MessageBox.Show("Press Left Button Mouse or Enter to restart", "Oops..");
                }
                txtLife.Content = $"x{life}";
                gameTimer.Stop();
            }

            if (Canvas.GetLeft(ball) <= 0 ||
                Canvas.GetLeft(ball) + ball.Width >= Width - tableHighScore.Width - 10)
            {
                deltaX *= -1;
            }
            if (Canvas.GetTop(ball) <= 0 || Canvas.GetLeft(ball) <= 0 && Canvas.GetTop(ball) <= 0)
            {
                deltaY *= -1;
            }
        }

        private void Start()
        {
            gameTimer.Start();
            lvlBorder.Visibility = Visibility.Collapsed;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Escape:
                    btnPause_Click(null, null);
                    break;
                case Key.Enter:
                    if (!gameTimer.IsEnabled && !gamePause)
                    {
                        Start();
                    }
                    break;

                case Key.Left:
                    if (!gameTimer.IsEnabled)
                        Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 50);
                    Canvas.SetLeft(playerRect, Canvas.GetLeft(playerRect) - 50);
                    break;

                case Key.Right:
                    if (!gameTimer.IsEnabled)
                        Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 50);
                    Canvas.SetLeft(playerRect, Canvas.GetLeft(playerRect) + 50);
                    break;
            }
            
        }
    }
}
