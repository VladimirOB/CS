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
            MainWindow wnd = new MainWindow();

            if (e.Args.Length == 1) 
            {
                wnd.playlistPath = e.Args[0];
            }
            double screenHeight = SystemParameters.FullPrimaryScreenHeight; // общая высота
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;  // общая ширина
            wnd.Top = (screenHeight - wnd.Height); // расположение окна снизу справа
            wnd.Left = (screenWidth - wnd.Width);
            wnd.Show();
        }
    }
}
