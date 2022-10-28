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

        private void resetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            Game.size = 4;
            Reset();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Game.size = 5;
            Reset();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Game.size = 6;
            Reset();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save("../../../Quick.dat");
        }

        void Save(string file_name)
        {
            StreamWriter sw = new StreamWriter(file_name);
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

        public void Load()
        {
            Controls.Clear();
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Game.Load(this, "../../../Quick.dat");
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMenu formMenu = new FormMenu(this);
            formMenu.BackgroundImage = new Bitmap("../../../background.jpg");
            DialogResult result = formMenu.ShowDialog();

            if(result == DialogResult.OK)
            {
                Reset();
            }
            if (result == DialogResult.Yes) // button Save
            {
                saveFileDialog1.Title = "Saving";
                saveFileDialog1.Filter = "System File|*.dat";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Save(saveFileDialog1.FileName);
                }
            }

            if(result == DialogResult.No) //button Load
            {
                openFileDialog1.Title = "Load";
                openFileDialog1.Filter = "System File|*.dat";
                // Проверка существования выбранного файла
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.Multiselect = false;
                if(openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Game.Load(this, openFileDialog1.FileName);
                }
            }
        }
    }
}