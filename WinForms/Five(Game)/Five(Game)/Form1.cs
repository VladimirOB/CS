namespace Five_Game_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MapController.Init(this);
        }

        public void Restart()
        {
            Controls.Clear();
            InitializeComponent();
            MapController.Init(this);
        }

        public void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
            MapController.Init(this);
        }
    }
}