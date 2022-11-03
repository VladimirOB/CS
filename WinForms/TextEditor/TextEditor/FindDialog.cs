using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public delegate void FindDelegate(string find);
    sealed partial class FindDialog : Form
    {
        public event FindDelegate PerformFind;
        private static readonly FindDialog fd = new FindDialog();
        public FindDialog() { }

        public static FindDialog CreateFindDialog()
        {
            fd.InitializeComponent();
            return fd;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            PerformFind?.Invoke(textBoxFind.Text);
        }

        private void FindDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
