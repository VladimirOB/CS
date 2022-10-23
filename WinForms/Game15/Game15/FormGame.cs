namespace Game15
{
    public partial class FormGame : System.Windows.Forms.Form
    {
        public FormGame()
        {
            InitializeComponent();
            Game.Init(this);
        }

        public void Reset()
        {
            Controls.Clear();
            InitializeComponent();
            Game.Init(this);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
            Game.Init(this);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }
    }
}