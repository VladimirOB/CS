using System.Windows.Forms;

namespace FlyingBalls
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        int posX, posY;
        int tag = 0;

        Dictionary<PictureBox, System.Windows.Forms.Timer> lstBalls = new Dictionary<PictureBox, System.Windows.Forms.Timer>();
        List<int> sX = new List<int>();
        List<int> sY = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox picture = new PictureBox();
            picture.Tag = tag;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            picture.Click += new System.EventHandler(pictureClick);
            timer.Tick += new System.EventHandler(pictureTimerTick);
            timer.Tag = tag++;

            picture.Size = new Size(50, 50);
            picture.Image = new Bitmap("../../../ball.jpg");
            picture.Location = new Point(posX, posY);
            picture.Parent = this;

            int stepX = 0, stepY = 0;
            do
            {
                stepX = rand.Next(-2, 2);
                stepY = rand.Next(-2, 2);
            }
            while (stepX == 0 && stepY == 0);
            sX.Add(stepX);
            sY.Add(stepY);
            lstBalls.Add(picture, timer);
            timer.Interval = 100;
            timer.Start();
        }

        private void pictureClick(object sender, EventArgs e)
        {
            PictureBox picture = sender as PictureBox;
            picture.Dispose();
        }

       

        private void pictureTimerTick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = sender as System.Windows.Forms.Timer;
            foreach (var item in lstBalls)
            {
                if(item.Value.Tag.Equals(item.Key.Tag))
                {
                    // Переместить шарик в новые координаты
                    
                    item.Key.Left += sX[(int)item.Key.Tag];
                    item.Key.Top += sY[(int)item.Key.Tag];

                    if(item.Key.Location.X < 0 || item.Key.Location.X + 50 > ClientSize.Width)
                    {
                        sX[(int)item.Key.Tag] = -sX[(int)item.Key.Tag];
                    }
                    if (item.Key.Location.Y < 0 || item.Key.Location.Y + 50 > ClientSize.Height)
                    {
                        sY[(int)item.Key.Tag] = -sY[(int)item.Key.Tag];
                    }
                }
            }
            
        }


        private bool CheckBorder()
        {
            return false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            posX = e.X;
            posY = e.Y;
            Text = $"{e.X} : {e.Y}";
        }
    }
}