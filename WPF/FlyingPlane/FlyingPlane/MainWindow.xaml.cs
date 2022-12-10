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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlyingPlane
{
    /*3. В окне программы в середине окна находится спрайт (самолётик, Микки-Маус). 
     * Пользователь мышью может указывать координаты в окне. 
     * При нажатии на кнопку Start спрайт движется по кривой, заданной введенными координатами. 
     * Движение организовать при помощи фреймовой анимации (discreet, linear, spline)*/

    public partial class MainWindow : Window
    {
        List<Point> lstSteps = new List<Point>();
        // текущее положение самолёта
        Point currentPlanePos;

        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            currentPlanePos.X = Canvas.GetLeft(image);
            currentPlanePos.Y = Canvas.GetTop(image);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // Анимация с использованием фреймов (кадров)
            DoubleAnimationUsingKeyFrames dx = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames dy = new DoubleAnimationUsingKeyFrames();

            byte n = (byte)rand.Next(0, 3);
            switch (n)
            {
                case 0:
                    // Задание линейных кадров (значение, время)
                    for (int i = 0; i < lstSteps.Count; i++)
                    {
                        dx.KeyFrames.Add(new LinearDoubleKeyFrame(lstSteps[i].X, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(i+1))));
                        dy.KeyFrames.Add(new LinearDoubleKeyFrame(lstSteps[i].Y, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(i+1))));
                    }
                    break;

                case 1:
                    for (int i = 0; i < lstSteps.Count; i++)
                    {
                        dx.KeyFrames.Add(new DiscreteDoubleKeyFrame(lstSteps[i].X, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(i + 1))));
                        dy.KeyFrames.Add(new DiscreteDoubleKeyFrame(lstSteps[i].Y, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(i + 1))));
                    }

                    break;

                case 2:
                    // Задание поведения по окончании анимации
                    //dx.FillBehavior = FillBehavior.HoldEnd;
                    //dy.FillBehavior = FillBehavior.HoldEnd;

                    for (int i = 0; i < lstSteps.Count; i++)
                    {
                        dx.KeyFrames.Add(new SplineDoubleKeyFrame(lstSteps[i].X, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(i + 1)), new KeySpline(0.9, 0.1, 0.1, 0.9)));
                        dy.KeyFrames.Add(new SplineDoubleKeyFrame(lstSteps[i].Y, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(i + 1)), new KeySpline(0.9, 0.1, 0.1, 0.9)));
                    }
                    break;
            }
            // Запустить метод по окончании анимации
            dx.Completed += Dx_Completed;
            dx.Duration = TimeSpan.FromSeconds(lstSteps.Count);
            dy.Duration = TimeSpan.FromSeconds(lstSteps.Count);

            // Запуск анимаций
            image.BeginAnimation(Canvas.LeftProperty, dx);
            image.BeginAnimation(Canvas.TopProperty, dy);
            
        }

        private void Dx_Completed(object? sender, EventArgs e)
        {
            currentPlanePos.X = Canvas.GetLeft(image);
            currentPlanePos.Y = Canvas.GetTop(image);
            lstSteps.Clear();

            // очистка точек траектории
            int index = mainCanvas.Children.Count - 1;
            while (index >= 0)
            {
                var item = mainCanvas.Children[index--];
                if (item is Ellipse && ((Ellipse)item).Width == 5)
                    mainCanvas.Children.Remove(item);
            }
        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            Canvas.SetLeft(ellipse, e.GetPosition(mainCanvas).X);
            Canvas.SetTop(ellipse, e.GetPosition(mainCanvas).Y);
            ellipse.Stroke = Brushes.Black;
            ellipse.Fill = Brushes.Black;
            ellipse.Width = 5;
            ellipse.Height = 5;
            mainCanvas.Children.Add(ellipse);
            lstSteps.Add(e.GetPosition(mainCanvas));
        }
    }
}
