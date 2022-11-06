using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class FormDialog : Form
    {
        public FormDialog(string name)
        {
            InitializeComponent();
            Text = name;
        }
    }
}
