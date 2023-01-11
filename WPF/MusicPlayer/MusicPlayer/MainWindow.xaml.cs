using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
using Microsoft.Win32;
using System.Windows.Interop;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace MusicPlayer
{

    public partial class MainWindow : Window
    {
        private bool suppressSeek, isRandom;
        Storyboard storyboard;
        public List<string> playlist = new List<string>();
        public int currentSoundPos;
        PlaylistWindow plWindow;

        // для загрузки плейлиста из закрытого плеера
        public string playlistPath;

        string memoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MusicPlayer";

       
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);

        private uint _messageId = RegisterWindowMessage("MyUniqueMessageIdentifier");


        Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
            storyboard = (Storyboard)this.Resources["MediaStoryboardResource"];
            sliderVolume.Value = 1;
            Topmost = true;
            if (!System.IO.Directory.Exists(memoryPath) || !System.IO.File.Exists(memoryPath + "\\memory.db"))
            {
                System.IO.Directory.CreateDirectory(memoryPath);
                File.WriteAllText(memoryPath + "\\memory.db", 1.ToString() + "\n"); // громкость

            }
            InitPlaylist();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if(msg == _messageId)
            {
                // The unsafe section where byte pointers are used.
                try
                {
                    //unsafe
                    {
                        //int len = lParam.ToInt32();
                        //string? temp = Marshal.PtrToStringAnsi(wParam, len);
                        //Marshal.FreeHGlobal(wParam);
                        //MessageBox.Show(temp);
                        //ReadProcessMemory(lParam, wParam, buffer, buffer.Length, 24);

                        string fileName = System.IO.File.ReadAllText(memoryPath + "\\buffer.db");
                        if(fileName.EndsWith(".mpbv"))
                        {
                            plWindow.LoadPlaylist(fileName);
                        }
                        else
                        {
                            currentSoundPos = 0;
                            playlist.Clear();
                            plWindow.plListBox.ItemsSource = null;
                            plWindow.plListBox.Items.Clear();
                            plWindow.fullNames.Clear();
                            plWindow.fullNames.Add(fileName);
                            plWindow.plListBox.Items.Add(System.IO.Path.GetFileName(fileName));
                            playlist.Add(fileName);
                            PlaySong(fileName);
                        }
                    }
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return IntPtr.Zero;
        }


        private void InitPlaylist()
        {
            plWindow = new PlaylistWindow(this);
            double screenHeight = SystemParameters.FullPrimaryScreenHeight; // общая высота
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;  // общая ширина
            plWindow.Top = (screenHeight - Height - plWindow.Height + 23); // расположение окна снизу справа
            plWindow.Left = (screenWidth - Width);
            plWindow.Topmost = true;
            plWindow.Show();
            plWindow.Visibility = Visibility.Collapsed;
            
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
                
                txtTime.Text = storyboardClock.CurrentTime.ToString().Substring(0,8);
                suppressSeek = true;
                sliderPosition.Value = storyboardClock.CurrentTime.Value.TotalSeconds;
                suppressSeek = false;
            }
        }


        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if(mediaElement.NaturalDuration.HasTimeSpan)
            sliderPosition.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            next_Click(null, null);
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
            mediaElement.Stop();
            plWindow.Close();
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
           else if(playlist.Count > 0)
           {
                PlaySong(playlist[0]);
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
            dlg.Multiselect = true;

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                playlist.Clear();
                plWindow.plListBox.ItemsSource = null;
                plWindow.plListBox.Items.Clear();
                plWindow.fullNames.Clear();
                foreach (var item in dlg.FileNames)
                { 
                    plWindow.fullNames.Add(item);
                    plWindow.plListBox.Items.Add(System.IO.Path.GetFileName(item));
                    playlist.Add(item);
                }
                PlaySong(dlg.FileName);
            }
        }

        public void PlaySong(string FileName)
        {
            //выделяем выбранную песню в пл
            plWindow.plListBox.SelectedItem = plWindow.plListBox.Items[currentSoundPos];

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
            if (playlist.Count > 0)
            {
                System.IO.File.WriteAllText(memoryPath + "\\sound.db", sliderVolume.Value.ToString());
                StreamWriter sw = new StreamWriter(memoryPath + "\\memory.db");
                foreach (var fileName in playlist)
                {
                    sw.WriteLine(fileName);
                }
                sw.Close();
            }
            mediaElement.Stop();
            plWindow.Close();
        }

        private void btnTop_Click(object sender, RoutedEventArgs e)
        {
            if (!Topmost)
            {
                Topmost = true;
                plWindow.Topmost = true;
                btnTop.Foreground = Brushes.DarkGreen;
            }
            else
            {
                plWindow.Topmost = false;
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
                    plWindow.plListBox.ItemsSource = null;
                    plWindow.plListBox.Items.Clear();
                    plWindow.fullNames = playlist;
                    foreach (var item in playlist)
                    {
                        plWindow.plListBox.Items.Add(System.IO.Path.GetFileName(item));
                    }
                    PlaySong(plWindow.fullNames.FirstOrDefault());
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
            foreach (string fileName in Directory.GetFiles(dirName))
            {
                if (fileName.EndsWith(".mp3") || fileName.EndsWith(".wav"))
                    playlist.Add(fileName);
            }
            foreach (string s2 in Directory.GetDirectories(dirName))
            {
                CheckDir(s2);
            }
        }

        private void btnPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (plWindow.Visibility == Visibility.Visible)
            {
                plWindow.Visibility = Visibility.Collapsed;
                btnPlaylist.Foreground = Brushes.DarkRed;
            }
            else
            {
                plWindow.Visibility = Visibility.Visible;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sliderVolume.Value = Convert.ToDouble(File.ReadLines(memoryPath + "\\sound.db").First());
            //проверка нужна при запуске приложения напрямую открыв плейлист
            if (playlistPath != null)
            {
                plWindow.LoadPlaylist(playlistPath);
            }
            else if(playlist.Count > 0) 
            {
                plWindow.fullNames.Add(playlist[0]);
                plWindow.plListBox.Items.Add(System.IO.Path.GetFileName(playlist[0]));
                PlaySong(playlist[0]);
            }
            //открываются песни, которые были в плеере при закрытии
            else 
            {
                plWindow.LoadPlaylist(memoryPath + "\\memory.db");
            }
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderVolume.Value == 1)
                txtVolume.Text = "Volume: " + "100";

            if (sliderVolume.Value.ToString().Length > 2)
                txtVolume.Text = "Volume: " + (sliderVolume.Value * 100).ToString().Substring(0, 2);
        }

        private void sliderBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(sliderBalance.Value > 0)
            {
                if (sliderBalance.Value.ToString().Length > 3)
                    txtBalance.Text = "Balance: " + sliderBalance.Value.ToString().Substring(0, 3);
                else
                    txtBalance.Text = "Balance: " + sliderBalance.Value.ToString();
            }
            else
            {
                if (sliderBalance.Value.ToString().Length > 4)
                    txtBalance.Text = "Balance: " + sliderBalance.Value.ToString().Substring(0, 4);
                else
                    txtBalance.Text = "Balance: " + sliderBalance.Value.ToString();
            }
            
        }
    }
}
