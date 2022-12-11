using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace MusicPlayer
{
    public partial class PlaylistWindow : Window
    {
        MainWindow wnd;
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
                    wnd.playlist.Add(fileName);
                    plListBox.Items.Add(fileName);
                }

            }
        }
        private void CheckDir(string dirName)
        {
            foreach (string fileName in Directory.GetFiles(dirName))
            {
                if (fileName.EndsWith(".mp3") || fileName.EndsWith(".wav"))
                {
                    wnd.playlist.Add(fileName);
                    plListBox.Items.Add(fileName);
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
            wnd.PlaySong(plListBox.SelectedItem.ToString());
        }
    }
}
