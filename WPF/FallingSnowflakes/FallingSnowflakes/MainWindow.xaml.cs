using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FallingSnowflakes
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timerSnowflakes = new DispatcherTimer();
        DispatcherTimer timerTractor = new DispatcherTimer();
        Random rand = new Random();

        Image snowflake = null;
        Point mousePos;

        //нужно для корректного удаления снежинок
        //Queue<Image> queue = new Queue<Image>();
        List<Image> lstImages = new List<Image>();
        //максимальное кол-во снежинок, потом нужно чистить снег
        int cntOfSnowflakes = 48;

        //цвет текста и трасформа для анимации
        SolidColorBrush brush = new SolidColorBrush(Colors.Red);
        ScaleTransform scale = new ScaleTransform();
        TextBox txtAlarm = null;
        bool txtStart;

        Storyboard sbDED;
        List<Storyboard> storyboards = new List<Storyboard>();

        public MainWindow()
        {
            InitializeComponent();
            timerTractor.Interval = TimeSpan.FromMilliseconds(100);
            timerTractor.Tick += TimerTractor_Tick;
            timerSnowflakes.Interval = TimeSpan.FromMilliseconds(500);
            timerSnowflakes.Tick += Timer_Tick;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           sbDED = this.FindResource("StoryBoardDED") as Storyboard;
        }

        private EasingFunctionBase selectEase()
        {
            EasingFunctionBase easingFunction = null;
            int n = rand.Next(0, 11);
            switch (3)
            {
                case 0:
                    easingFunction = new BackEase();

                    // амплитуда (сила) смещения назад
                    ((BackEase)easingFunction).Amplitude = 0.1;
                    break;

                case 1:
                    easingFunction = new ElasticEase();
                    ((ElasticEase)easingFunction).Springiness = 1;
                    ((ElasticEase)easingFunction).Oscillations = 1;
                    break;

                case 2:
                    easingFunction = new SineEase();
                    break;

                case 3:
                    easingFunction = new BounceEase();
                    // количество отскоков
                    ((BounceEase)easingFunction).Bounces = 0;

                    // амплитуда отскока
                    ((BounceEase)easingFunction).Bounciness = 0.1;
                    break;

                case 4:
                    easingFunction = new QuadraticEase();
                    break;

                case 5:
                    easingFunction = new CubicEase();
                    break;

                case 6:
                    easingFunction = new QuarticEase();
                    break;

                case 7:
                    easingFunction = new QuinticEase();
                    break;

                case 8:
                    easingFunction = new PowerEase();
                    ((PowerEase)easingFunction).Power = 1;
                    break;

                case 9:
                    easingFunction = new ExponentialEase();
                    ((ExponentialEase)easingFunction).Exponent = 1;
                    break;

                case 10:
                    easingFunction = new CircleEase();
                    break;

                default:
                    break;
            }
            return easingFunction;
        }
        private Image createSnowflake()
        {
            Image img = new Image();
            img.Source = snowflakeImage.Source;
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            img.Width = 25;
            img.Height = 25;
            Panel.SetZIndex(img, 2);
            Canvas.SetLeft(img, mousePos.X);
            Canvas.SetTop(img, mousePos.Y);
            mainCanvas.Children.Add(img);
            return img;
        }

        private void createAnim()
        {  
            DoubleAnimation leftAnimation = new DoubleAnimation();
            leftAnimation.From = 0;
            leftAnimation.To = rand.Next((int)mousePos.X - 250, (int)mousePos.X + 250);
            leftAnimation.Duration = TimeSpan.FromSeconds(5);

            EasingFunctionBase easingFunction = selectEase();
            easingFunction.EasingMode = EasingMode.EaseOut;

            DoubleAnimation topAnimation = new DoubleAnimation();
            topAnimation.From = 0;
            topAnimation.To = Height * 2;
            topAnimation.EasingFunction = easingFunction;
            topAnimation.Duration = TimeSpan.FromSeconds(5);
            topAnimation.Completed += TopAnimation_Completed;

            //snowflake.BeginAnimation(LeftProperty, leftAnimation);
            //snowflake.BeginAnimation(HeightProperty, topAnimation);

            Storyboard storyboardSnowFlake = new Storyboard();
            //storyboardSnowFlake.Children.Add(leftAnimation);
            //Storyboard.SetTargetProperty(leftAnimation, new PropertyPath(WidthProperty));
            storyboardSnowFlake.Children.Add(topAnimation);
            Storyboard.SetTargetProperty(topAnimation, new PropertyPath(HeightProperty));
            storyboardSnowFlake.Begin(snowflake, true);
            storyboards.Add(storyboardSnowFlake);
        }

        private void createTxtAlarm()
        {
            sbDED.Resume();
            txtStart = true;
            if (txtAlarm == null)
            {
                txtAlarm = new TextBox();
                txtAlarm.BorderBrush = Brushes.Transparent;
                txtAlarm.FontSize = 36;
                txtAlarm.Foreground = brush;
                txtAlarm.Background = Brushes.Transparent;
                txtAlarm.TextWrapping = TextWrapping.Wrap;
                txtAlarm.Width = 300;
                Canvas.SetTop(txtAlarm, 115);
                Canvas.SetLeft(txtAlarm, 500);
                Panel.SetZIndex(txtAlarm, 2);
                txtAlarm.Text = "Михалыыыыч! Не спи!";
                mainCanvas.Children.Add(txtAlarm);
                scale.ScaleX = 1;
                scale.ScaleY = 1;
                txtAlarm.RenderTransform = scale;
            }
            txtAlarm.Visibility = Visibility.Visible;


            ColorAnimation colorAnimation = new ColorAnimation(Colors.Red, Colors.Purple, TimeSpan.FromMilliseconds(2000));
            colorAnimation.AutoReverse = true;
            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Анимация размера
            DoubleAnimation dx = new DoubleAnimation(1, 2, TimeSpan.FromMilliseconds(2000));
            DoubleAnimation dy = new DoubleAnimation(1, 2, TimeSpan.FromMilliseconds(2000));
            dx.AutoReverse = true;
            dy.AutoReverse = true;
            dx.Completed += txtAnim_Completed;
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, dx);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, dy);
        }

        private void txtAnim_Completed(object? sender, EventArgs e)
        {
            txtAlarm.Visibility = Visibility.Collapsed;
            timerTractor.Start();
        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(cntOfSnowflakes > 0)
            {
                if(cntOfSnowflakes % 25 == 0)
                {
                    sbDED.Begin(); // каждую 25 снежинку выезжает дед
                }

                mousePos = e.GetPosition(mainCanvas);
                snowflake = createSnowflake();
                lstImages.Add(snowflake);
                cntOfSnowflakes--;
                createAnim();
            }
            else
            {
                if(!txtStart)
                createTxtAlarm();
            }
           
        }

        private void TimerTractor_Tick(object? sender, EventArgs e)
        {
            //проверка на то, что все снежинки исчезли
            if(lstImages.Count == 0)
            {
                if (Canvas.GetLeft(tractor) > -559)
                {
                    Canvas.SetLeft(tractor, Canvas.GetLeft(tractor) - 10);
                    if (Canvas.GetLeft(tractor) < 900)
                    {
                        Canvas.SetLeft(snow, Canvas.GetLeft(snow) - 10);
                    }
                }
                else
                {
                    //возвращаем всё на исходные
                    Canvas.SetLeft(tractor, 1205);
                    Canvas.SetLeft(snow, - 178);
                    Canvas.SetTop(snow, 562);
                    cntOfSnowflakes = 48;
                    timerTractor.Stop();
                    txtStart = false;
                }
            }
        }

        private void TopAnimation_Completed(object? sender, EventArgs e)
        {
            //когда снежинка упала, она удаляется и уровень снега растёт
            mainCanvas.Children.Remove(lstImages.First());
            lstImages.RemoveAt(0);
            storyboards.RemoveAt(0);
            Canvas.SetTop(snow, Canvas.GetTop(snow) - 5);
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (timerSnowflakes.IsEnabled)
            {
                timerSnowflakes.Stop();
                return;
            }
            timerSnowflakes.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (cntOfSnowflakes > 0)
            {
                mousePos.X = rand.Next(10, 1000);
                mousePos.Y = rand.Next(10, 100);
                snowflake = createSnowflake();
                lstImages.Add(snowflake);
                cntOfSnowflakes--;
                createAnim();
            }
            else
            {
                if(!txtStart)
                createTxtAlarm();
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(sbSnowflake.Children[1].ToString());
            sbDED.Pause();

            for (int i = 0; i < storyboards.Count; i++)
            {
                storyboards[i].Pause(lstImages[i]);
            }
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            sbDED.Resume();
            for (int i = 0; i < storyboards.Count; i++)
            {
                storyboards[i].Resume(lstImages[i]);
            }

        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            sbDED.Stop();
            int i = storyboards.Count-1;
            while (storyboards.Count > 0)
            { 
                mainCanvas.Children.Remove(lstImages[i]);
                lstImages.RemoveAt(i);
                storyboards.RemoveAt(i);
                i--;
            }
        }
    }
}
