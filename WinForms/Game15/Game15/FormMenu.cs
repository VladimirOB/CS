using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game15
{
    public partial class FormMenu : Form
    {
        protected FormGame formGame;
        public FormMenu(FormGame formGame)
        {
            InitializeComponent();
            this.formGame = formGame;
            Text = "Main menu";
            Width = 900;
            Height = 635;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Game.size = 4;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Game.size = 5;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Game.size = 6;
        }
    }
}
