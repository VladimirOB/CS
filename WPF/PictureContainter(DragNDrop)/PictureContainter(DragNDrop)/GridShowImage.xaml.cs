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

namespace PictureContainter_DragNDrop_
{
    /// <summary>
    /// Логика взаимодействия для GridShowImage.xaml
    /// </summary>
    public partial class GridShowImage : Window
    {
        public GridShowImage(int width, int height)
        {
            InitializeComponent();
            Width = width;
            Height = height;
        }
    }
}
