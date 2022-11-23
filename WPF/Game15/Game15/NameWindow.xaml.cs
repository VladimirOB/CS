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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game15
{
    public delegate void EnterName(string name);
     
    public partial class NameWindow : Window
    {
        public event EnterName ChangeName;

        public NameWindow()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            ChangeName?.Invoke(txtBox.Text);
            Close();
        }
    }
}
