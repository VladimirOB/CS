using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
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

namespace FlyingBalls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        System.Windows.Point mousePos;
        int posX, posY;
        int tag = 0;
        List<Image> lstBalls = new List<Image>();
        List<int> sX = new List<int>();
        List<int> sY = new List<int>();
        System.Timers.Timer timer;


        Grid myGrid;
        Label firstNameLabel;
        Label lastNameLabel;
        TextBox firstName;
        TextBox lastName;
        Button submit;
        Button clear;

        public MainWindow()
        {
            InitializeComponent();
            timer =  new System.Timers.Timer(100);
            timer.Elapsed += TimerTick;
            timer.Start();
            InitGrid();
        }
       
        void InitGrid()
        {
            myGrid = new Grid();
            myGrid.Width = 800;
            myGrid.Height = 500;
            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.ShowGridLines = true;
        }

        void CreateControls()
        {
            firstNameLabel = new Label();
            firstNameLabel.Content = "Enter your first name:";
            myGrid.Children.Add(firstNameLabel);

            firstName = new TextBox();
            firstName.Margin = new Thickness(0, 5, 10, 5);
            Grid.SetColumn(firstName, 1);
            myGrid.Children.Add(firstName);

            lastNameLabel = new Label();
            lastNameLabel.Content = "Enter your last name:";
            Grid.SetRow(lastNameLabel, 1);
            myGrid.Children.Add(lastNameLabel);

            lastName = new TextBox();
            lastName.Margin = new Thickness(0, 5, 10, 5);
            Grid.SetColumn(lastName, 1);
            Grid.SetRow(lastName, 1);
            myGrid.Children.Add(lastName);

            submit = new Button();
            submit.Content = "View message";
            Grid.SetRow(submit, 2);
            myGrid.Children.Add(submit);

            clear = new Button();
            clear.Content = "Clear Name";
            Grid.SetRow(clear, 2);
            Grid.SetColumn(clear, 1);
            myGrid.Children.Add(clear);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            //Grid_MouseUp(sender, null);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            var position = Mouse.GetPosition(this);
            mousePos.X = position.X;
            mousePos.Y = position.Y;
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.Height = 100;
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            ellipse.Width = 100;
            ellipse.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);
            //<Ellipse HorizontalAlignment="Left" Height="100" Margin="256,179,0,0" Stroke="Black" VerticalAlignment="Top" Width="100"/>
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}
