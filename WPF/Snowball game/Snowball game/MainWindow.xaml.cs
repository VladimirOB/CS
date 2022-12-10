using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snowball_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //добавить бабку которая будет вылазить либо слевого края либо с правого, в тот момент когда Yeti нет

        //жизни
        Image[] hearts;
        //изначально 3 хп
        private int health = 3;
        // чтоб нельзя было кидать снежки "нонстоп"
        private bool reloadPlayer = false;
        private bool reloadYeti = false, reloadYeti2 = false;
        //отслеживание попадания в цель, чтоб не дублировать анимацию
        private bool hit = false, block = false, block2 = false;
        //очки, начисляются за попадание по Yeti
        private int score = 0;
        //анимация для Yeti
        Storyboard sbYETI;
        Storyboard sbYETI2;
        //таймер для отображения Yeti
        DispatcherTimer gameTimer;
        //таймер для отображения Yeti2
        DispatcherTimer gameTimer2;

        Image yetiSnowball = null;
        Image yetiSnowball2 = null;
        //место щелчка мыши внутри Yeti
        Point mouseClickYeti;

        SoundPlayer dmgSound, hitSound, dieSound, blockSound;
        MediaPlayer mp = new MediaPlayer();

        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateYetiSnowball();
            InitHearts();
            InitSounds();
            InitTimer();
            sbYETI = this.FindResource("sbYeti") as Storyboard;
            sbYETI2 = this.FindResource("sbYeti2") as Storyboard;

        }

        private void InitTimer()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(1250);
            gameTimer.Tick += GameTimer_Tick;

            gameTimer2 = new DispatcherTimer();
            gameTimer2.Interval = TimeSpan.FromMilliseconds(1500);
            gameTimer2.Tick += GameTimer2_Tick;
        }

        private void InitSounds()
        {
            dmgSound = new SoundPlayer();
            hitSound = new SoundPlayer();
            dieSound = new SoundPlayer();
            blockSound = new SoundPlayer();
            try
            {
                dmgSound.SoundLocation = "dmg.wav";
                dmgSound.Load();

                hitSound.SoundLocation = "hit.wav";
                hitSound.Load();

                dieSound.SoundLocation = "die.wav";
                dieSound.Load();

                blockSound.SoundLocation = "block.wav";
                blockSound.Load();
            }
            catch(System.IO.FileNotFoundException err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void InitMusic()
        {
            int r = random.Next(0, 3);
            switch(r)
            {
                case 0:
                    mp.Open(new Uri("1.mp3", UriKind.RelativeOrAbsolute));
                    break;
                case 1:
                    mp.Open(new Uri("2.mp3", UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    mp.Open(new Uri("3.mp3", UriKind.RelativeOrAbsolute));
                    break;
            }
            
            mp.Volume = 0.5;
            mp.Balance = 0;
            mp.Position = new TimeSpan(0, 0, 0);
            mp.SpeedRatio = 1;
            mp.Play();
        }

        private void InitHearts()
        {
            hearts = new Image[3];
            int left = 0;
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i] = new Image();
                hearts[i].Stretch = Stretch.Fill;
                hearts[i].Height = 50;
                hearts[i].Width = 50;
                hearts[i].Source = heartFull.Source;
                Canvas.SetLeft(hearts[i], left);
                Canvas.SetTop(hearts[i], 0);
                left += 50;
                mainCanvas.Children.Add(hearts[i]);
            }

        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            Canvas.SetLeft(yetiImage, random.Next(0, 270));
            Canvas.SetLeft(yetiSnowball, Canvas.GetLeft(yetiImage) / 2);
            Canvas.SetTop(yetiSnowball, Canvas.GetTop(yetiImage) - 250);
            sbYETI.Begin();

        }

        private void GameTimer2_Tick(object? sender, EventArgs e)
        {
            Canvas.SetLeft(yetiImage2, random.Next(325, 650));
            Canvas.SetLeft(yetiSnowball, Canvas.GetLeft(yetiImage2) / 2);
            Canvas.SetTop(yetiSnowball, Canvas.GetTop(yetiImage2) - 250);
            sbYETI2.Begin();
        }

        private void CreateYetiSnowball()
        {
            yetiSnowball = new Image();
            yetiSnowball.Source = snowballImage.Source;
            yetiSnowball.Width = 60;
            yetiSnowball.Height = 60;
            yetiSnowball.RenderTransformOrigin = new Point(0.5, 0.5);
            yetiSnowball.RenderTransform = new ScaleTransform(1, 1);
            yetiSnowball.MouseDown += YetiSnowball_MouseDown;
            mainCanvas.Children.Add(yetiSnowball);
            yetiSnowball.Visibility = Visibility.Collapsed;

            yetiSnowball2 = new Image();
            yetiSnowball2.Source = snowballImage.Source;
            yetiSnowball2.Width = 60;
            yetiSnowball2.Height = 60;
            yetiSnowball2.RenderTransformOrigin = new Point(0.5, 0.5);
            yetiSnowball2.RenderTransform = new ScaleTransform(1, 1);
            yetiSnowball2.MouseDown += YetiSnowball2_MouseDown;
            mainCanvas.Children.Add(yetiSnowball2);
            yetiSnowball2.Visibility = Visibility.Collapsed;
        }

        private void YetiSnowball_MouseDown(object sender, MouseButtonEventArgs e)
        {
            blockSound.Play();
            block = true;
            reloadPlayer = false;
            yetiSnowball.Visibility = Visibility.Collapsed;
            e.Handled = true;
        }

        private void YetiSnowball2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            blockSound.Play();
            block2 = true;
            reloadPlayer = false;
            yetiSnowball2.Visibility = Visibility.Collapsed;
            e.Handled = true;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //если таймер был запущен, он выключается, происходит сброс очков и жизней и отображается окно меню
            if(gameTimer.IsEnabled)
            {
                mp.Stop();
                gameTimer.Stop();
                gameTimer2.Stop();
                txtStart.Visibility = Visibility.Visible;
                borderStart.Visibility = Visibility.Visible;
                txtStart.Text = $"Вы заработали {score} очка(ов) Нажмите Start";
                return;
            }
            score = 0;
            health = 3;
            //возвращаем изображение здоровью
            foreach (var heart in hearts)
            {
                heart.Source = heartFull.Source;
            }
            txtScore.Text = "Score: 0";
            txtStart.Visibility = Visibility.Collapsed;
            borderStart.Visibility = Visibility.Collapsed;
            reloadPlayer = false;
            gameTimer.Start();
            gameTimer2.Start();
            InitMusic();
            //чтоб не отработал тунель (Canvas_MouseDown)
            e.Handled = true;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!reloadPlayer)
            {
                Point coords = e.GetPosition(mainCanvas);
                DoubleAnimation leftAnimation = new DoubleAnimation();
                leftAnimation.From = 370;
                leftAnimation.To = coords.X - 30;
                leftAnimation.Duration = TimeSpan.FromMilliseconds(250);

                //EasingFunctionBase easingFunction = selectEase();
                //easingFunction.EasingMode = EasingMode.EaseOut;

                DoubleAnimation topAnimation = new DoubleAnimation();
                topAnimation.From = 350;
                topAnimation.To = coords.Y - 30;
                //topAnimation.EasingFunction = easingFunction;
                topAnimation.Duration = TimeSpan.FromMilliseconds(250);
                reloadPlayer = true;
                leftAnimation.FillBehavior = FillBehavior.Stop;
                topAnimation.FillBehavior = FillBehavior.Stop;
                topAnimation.Completed += Canvas_MouseDownAnim_Completed;
                snowballImage.BeginAnimation(Canvas.LeftProperty, leftAnimation);
                snowballImage.BeginAnimation(Canvas.TopProperty, topAnimation);

                hitSound.Play();
                //т.к. есть туннелинг, метод отработает и при нажатии на Yeti
            }
        }

        private void Canvas_MouseDownAnim_Completed(object? sender, EventArgs e)
        {
            //если снежок попал в Yeti
            if (hit)
            {
                hit = false;
            }
            else
            {
                reloadPlayer = false;
            }
        }

        private void YetiAnimation_CurrentTimeInvalidated(object? sender, EventArgs e)
        {
            //когда Yeti полностью появился и у него не перезарядка и strategy сработал на выстрел
            if(yetiImage.Opacity == 1 && !reloadYeti)
            {
                reloadYeti = true;
                yetiSnowball.Visibility = Visibility.Visible;
                DoubleAnimation leftAnimation = new DoubleAnimation();
                leftAnimation.From = Canvas.GetLeft(yetiImage)+100; // +100 < чтоб снежок вылетал чётко из руки
                leftAnimation.To = Width/2;
                leftAnimation.Duration = TimeSpan.FromMilliseconds(333);

                DoubleAnimation topAnimation = new DoubleAnimation();
                topAnimation.From = Canvas.GetTop(yetiImage)+100;
                topAnimation.To = Height;
                topAnimation.Duration = TimeSpan.FromMilliseconds(333);
                topAnimation.Completed += yetiSnowball_Completed;
                leftAnimation.FillBehavior = FillBehavior.Stop;
                topAnimation.FillBehavior = FillBehavior.Stop;

                DoubleAnimation scaleX = new DoubleAnimation(1, 100, TimeSpan.FromSeconds(2)); // Попробовать создавать каждый раз новый yetiSnowball
                DoubleAnimation scaleY = new DoubleAnimation(1, 100, TimeSpan.FromSeconds(2));
                yetiSnowball.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleX);
                yetiSnowball.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleY);

                yetiSnowball.BeginAnimation(LeftProperty, leftAnimation);
                yetiSnowball.BeginAnimation(HeightProperty, topAnimation);
            }
        }

        private void YetiAnimation_CurrentTimeInvalidated2(object? sender, EventArgs e)
        {
            //когда Yeti полностью появился и у него не перезарядка и strategy сработал на выстрел
            if (yetiImage2.Opacity == 1 && !reloadYeti2)
            {
                reloadYeti2 = true;
                yetiSnowball2.Visibility = Visibility.Visible;
                DoubleAnimation leftAnimation = new DoubleAnimation();
                leftAnimation.From = Canvas.GetLeft(yetiImage2) + 100; // +100 < чтоб снежок вылетал чётко из руки
                leftAnimation.To = Width / 2;
                leftAnimation.Duration = TimeSpan.FromMilliseconds(333);

                DoubleAnimation topAnimation = new DoubleAnimation();
                topAnimation.From = Canvas.GetTop(yetiImage2) + 100;
                topAnimation.To = Height;
                topAnimation.Duration = TimeSpan.FromMilliseconds(333);
                topAnimation.Completed += yetiSnowball2_Completed;
                leftAnimation.FillBehavior = FillBehavior.Stop;
                topAnimation.FillBehavior = FillBehavior.Stop;

                DoubleAnimation scaleX = new DoubleAnimation(1, 100, TimeSpan.FromSeconds(2)); // Попробовать создавать каждый раз новый yetiSnowball
                DoubleAnimation scaleY = new DoubleAnimation(1, 100, TimeSpan.FromSeconds(2));
                yetiSnowball2.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleX);
                yetiSnowball2.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleY);

                yetiSnowball2.BeginAnimation(LeftProperty, leftAnimation);
                yetiSnowball2.BeginAnimation(HeightProperty, topAnimation);
            }
        }

        private void yetiSnowball_Completed(object? sender, EventArgs e)
        {
            if (!block)
            {
                
                yetiSnowball.Visibility = Visibility.Collapsed;
                health -= 1;
                dmgSound.Play();
                switch (health)
                {
                    case 2:
                        hearts[2].Source = heartNull.Source;
                        break;
                    case 1:
                        hearts[1].Source = heartNull.Source;
                        break;
                    case 0:
                        hearts[0].Source = heartNull.Source;
                        dieSound.Play();
                        Start_Click(null, null);
                        break;
                }
                
               
            }
        }

        private void yetiSnowball2_Completed(object? sender, EventArgs e)
        {
            if (!block2)
            {
                
                yetiSnowball2.Visibility = Visibility.Collapsed;
                health -= 1;
                dmgSound.Play();
                switch (health)
                {
                    case 2:
                        hearts[2].Source = heartNull.Source;
                        break;
                    case 1:
                        hearts[1].Source = heartNull.Source;
                        break;
                    case 0:
                        hearts[0].Source = heartNull.Source;
                        dieSound.Play();
                        Start_Click(null, null);
                        break;
                }
                
            }
        }

      
        private void yeti_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!reloadPlayer)
            {
                txtScore.Text = "Score: " + ++score;
                reloadYeti = true; // чтоб Yeti не кинул снежок
                mouseClickYeti = e.GetPosition(yetiImage);
                hit = true;
            }
        }

        private void yeti2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!reloadPlayer)
            {
                txtScore.Text = "Score: " + ++score;
                reloadYeti2 = true; // чтоб Yeti не кинул снежок
                mouseClickYeti = e.GetPosition(yetiImage);
                hit = true;
            }
        }

        private void yetiAnim_Completed(object sender, EventArgs e)
        {
            block = false;
            reloadYeti = false;
            reloadPlayer = false;
        }

        private void yetiAnim_Completed2(object sender, EventArgs e)
        {
            block2 = false;
            reloadYeti2 = false;
            reloadPlayer = false;
        }
    }
}
