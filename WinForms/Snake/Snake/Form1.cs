using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();

        private Circle food = new Circle();

        private Circle bigFood = new Circle();
        bool createBigFood = false;
        bool deleteBigFood = false;

        int maxWidth;
        int maxHeight;

        int score;
        int highScore = 0;

        Random rand = new Random();

        bool goLeft, goRight, goDown, goUp;

        public Form1()
        {
            InitializeComponent();

            new Settings();

            txtHighScore.ForeColor = Color.Maroon;
            //highScore = Convert.ToInt32(File.ReadAllText("highScore.txt"));
            txtHighScore.Text = "High Score: " + highScore;
            bigFoodBar.Value = 100;

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left && Settings.directions != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Settings.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Settings.directions != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Settings.directions != "up")
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            //Настройки направления
            if(goLeft)
            {
                Settings.directions = "left";
            }
            if (goRight)
            {
                Settings.directions = "right";
            }
            if (goDown)
            {
                Settings.directions = "down";
            }
            if (goUp)
            {
                Settings.directions = "up";
            }

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0) // голова
                {
                    switch(Settings.directions)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                    }

                    // для прохода сквозь стены
                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }

                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }
                    // bigFood.X = верхний левый
                    if(createBigFood)
                    {
                        for (int k = bigFood.X; k < bigFood.X + 3; k++)
                        {
                            for (int l = bigFood.Y; l < bigFood.Y + 3; l++)
                            {
                                if (Snake[i].X == k && Snake[i].Y == l)
                                {
                                    EatBigFood();
                                    DelBigFood();
                                    break;
                                }
                            }
                        }
                    }
                    
                    

                    // Начинается с 1, потому что 0 - голова
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            GameOver();
                        }
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }

            picCanvas.Invalidate(); //аннулировать

        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Brush snakeColor;
            //Bitmap f = new Bitmap("../../../food2.bmp");
            //canvas.DrawImage(f, food.X, food.Y);
            for (int i = 0; i < Snake.Count; i++)
            {
                if( i == 0)
                {
                    snakeColor = Brushes.AliceBlue;
                }
                else
                {
                    snakeColor = Brushes.DarkGreen;
                }
                canvas.FillEllipse(snakeColor, new Rectangle //Заполнить эллипс
                    (
                    Snake[i].X * Settings.Width,
                    Snake[i].Y * Settings.Height,
                    Settings.Width,Settings.Height
                    ));
            }
            canvas.FillEllipse(Brushes.DarkRed, new Rectangle //Заполнить эллипс
                   (
                   food.X * Settings.Width,
                   food.Y * Settings.Height,
                   Settings.Width, Settings.Height
                   ));

            if(createBigFood)
            {
                canvas.FillEllipse(Brushes.Red, new Rectangle //Заполнить эллипс
                   (
                   bigFood.X * Settings.Width,
                   bigFood.Y * Settings.Height,
                   Settings.Width*3, Settings.Height*3
                   ));
                if(!deleteBigFood)
                BigFoodTimer();
                deleteBigFood = true;
            }

        }

        private void bigFoodTimer_Tick(object sender, EventArgs e)
        {
            if(bigFoodBar.Value > bigFoodBar.Minimum)
            {
                // переместить progressBar1 на один шаг (свойство Step)
                //bigFoodBar.PerformStep();
                bigFoodBar.Value--;
            }
            else
            {
                DelBigFood();
            }
        }

        private void DelBigFood()
        {
            bigFoodTimer.Stop();
            bigFoodBar.Value = 100;
            createBigFood = false;
            bigFoodBar.Enabled = false;
            bigFoodBar.Visible = false;

        }

        private void BigFoodTimer()
        {
            bigFoodBar.Enabled = true;
            bigFoodBar.Visible = true;
            bigFoodTimer.Start();
        }

        private void screenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Label caption = new Label();
            caption.Text = "I scored: " + score + ".Highscore is " + highScore + ".";
            caption.Font = new Font("Ariel", 12, FontStyle.Bold);
            caption.ForeColor = Color.Purple;
            caption.AutoSize = false;
            caption.Width = picCanvas.Width;
            caption.Height = 30;
            caption.TextAlign = ContentAlignment.MiddleCenter;
            picCanvas.Controls.Add(caption);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Snake screenshot";
            dialog.DefaultExt = "jpg";
            dialog.Filter = "JPG Image File | *.jpg";
            dialog.ValidateNames = true;

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(picCanvas.Width);
                int height = Convert.ToInt32(picCanvas.Height);
                Bitmap image = new Bitmap(width, height);
                picCanvas.DrawToBitmap(image, new Rectangle(0, 0, width, height));
                image.Save(dialog.FileName, ImageFormat.Jpeg); // чтоб работало < > using System.Drawing.Imaging;
                picCanvas.Controls.Remove(caption);
            }
        }

        private void RestartGame()
        {
            maxWidth = picCanvas.Width / Settings.Width - 1;
            maxHeight = picCanvas.Height / Settings.Height - 1;

            Snake.Clear();
            score = 0;
            txtScore.Text = "Score: " + score;

            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head); // Добавили голову змеи

            for (int i = 0; i < 7; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }
            do
                food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            while (Snake.Any(i => i.X == food.X && i.Y == food.Y));
            gameTimer.Start();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Interval = 120;
            bigFoodTimer.Interval = 50;
            RestartGame();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Interval = 60;
            bigFoodTimer.Interval = 25;
            RestartGame();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Interval = 30;
            bigFoodTimer.Interval = 10;
            RestartGame();
        }

        private void EatFood()
        {
            score += 1;
            txtScore.Text = "Score: " + score;

            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);

            do
                food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            while (Snake.Any(i => i.X == food.X && i.Y == food.Y));

            if (score % 7 == 0)
            {
                do
                bigFood = new Circle { X = rand.Next(6, maxWidth - 3), Y = rand.Next(6, maxHeight - 3) };
                
                while (Snake.Any(i => i.X == bigFood.X && i.Y == bigFood.Y));
                createBigFood = true;
                deleteBigFood = false;
            }
        }
        private void EatBigFood()
        {
            score += 3;
            txtScore.Text = "Score: " + score;
            Circle body = new Circle
            {
                X = Snake[Snake.Count - 2].X,
                Y = Snake[Snake.Count - 2].Y
            };
            Snake.Add(body);
            body.X = Snake[Snake.Count - 1].X;
            body.Y = Snake[Snake.Count - 1].Y;
            Snake.Add(body);
        }

        private void GameOver()
        {
            gameTimer.Stop();
            
            if(score > highScore)
            {
                highScore = score;
                txtHighScore.Text = "High Score: " + highScore;// + Environment.NewLine
                //txtHighScore.TextAlign = ContentAlignment.MiddleCenter; // Выравнивание по центру
                File.WriteAllText("highScore.txt", highScore.ToString());
            }
            MessageBox.Show("Game Over!", "Attention!");
        }
    }
}