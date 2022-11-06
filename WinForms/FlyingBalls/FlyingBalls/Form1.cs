using System.Windows.Forms;

namespace FlyingBalls
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        int posX, posY;
        int tag = 0;

        List<PictureBox> lstBalls = new List<PictureBox>();
        List<int> sX = new List<int>();
        List<int> sY = new List<int>();
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox picture = new PictureBox();
            picture.Tag = tag++;
            picture.Click += new System.EventHandler(pictureClick);

            picture.Size = new Size(50, 50);
            picture.Image = new Bitmap("../../../ball.jpg");
            picture.Location = new Point(posX, posY);
            picture.Parent = this;

            int stepX = 0, stepY = 0;
            do
            {
                stepX = rand.Next(-5, 5);
                stepY = rand.Next(-5, 5);
            }
            while (stepX == 0 && stepY == 0);
            sX.Add(stepX);
            sY.Add(stepY);
            lstBalls.Add(picture);
        }

        private void pictureClick(object sender, EventArgs e)
        {
            PictureBox picture = sender as PictureBox;
            picture.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var item in lstBalls)
            {
                // Переместить шарик в новые координаты
                item.Left += sX[(int)item.Tag];
                item.Top += sY[(int)item.Tag];

                if (item.Location.X < 0 || item.Location.X + 50 > ClientSize.Width)
                {
                    sX[(int)item.Tag] = -sX[(int)item.Tag];
                }
                if (item.Location.Y < 0 || item.Location.Y + 50 > ClientSize.Height)
                {
                    sY[(int)item.Tag] = -sY[(int)item.Tag];
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            posX = e.X;
            posY = e.Y;
            Text = $"{e.X} : {e.Y}";
        }
    }
}