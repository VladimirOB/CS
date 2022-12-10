using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayer
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //MainWindow wnd = new Window();
            if (e.Args.Length == 1)
            {
                MusicPlayer.MainWindow.Instance.NewSong(e.Args[0]);
            }
            double screenHeight = SystemParameters.FullPrimaryScreenHeight; // общая высота
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;  // общая ширина
            MusicPlayer.MainWindow.Instance.Top = (screenHeight - MusicPlayer.MainWindow.Instance.Height); // расположение окна снизу справа
            MusicPlayer.MainWindow.Instance.Left = (screenWidth - MusicPlayer.MainWindow.Instance.Width);
            MusicPlayer.MainWindow.Instance.Show();
        }
    }
}
