using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Game15
{
    public partial class FormMenu : Form
    {
        private int size = 4;
        public FormMenu()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            size = 4;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            size = 5;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            size = 6;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            FormGame game = new FormGame(size);
            game.Show();
            Close();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMenu_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void FormMenu_Move(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.SizeAll;
        }
    }
}
