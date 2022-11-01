using System.Drawing;
using System.Runtime.CompilerServices;

namespace Game15
{
    public partial class FormGame : System.Windows.Forms.Form
    {
        public FormGame(int size)
        {
            Game.size = size;
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
            saveFileDialog1.Title = "Saving";
            saveFileDialog1.Filter = "System File|*.dat";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Save(saveFileDialog1.FileName);
            }
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
            openFileDialog1.Title = "Load";
            openFileDialog1.Filter = "System File|*.dat";
            // Проверка существования выбранного файла
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Game.Load(this, openFileDialog1.FileName);
            }
        }

        //Сворачивание в трей
        private void FormGame_Resize(object sender, EventArgs e)
        {
            if(Left == -32000 && Top == -32000)
            {
                notifyIcon1.Visible = true;
                Visible = false;
                notifyIcon1.ShowBalloonTip(1500);
            }
            else
            {
                MaximumSize = new Size(Game.size * Game.CELLSIZE + 20, (Game.size * Game.CELLSIZE) + 70);
                Width = Game.size * Game.CELLSIZE + 20;
                Height = (Game.size * Game.CELLSIZE) + 70;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Visible = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void FormGame_Move(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.SizeAll;
        }

        private void FormGame_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.Arrow;
        }
    }
}