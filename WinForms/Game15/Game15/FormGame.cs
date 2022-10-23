namespace Game15
{
    public partial class FormGame : System.Windows.Forms.Form
    {
        int difficulty = 4;
        public FormGame()
        {
            InitializeComponent();
            Game.Init(this, difficulty);
        }

        public void Reset()
        {
            Controls.Clear();
            InitializeComponent();
            Game.Init(this, difficulty);
        }

        private void resetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            difficulty = 4;
            Reset();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = 5;
            Reset();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = 6;
            Reset();
        }
    }
}