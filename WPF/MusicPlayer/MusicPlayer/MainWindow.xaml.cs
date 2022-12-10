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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        MediaPlayer mp = new MediaPlayer();
        
        private bool suppressSeek;

        DispatcherTimer playTimer;
        int currentPlayTime = 0;

        static MainWindow()
        {
            Instance = new MainWindow();
        }

        private MainWindow()
        {
            InitializeComponent();
            InitMediaPlayer();
            //BindingVolume();
            playTimer = new DispatcherTimer();
            playTimer.Interval = TimeSpan.FromSeconds(1);
            playTimer.Tick += PlayTimer_Tick;
        }

        private void PlayTimer_Tick(object? sender, EventArgs e)
        {

            if (sliderPosition.Value == sliderPosition.Maximum)
            {
                playTimer.Stop();
                return;
            }
            currentPlayTime += 1;
            sliderPosition.Value = currentPlayTime;

            //txtTime.Text = currentPlayTime.ToString();
        }

        private void InitMediaPlayer()
        {
            mp.MediaOpened += Mp_MediaOpened;
            mp.Volume = 1;
            mp.Balance = 0;
            mp.SpeedRatio = 1;
        }

        private void Mp_MediaOpened(object? sender, EventArgs e)
        {
            sliderPosition.Maximum = mp.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void BindingVolume()
        {
            Binding binding = new Binding();
            binding.ElementName = "mp"; // элемент-источник
            binding.Path = new PropertyPath("Volume"); // свойство элемента-источника
            binding.Mode = BindingMode.TwoWay;
            sliderVolume.SetBinding(MediaElement.VolumeProperty, binding); // установка привязки для элемента-приемника
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void collapse_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void borderTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            e.Handled = true;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            mp.Play();
            if(!playTimer.IsEnabled)
            playTimer.Start();
            txtState.Text = "play";
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            mp.Pause();
            if (playTimer.IsEnabled)
                playTimer.Stop();
            txtState.Text = "pause";
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            mp.Stop();
            if (playTimer.IsEnabled)
                playTimer.Stop();
            currentPlayTime = 0;
            sliderPosition.Value = 0;
            txtState.Text = "stop";
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "mp3 Files (mp3)|*.mp3|wav Files (wav)|*.wav";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                sliderPosition.Value = 0;
                currentPlayTime = 0;
                if (!playTimer.IsEnabled)
                    playTimer.Start();
                NewSong(dlg.FileName);
            }
        }

        public void NewSong(string FileName)
        {
            mp.Open(new Uri(FileName, UriKind.Absolute));
            txtFileName.Text = System.IO.Path.GetFileName(FileName);
            txtState.Text = "play";
            mp.Position = new TimeSpan(0, 0, 0);
            // настройка слайдера перемотки
            mp.Play();
        }

        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mp.Position = TimeSpan.FromSeconds(sliderPosition.Value);
            currentPlayTime = (int)sliderPosition.Value;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mp.Stop();
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mp.Volume = sliderVolume.Value;
        }
    }
}
