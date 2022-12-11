using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {
        private bool suppressSeek;
        Storyboard storyboard;
        public List<string> playlist = new List<string>();
        public int currentSoundPos;
        PlaylistWindow plWind;

        Random rand = new Random();
        private bool isRandom;
        public MainWindow()
        {
            InitializeComponent();
            storyboard = (Storyboard)this.Resources["MediaStoryboardResource"];
            sliderVolume.Value = 1;
            Topmost = true;
            InitPlaylist();
        }

        private void InitPlaylist()
        {
            plWind = new PlaylistWindow(this);
            double screenHeight = SystemParameters.FullPrimaryScreenHeight; // общая высота
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;  // общая ширина
            plWind.Top = (screenHeight - Height - plWind.Height); // расположение окна снизу справа
            plWind.Left = (screenWidth - Width);
            plWind.Topmost = true;
            plWind.Show();
            plWind.Visibility = Visibility.Collapsed;
        }

        private void storyboard_CurrentTimeInvalidated(object? sender, EventArgs e)
        {

            Clock storyboardClock = (Clock)sender;

            if (storyboardClock.CurrentProgress == null)
            {
                txtTime.Text = "";
            }
            else
            {
                // настроить вывод текущего времени проигрывания и позиции слайдера
                txtTime.Text = storyboardClock.CurrentTime.ToString();
                suppressSeek = true;
                sliderPosition.Value = storyboardClock.CurrentTime.Value.TotalSeconds;
                suppressSeek = false;
            }
        }


        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderPosition.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            next_Click(null, null);
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
            mediaElement.Play();
           if(mediaElement.Source != null)
            {
                if (storyboard.GetIsPaused())
                    storyboard.Resume();
                else
                    storyboard.Begin();
            }
           
            txtState.Text = "play";
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            if (playlist.Count > 0)
            {
                if (currentSoundPos > 0)
                {
                    PlaySong(playlist[--currentSoundPos]);
                }
                else if(currentSoundPos == 0)
                {
                    currentSoundPos = playlist.Count - 1;
                    PlaySong(playlist[currentSoundPos]);
                }
            }
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            storyboard.Pause();
            txtState.Text = "pause";
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            storyboard.Pause();
            storyboard.Stop();
            sliderPosition.Value = 0;
            txtState.Text = "stop";
            txtTime.Text = "00:00";
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (playlist.Count > 0)
            {
                if (!isRandom)
                {
                    if (playlist.Count == 1)
                    {
                        PlaySong(playlist[0]);
                    }
                    else if (playlist.Count - 1 > currentSoundPos)
                        PlaySong(playlist[++currentSoundPos]);
                    else if (playlist.Count - 1 == currentSoundPos)
                    {
                        currentSoundPos = 0;
                        PlaySong(playlist[currentSoundPos]);
                    }
                }
                else
                {
                    currentSoundPos = rand.Next(playlist.Count);
                    PlaySong(playlist[currentSoundPos]);
                }
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "mp3 Files (mp3)|*.mp3|wav Files (wav)|*.wav";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                plWind.plListBox.ItemsSource = null;
                PlaySong(dlg.FileName);
                plWind.plListBox.Items.Add(dlg.FileName);
                playlist.Add(dlg.FileName);
            }
        }

        public void PlaySong(string FileName)
        {
            sliderPosition.Value = 0;
            suppressSeek = true;
            storyboard.Stop();
            mediaElement.Stop();
            mediaElement.Clock = null;
            mediaElement.Source = new Uri(FileName, UriKind.Absolute);
            txtFileName.Text = System.IO.Path.GetFileName(FileName);
            txtState.Text = "play";
            MediaTimeline line1 = (MediaTimeline)storyboard.Children[0];
            line1.Source = mediaElement.Source;
            suppressSeek = false;
            storyboard.Begin();
            mediaElement.Play();
        }

        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!suppressSeek)
                storyboard.Seek(TimeSpan.FromSeconds(sliderPosition.Value), TimeSeekOrigin.BeginTime);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mediaElement.Stop();
            plWind.Close();
        }

        private void btnTop_Click(object sender, RoutedEventArgs e)
        {
            if (!Topmost)
            {
                Topmost = true;
                plWind.Topmost = true;
                btnTop.Foreground = Brushes.DarkGreen;
            }
            else
            {
                plWind.Topmost = false;
                Topmost = false;
                btnTop.Foreground = Brushes.DarkRed;
            }
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop) &&
              (e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            playlist.Clear();
            string[] fls = (string[])e.Data.GetData(DataFormats.FileDrop);
            if(e.Data.GetDataPresent(DataFormats.FileDrop) &&
              (e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                foreach(var item in fls)
                {
                    CheckFiles(item);
                }

                if(playlist.Count > 0)
                {
                    plWind.plListBox.ItemsSource = null;
                    plWind.plListBox.Items.Clear();
                    plWind.plListBox.ItemsSource = playlist;
                    PlaySong(playlist[0]);
                }
            }
        }
        private void CheckFiles(string fileName)
        {
            FileAttributes attr = File.GetAttributes(fileName);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                CheckDir(fileName);
            }
            else
            {
                if (fileName.EndsWith(".mp3") || fileName.EndsWith(".wav"))
                {
                    playlist.Add(fileName);
                }

            }
        }
        private void CheckDir(string dirName)
        {
            foreach (string s1 in Directory.GetFiles(dirName))
            {
                if (s1.EndsWith(".mp3") || s1.EndsWith(".wav"))
                    playlist.Add(s1);
            }
            foreach (string s2 in Directory.GetDirectories(dirName))
            {
                CheckDir(s2);
            }
        }

        private void btnPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (plWind.Visibility == Visibility.Visible)
            {
                plWind.Visibility = Visibility.Collapsed;
                btnPlaylist.Foreground = Brushes.DarkRed;
            }
            else
            {
                plWind.Visibility = Visibility.Visible;
                btnPlaylist.Foreground = Brushes.DarkGreen;
            }
                
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            if (isRandom)
            {
                isRandom = false;
                btnRandom.Foreground = Brushes.DarkRed;
            }
            else
            {
                isRandom = true;
                btnRandom.Foreground = Brushes.DarkGreen;
            }
                
        }
    }
}
