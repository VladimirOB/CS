namespace DynoRunner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Dyno = new System.Windows.Forms.PictureBox();
            this.Obstacle1 = new System.Windows.Forms.PictureBox();
            this.Obstacle2 = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.txtScore = new System.Windows.Forms.Label();
            this.cloudPB2 = new System.Windows.Forms.PictureBox();
            this.cloudPB3 = new System.Windows.Forms.PictureBox();
            this.cloudPB1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dyno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Obstacle1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Obstacle2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cloudPB2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cloudPB3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cloudPB1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(-10, 408);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(821, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Dyno
            // 
            this.Dyno.Image = global::DynoRunner.Properties.Resources.running;
            this.Dyno.Location = new System.Drawing.Point(38, 365);
            this.Dyno.Name = "Dyno";
            this.Dyno.Size = new System.Drawing.Size(40, 43);
            this.Dyno.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Dyno.TabIndex = 1;
            this.Dyno.TabStop = false;
            this.Dyno.Tag = "dyno";
            // 
            // Obstacle1
            // 
            this.Obstacle1.Image = global::DynoRunner.Properties.Resources.obstacle_1;
            this.Obstacle1.Location = new System.Drawing.Point(477, 362);
            this.Obstacle1.Name = "Obstacle1";
            this.Obstacle1.Size = new System.Drawing.Size(23, 46);
            this.Obstacle1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Obstacle1.TabIndex = 2;
            this.Obstacle1.TabStop = false;
            this.Obstacle1.Tag = "obstacle";
            // 
            // Obstacle2
            // 
            this.Obstacle2.Image = global::DynoRunner.Properties.Resources.obstacle_2;
            this.Obstacle2.Location = new System.Drawing.Point(720, 375);
            this.Obstacle2.Name = "Obstacle2";
            this.Obstacle2.Size = new System.Drawing.Size(32, 33);
            this.Obstacle2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Obstacle2.TabIndex = 3;
            this.Obstacle2.TabStop = false;
            this.Obstacle2.Tag = "obstacle";
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtScore
            // 
            this.txtScore.AutoSize = true;
            this.txtScore.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtScore.Location = new System.Drawing.Point(12, 9);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(105, 32);
            this.txtScore.TabIndex = 4;
            this.txtScore.Text = "Score: 0";
            // 
            // cloudPB2
            // 
            this.cloudPB2.Image = global::DynoRunner.Properties.Resources.cloud;
            this.cloudPB2.Location = new System.Drawing.Point(652, 153);
            this.cloudPB2.Name = "cloudPB2";
            this.cloudPB2.Size = new System.Drawing.Size(100, 50);
            this.cloudPB2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cloudPB2.TabIndex = 6;
            this.cloudPB2.TabStop = false;
            this.cloudPB2.Tag = "cloud";
            // 
            // cloudPB3
            // 
            this.cloudPB3.Image = global::DynoRunner.Properties.Resources.cloud;
            this.cloudPB3.Location = new System.Drawing.Point(410, 49);
            this.cloudPB3.Name = "cloudPB3";
            this.cloudPB3.Size = new System.Drawing.Size(100, 50);
            this.cloudPB3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cloudPB3.TabIndex = 7;
            this.cloudPB3.TabStop = false;
            this.cloudPB3.Tag = "cloud";
            // 
            // cloudPB1
            // 
            this.cloudPB1.Image = global::DynoRunner.Properties.Resources.cloud;
            this.cloudPB1.Location = new System.Drawing.Point(150, 126);
            this.cloudPB1.Name = "cloudPB1";
            this.cloudPB1.Size = new System.Drawing.Size(100, 50);
            this.cloudPB1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cloudPB1.TabIndex = 8;
            this.cloudPB1.TabStop = false;
            this.cloudPB1.Tag = "cloud";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cloudPB1);
            this.Controls.Add(this.cloudPB3);
            this.Controls.Add(this.cloudPB2);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.Obstacle2);
            this.Controls.Add(this.Obstacle1);
            this.Controls.Add(this.Dyno);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dyno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Obstacle1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Obstacle2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cloudPB2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cloudPB3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cloudPB1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox Dyno;
        private PictureBox Obstacle1;
        private PictureBox Obstacle2;
        private System.Windows.Forms.Timer gameTimer;
        private Label txtScore;
        private PictureBox cloudPB2;
        private PictureBox cloudPB3;
        private PictureBox cloudPB1;
    }
}