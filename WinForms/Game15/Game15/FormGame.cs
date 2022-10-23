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

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to leave?", e.CloseReason.ToString(), MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                e.Cancel = true; // отмена выхода
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("db.dat");
            sw.WriteLine(Game.size);
            for (int i = 0; i < Game.size; i++)
            {
                for (int j = 0; j < Game.size; j++)
                {
                    sw.Write(Game.map[i, j] + " ");
                }
            }
            sw.Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
            Game.Load(this);
        }
    }
}