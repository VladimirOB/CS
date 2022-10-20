using Sapper.Controllers;
using System.Windows.Forms;

namespace Sapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MapController.mapSize = 10;
            MapController.numberOfBombs = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            BackgroundImage = null;
            MapController.Init(this);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MapController.mapSize = 10;
            MapController.numberOfBombs = 10;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MapController.mapSize = 15;
            MapController.numberOfBombs = 30;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            MapController.mapSize = 20;
            MapController.numberOfBombs = 60;
        }
    }
}