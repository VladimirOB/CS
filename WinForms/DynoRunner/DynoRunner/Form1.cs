namespace DynoRunner
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        bool isGameOver = false;
        int jumpSpeed;
        int force = 12;
        int obstacleSpeed = 10;
        int cloudSpeed = 5;
        int score = 0;
        int pos;
        Random rand = new Random(); 
        


        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }

        private void ResetGame()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score: " + score;
            Dyno.Image = Properties.Resources.running;
            isGameOver = false;
            Dyno.Top = 365;

            foreach (Control x in Controls)
            {
                if(x is PictureBox && (string)x.Tag == "obstacle")
                {
                    pos = this.ClientSize.Width + rand.Next(100, 500) + Width;
                    x.Left = pos;
                }
            }
            gameTimer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Dyno.Top += jumpSpeed;
            txtScore.Text = "Score: " + score;

            if(jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
                jumpSpeed = 12;

            if(Dyno.Top > 364 && jumping == false)
            {
                force = 12;
                Dyno.Top = 365;
                jumpSpeed = 0;
            }

            foreach (Control x in Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;
                    if(x.Left < -50)
                    {
                        x.Left = ClientSize.Width + rand.Next(50, 500) + Width;
                        score++;
                    }

                    // если границы динозавра пересеклись с границами кактуса
                    if(Dyno.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        Dyno.Image = Properties.Resources.dead;
                        txtScore.Text += " Press Enter to restart the game!";
                        isGameOver = true;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "cloud")
                {
                    x.Left -= cloudSpeed;
                    if (x.Left < -50)
                    {
                        x.Left = ClientSize.Width + 50;
                    }
                }
            }

            if(score > 10)
            {
                obstacleSpeed = 15;
            }
            if (score > 20)
            {
                obstacleSpeed = 20;
            }
        }


        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                ResetGame();
            }
        }
    }
}