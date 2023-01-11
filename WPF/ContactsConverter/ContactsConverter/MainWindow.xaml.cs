using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ContactsConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string? str;
        MatchCollection m2;
        MatchCollection m3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = "vcf";
            dlg.Filter = "vCard files(VCF)|*.vcf";
            dlg.Multiselect = false;

            bool? result = dlg.ShowDialog();
            if(result == true)
            {
                mainDockPanel.Children.Remove(txtHelp);
                str = System.IO.File.ReadAllText(dlg.FileName);
                m2 = Regex.Matches(str, "VERSION:2\\.1");
                m3 = Regex.Matches(str, "VERSION:3\\.0");
                if (m2.Count > 0)
                    AddToListBox_v2();
                else if (m3.Count > 0)
                    AddToListBox_v3();
                else
                    MessageBox.Show("К сожалению ваша версия пока не поддерживается.", "Error");
            }    
        }

        private void AddToListBox_v2()
        {
            lbFile.Items.Clear();
            lbFile.Items.Add("\t\t\t\tVERSION: 2.1\n");
            string[] matches = { "PREF:", "CELL:", "HOME:" };
            MatchCollection m = null;
            foreach (var match in matches)
            {
                m = Regex.Matches(str, match);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < m.Count; i++)
                {
                    int j = m[i].Index + 5;
                    for (; str[j] != 'T' && str[j] != 'E'; j++)
                    {
                        sb.Append(str[j]);
                    }
                    lbFile.Items.Add(sb.ToString());
                    sb.Clear();
                }
            } 
        }

        private void AddToListBox_v3()
        {
            lbFile.Items.Clear();
            lbFile.Items.Add("\t\t\t\tVERSION: 3.0\n");
            MatchCollection m = Regex.Matches(str, "FN:");
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m.Count; i++)
            {
                int j = m[i].Index + 3;
                for (; str[j] != ':'; j++)
                {
                    sb.Append(str[j]);
                }
                sb.Append(str[j]);
                sb.Replace("TEL;TYPE=CELL:", "");
                sb.Replace("TEL;TYPE=CELL;TYPE=PREF:", "");
                j++;
                if(!sb.ToString().EndsWith("END:"))
                {
                    for (; str[j] != 'T' && str[j] != 'E' && str[j] != ':'; j++)
                    {
                        sb.Append(str[j]);
                    }
                    lbFile.Items.Add(sb.ToString());
                }
                sb.Clear();
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (m2.Count > 0)
            {
                str = Regex.Replace(str, ":071", "+7949");
                str = Regex.Replace(str, "\\+38071", "+7949");
                AddToListBox_v2();
            }
               
            else if (m3.Count > 0)
            {
                str = string.Join("", Regex.Replace(str, "071-", "+7949").Split('-'));
                str = Regex.Replace(str, "\\+38071", "+7949");
                AddToListBox_v3();
            }
                
            MessageBox.Show("Сonversion successful", "Сonversion");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = "vcf";
            dlg.Filter = "vCard files(VCF)|*.vcf";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                System.IO.File.WriteAllText(dlg.FileName, str);
            }
        }
    }
}
