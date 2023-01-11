using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Arkanoid
{

    public delegate void SaveLoad(byte lvl, byte life);
    public delegate void SoundCheck(bool sound);

    public partial class menuWindow : Window
    {
        public event SaveLoad saveLoad;
        public event SoundCheck sound;
        string file;
        bool soundCheck = true;
        MediaPlayer music = new MediaPlayer();
        public menuWindow()
        {
            InitializeComponent();
            if (!System.IO.File.Exists("save.db"))
                System.IO.File.Create("save.db");
            else
            {
                file =  System.IO.File.ReadAllText("save.db");
                if(file.Length > 0)
                {
                    btnContinue.Visibility = Visibility.Visible;
                }
            }
            InitSound();
        }

        private void InitSound()
        {
            try
            {
                music.Open(new Uri("menu.mp3", UriKind.Relative));
                music.Play();
            }
            catch (System.IO.FileNotFoundException err)
            {
            }
            catch (FormatException err)
            {
            }
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            string[] lvlLife = file.Split(' ');
            saveLoad?.Invoke(Convert.ToByte(lvlLife[0]), Convert.ToByte(lvlLife[1]));
            sound.Invoke(soundCheck);
            music.Stop();
            DialogResult = true;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            sound.Invoke(soundCheck);
            music.Stop();
            DialogResult = true;
        }

        private void btnSound_Click(object sender, RoutedEventArgs e)
        {
            if (soundCheck)
            {
                soundCheck = false;
                gradientBtnPause.Color = Colors.DarkRed;
                btnTxtPause.Text = "OFF";
                music.Stop();
            }
            else
            {
                soundCheck = true;
                btnTxtPause.Text = "ON";
                gradientBtnPause.Color = Colors.Lime;
                music.Play();
            }
        }
    }
}
