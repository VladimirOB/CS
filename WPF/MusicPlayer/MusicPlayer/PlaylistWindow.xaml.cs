using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MusicPlayer
{
    public partial class PlaylistWindow : Window
    {
        MainWindow wnd;
        // можно хранить List с FullName, а в LB показывать только Name
        public List<string> fullNames = new List<string>();

        public PlaylistWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            wnd = mainWindow;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            wnd.btnPlaylist.Foreground = Brushes.DarkRed;
            Visibility = Visibility.Collapsed;
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
             (e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            string[] fls = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
              (e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                foreach (var item in fls)
                {
                    CheckFiles(item);
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
                    fullNames.Add(fileName);
                    plListBox.Items.Add(System.IO.Path.GetFileName(fileName));
                    wnd.playlist.Add(fileName);
                }
            }
        }
        private void CheckDir(string dirName)
        {
            foreach (string fileName in Directory.GetFiles(dirName))
            {
                if (fileName.EndsWith(".mp3") || fileName.EndsWith(".wav"))
                {
                    fullNames.Add(fileName);
                    plListBox.Items.Add(System.IO.Path.GetFileName(fileName));
                    wnd.playlist.Add(fileName);
                }
                    
            }
            foreach (string s2 in Directory.GetDirectories(dirName))
            {
                CheckDir(s2);
            }
        }

        private void borderTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            wnd.currentSoundPos = plListBox.SelectedIndex;
            wnd.PlaySong(fullNames[plListBox.SelectedIndex]);
        }

        private void savePL_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".mpbv";
            dlg.Filter = "mpbv Files (playlist)|*.mpbv";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                StreamWriter sw = new StreamWriter(dlg.FileName);
                foreach (var item in fullNames)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
        }

        private void openPL_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mpbv";
            dlg.Filter = "mpbv Files (playlist)|*.mpbv";
            dlg.Multiselect = false;

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                LoadPlaylist(dlg.FileName);
            }
        }

        public void LoadPlaylist(string path)
        {
            StreamReader sr = new StreamReader(path);
            string? line;
            wnd.playlist.Clear();
            plListBox.ItemsSource = null;
            plListBox.Items.Clear();
            fullNames.Clear();

            while ((line = sr.ReadLine()) != null)
            {
                fullNames.Add(line);
                plListBox.Items.Add(System.IO.Path.GetFileName(line));
                wnd.playlist.Add(line);
            }
            sr.Close();
            if(fullNames.Count > 0)
            {
                plListBox.SelectedItem = plListBox.Items[0];
                wnd.PlaySong(fullNames[0]);
            }
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                DeleteSound();
            }
        }

        private void DeleteSound()
        {
            int index = plListBox.SelectedIndex;
            if (index != wnd.currentSoundPos && index != -1)
            {
                if (index < wnd.currentSoundPos)
                    wnd.currentSoundPos--;
                plListBox.Items.RemoveAt(index);
                wnd.playlist.RemoveAt(index);
                fullNames.RemoveAt(index);
                if (wnd.playlist.Count == 1)
                    wnd.currentSoundPos = 0;
            }
            else
            {
                Storyboard sb = FindResource("StoryBoard1") as Storyboard;
                sb.Begin();
            }
        }

        private void contextItemDelete_click(object sender, RoutedEventArgs e)
        {
            DeleteSound();
        }
    }
}
