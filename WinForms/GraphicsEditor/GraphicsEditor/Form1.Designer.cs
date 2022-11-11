namespace GraphicsEditor
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnColorPurple = new System.Windows.Forms.Button();
            this.btnColorRed = new System.Windows.Forms.Button();
            this.btnColorBlue = new System.Windows.Forms.Button();
            this.btnColorGreen = new System.Windows.Forms.Button();
            this.btnColorYellow = new System.Windows.Forms.Button();
            this.btnColorGray = new System.Windows.Forms.Button();
            this.btnColorWhite = new System.Windows.Forms.Button();
            this.labelCurrentColor = new System.Windows.Forms.Label();
            this.btnColorBlack = new System.Windows.Forms.Button();
            this.panelCurrentColor = new System.Windows.Forms.Panel();
            this.buttonBackSpace = new System.Windows.Forms.Button();
            this.comboBoxLineSize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TBSizeHeight = new System.Windows.Forms.TextBox();
            this.TBSizeWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkFill = new System.Windows.Forms.CheckBox();
            this.checkLine = new System.Windows.Forms.CheckBox();
            this.checkTriangle = new System.Windows.Forms.CheckBox();
            this.checkRect = new System.Windows.Forms.CheckBox();
            this.checkEllipse = new System.Windows.Forms.CheckBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 494);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(192, 17);
            this.toolStripStatusLabel1.Text = "Здесь может быть ваша реклама!";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.colorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newImageToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newImageToolStripMenuItem
            // 
            this.newImageToolStripMenuItem.Name = "newImageToolStripMenuItem";
            this.newImageToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.newImageToolStripMenuItem.Text = "New Image";
            this.newImageToolStripMenuItem.Click += new System.EventHandler(this.newImageToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.loadToolStripMenuItem.Text = "Load Image";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.colorToolStripMenuItem.Text = "Color";
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnColorPurple);
            this.panel1.Controls.Add(this.btnColorRed);
            this.panel1.Controls.Add(this.btnColorBlue);
            this.panel1.Controls.Add(this.btnColorGreen);
            this.panel1.Controls.Add(this.btnColorYellow);
            this.panel1.Controls.Add(this.btnColorGray);
            this.panel1.Controls.Add(this.btnColorWhite);
            this.panel1.Controls.Add(this.labelCurrentColor);
            this.panel1.Controls.Add(this.btnColorBlack);
            this.panel1.Controls.Add(this.panelCurrentColor);
            this.panel1.Controls.Add(this.buttonBackSpace);
            this.panel1.Controls.Add(this.comboBoxLineSize);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TBSizeHeight);
            this.panel1.Controls.Add(this.TBSizeWidth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkFill);
            this.panel1.Controls.Add(this.checkLine);
            this.panel1.Controls.Add(this.checkTriangle);
            this.panel1.Controls.Add(this.checkRect);
            this.panel1.Controls.Add(this.checkEllipse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(79, 470);
            this.panel1.TabIndex = 3;
            // 
            // btnColorPurple
            // 
            this.btnColorPurple.BackColor = System.Drawing.Color.Purple;
            this.btnColorPurple.Location = new System.Drawing.Point(3, 401);
            this.btnColorPurple.Name = "btnColorPurple";
            this.btnColorPurple.Size = new System.Drawing.Size(30, 30);
            this.btnColorPurple.TabIndex = 21;
            this.btnColorPurple.UseVisualStyleBackColor = false;
            this.btnColorPurple.Click += new System.EventHandler(this.btnColorPurple_Click);
            // 
            // btnColorRed
            // 
            this.btnColorRed.BackColor = System.Drawing.Color.Red;
            this.btnColorRed.Location = new System.Drawing.Point(39, 401);
            this.btnColorRed.Name = "btnColorRed";
            this.btnColorRed.Size = new System.Drawing.Size(30, 30);
            this.btnColorRed.TabIndex = 20;
            this.btnColorRed.UseVisualStyleBackColor = false;
            this.btnColorRed.Click += new System.EventHandler(this.btnColorRed_Click);
            // 
            // btnColorBlue
            // 
            this.btnColorBlue.BackColor = System.Drawing.Color.Blue;
            this.btnColorBlue.Location = new System.Drawing.Point(5, 365);
            this.btnColorBlue.Name = "btnColorBlue";
            this.btnColorBlue.Size = new System.Drawing.Size(30, 30);
            this.btnColorBlue.TabIndex = 19;
            this.btnColorBlue.UseVisualStyleBackColor = false;
            this.btnColorBlue.Click += new System.EventHandler(this.btnColorBlue_Click);
            // 
            // btnColorGreen
            // 
            this.btnColorGreen.BackColor = System.Drawing.Color.Green;
            this.btnColorGreen.Location = new System.Drawing.Point(39, 365);
            this.btnColorGreen.Name = "btnColorGreen";
            this.btnColorGreen.Size = new System.Drawing.Size(30, 30);
            this.btnColorGreen.TabIndex = 18;
            this.btnColorGreen.UseVisualStyleBackColor = false;
            this.btnColorGreen.Click += new System.EventHandler(this.btnColorGreen_Click);
            // 
            // btnColorYellow
            // 
            this.btnColorYellow.BackColor = System.Drawing.Color.Yellow;
            this.btnColorYellow.Location = new System.Drawing.Point(3, 329);
            this.btnColorYellow.Name = "btnColorYellow";
            this.btnColorYellow.Size = new System.Drawing.Size(30, 30);
            this.btnColorYellow.TabIndex = 17;
            this.btnColorYellow.UseVisualStyleBackColor = false;
            this.btnColorYellow.Click += new System.EventHandler(this.btnColorYellow_Click);
            // 
            // btnColorGray
            // 
            this.btnColorGray.BackColor = System.Drawing.Color.Gray;
            this.btnColorGray.Location = new System.Drawing.Point(39, 329);
            this.btnColorGray.Name = "btnColorGray";
            this.btnColorGray.Size = new System.Drawing.Size(30, 30);
            this.btnColorGray.TabIndex = 16;
            this.btnColorGray.UseVisualStyleBackColor = false;
            this.btnColorGray.Click += new System.EventHandler(this.btnColorGray_Click);
            // 
            // btnColorWhite
            // 
            this.btnColorWhite.BackColor = System.Drawing.SystemColors.Window;
            this.btnColorWhite.Location = new System.Drawing.Point(3, 293);
            this.btnColorWhite.Name = "btnColorWhite";
            this.btnColorWhite.Size = new System.Drawing.Size(30, 30);
            this.btnColorWhite.TabIndex = 15;
            this.btnColorWhite.UseVisualStyleBackColor = false;
            this.btnColorWhite.Click += new System.EventHandler(this.btnColorWhite_Click);
            // 
            // labelCurrentColor
            // 
            this.labelCurrentColor.AutoSize = true;
            this.labelCurrentColor.Location = new System.Drawing.Point(18, 235);
            this.labelCurrentColor.Name = "labelCurrentColor";
            this.labelCurrentColor.Size = new System.Drawing.Size(39, 15);
            this.labelCurrentColor.TabIndex = 14;
            this.labelCurrentColor.Text = "Color:";
            // 
            // btnColorBlack
            // 
            this.btnColorBlack.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnColorBlack.Location = new System.Drawing.Point(39, 293);
            this.btnColorBlack.Name = "btnColorBlack";
            this.btnColorBlack.Size = new System.Drawing.Size(30, 30);
            this.btnColorBlack.TabIndex = 13;
            this.btnColorBlack.UseVisualStyleBackColor = false;
            this.btnColorBlack.Click += new System.EventHandler(this.btnColorBlack_Click);
            // 
            // panelCurrentColor
            // 
            this.panelCurrentColor.BackColor = System.Drawing.Color.White;
            this.panelCurrentColor.Location = new System.Drawing.Point(21, 253);
            this.panelCurrentColor.Name = "panelCurrentColor";
            this.panelCurrentColor.Size = new System.Drawing.Size(34, 34);
            this.panelCurrentColor.TabIndex = 12;
            // 
            // buttonBackSpace
            // 
            this.buttonBackSpace.BackColor = System.Drawing.SystemColors.Window;
            this.buttonBackSpace.Location = new System.Drawing.Point(3, 201);
            this.buttonBackSpace.Name = "buttonBackSpace";
            this.buttonBackSpace.Size = new System.Drawing.Size(70, 23);
            this.buttonBackSpace.TabIndex = 11;
            this.buttonBackSpace.Text = "Backspace";
            this.buttonBackSpace.UseVisualStyleBackColor = false;
            this.buttonBackSpace.Click += new System.EventHandler(this.buttonBackSpace_Click);
            // 
            // comboBoxLineSize
            // 
            this.comboBoxLineSize.FormattingEnabled = true;
            this.comboBoxLineSize.ItemHeight = 15;
            this.comboBoxLineSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBoxLineSize.Location = new System.Drawing.Point(39, 155);
            this.comboBoxLineSize.Name = "comboBoxLineSize";
            this.comboBoxLineSize.Size = new System.Drawing.Size(35, 23);
            this.comboBoxLineSize.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "W:       H:";
            // 
            // TBSizeHeight
            // 
            this.TBSizeHeight.Location = new System.Drawing.Point(39, 36);
            this.TBSizeHeight.Name = "TBSizeHeight";
            this.TBSizeHeight.Size = new System.Drawing.Size(30, 23);
            this.TBSizeHeight.TabIndex = 8;
            this.TBSizeHeight.Text = "100";
            this.TBSizeHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBSizeHeight_KeyPress);
            // 
            // TBSizeWidth
            // 
            this.TBSizeWidth.Location = new System.Drawing.Point(3, 36);
            this.TBSizeWidth.Name = "TBSizeWidth";
            this.TBSizeWidth.Size = new System.Drawing.Size(30, 23);
            this.TBSizeWidth.TabIndex = 7;
            this.TBSizeWidth.Text = "100";
            this.TBSizeWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBSizeWidth_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size:";
            // 
            // checkFill
            // 
            this.checkFill.AutoSize = true;
            this.checkFill.Location = new System.Drawing.Point(39, 109);
            this.checkFill.Name = "checkFill";
            this.checkFill.Size = new System.Drawing.Size(41, 19);
            this.checkFill.TabIndex = 5;
            this.checkFill.Text = "Fill";
            this.checkFill.UseVisualStyleBackColor = true;
            // 
            // checkLine
            // 
            this.checkLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkLine.BackgroundImage = global::GraphicsEditor.Properties.Resources.Line;
            this.checkLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkLine.Location = new System.Drawing.Point(3, 150);
            this.checkLine.Name = "checkLine";
            this.checkLine.Size = new System.Drawing.Size(30, 30);
            this.checkLine.TabIndex = 4;
            this.checkLine.Tag = "1";
            this.checkLine.UseVisualStyleBackColor = true;
            this.checkLine.Click += new System.EventHandler(this.checkLine_Click);
            // 
            // checkTriangle
            // 
            this.checkTriangle.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkTriangle.BackgroundImage = global::GraphicsEditor.Properties.Resources.Triangle;
            this.checkTriangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkTriangle.Location = new System.Drawing.Point(39, 66);
            this.checkTriangle.Name = "checkTriangle";
            this.checkTriangle.Size = new System.Drawing.Size(30, 30);
            this.checkTriangle.TabIndex = 3;
            this.checkTriangle.Tag = "4";
            this.checkTriangle.UseVisualStyleBackColor = true;
            this.checkTriangle.Click += new System.EventHandler(this.checkTriangle_Click);
            // 
            // checkRect
            // 
            this.checkRect.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkRect.BackgroundImage = global::GraphicsEditor.Properties.Resources.Rectangle;
            this.checkRect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkRect.Location = new System.Drawing.Point(3, 66);
            this.checkRect.Name = "checkRect";
            this.checkRect.Size = new System.Drawing.Size(30, 30);
            this.checkRect.TabIndex = 2;
            this.checkRect.Tag = "2";
            this.checkRect.UseVisualStyleBackColor = true;
            this.checkRect.Click += new System.EventHandler(this.checkRect_Click);
            // 
            // checkEllipse
            // 
            this.checkEllipse.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkEllipse.BackgroundImage = global::GraphicsEditor.Properties.Resources.Ellipse;
            this.checkEllipse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkEllipse.Location = new System.Drawing.Point(3, 102);
            this.checkEllipse.Name = "checkEllipse";
            this.checkEllipse.Size = new System.Drawing.Size(30, 30);
            this.checkEllipse.TabIndex = 1;
            this.checkEllipse.Tag = "3";
            this.checkEllipse.UseVisualStyleBackColor = true;
            this.checkEllipse.Click += new System.EventHandler(this.checkEllipse_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(79, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 470);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(82, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(542, 470);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 516);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Graphics Editor";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel1;
        private Splitter splitter1;
        private PictureBox pictureBox1;
        private CheckBox checkEllipse;
        private CheckBox checkRect;
        private CheckBox checkTriangle;
        private CheckBox checkLine;
        private CheckBox checkFill;
        private Label label2;
        private TextBox TBSizeHeight;
        private TextBox TBSizeWidth;
        private Label label1;
        private ComboBox comboBoxLineSize;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem colorToolStripMenuItem;
        private ToolStripMenuItem newImageToolStripMenuItem;
        private ToolStripMenuItem saveImageToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem loadToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private Button buttonBackSpace;
        private Button btnColorBlack;
        private Panel panelCurrentColor;
        private Label labelCurrentColor;
        private Button btnColorWhite;
        private Button btnColorGray;
        private Button btnColorYellow;
        private Button btnColorGreen;
        private Button btnColorBlue;
        private Button btnColorRed;
        private Button btnColorPurple;
    }
}