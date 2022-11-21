namespace GraphicsEditor_v3
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
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBVGFormatLayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLayersListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelBright = new System.Windows.Forms.Label();
            this.tBarBrightness = new System.Windows.Forms.TrackBar();
            this.btnRotateRight = new System.Windows.Forms.Button();
            this.labelRotate = new System.Windows.Forms.Label();
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.btnColor20 = new System.Windows.Forms.Button();
            this.btnColor19 = new System.Windows.Forms.Button();
            this.btnColor18 = new System.Windows.Forms.Button();
            this.btnColor17 = new System.Windows.Forms.Button();
            this.btnColor16 = new System.Windows.Forms.Button();
            this.btnColor15 = new System.Windows.Forms.Button();
            this.btnColor14 = new System.Windows.Forms.Button();
            this.btnColor13 = new System.Windows.Forms.Button();
            this.btnColor12 = new System.Windows.Forms.Button();
            this.btnColor11 = new System.Windows.Forms.Button();
            this.labelChangeColor = new System.Windows.Forms.Label();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.btnColor10 = new System.Windows.Forms.Button();
            this.btnColor9 = new System.Windows.Forms.Button();
            this.btnColor8 = new System.Windows.Forms.Button();
            this.btnColor7 = new System.Windows.Forms.Button();
            this.btnColor6 = new System.Windows.Forms.Button();
            this.btnColor5 = new System.Windows.Forms.Button();
            this.btnColor4 = new System.Windows.Forms.Button();
            this.btnColor3 = new System.Windows.Forms.Button();
            this.btnColor2 = new System.Windows.Forms.Button();
            this.btnColor1 = new System.Windows.Forms.Button();
            this.labelColor = new System.Windows.Forms.Label();
            this.panelCurrentColor = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHighlight = new System.Windows.Forms.RadioButton();
            this.btnGrater = new System.Windows.Forms.RadioButton();
            this.btnTriangle = new System.Windows.Forms.RadioButton();
            this.gbLabel = new System.Windows.Forms.Label();
            this.btnEllipse = new System.Windows.Forms.RadioButton();
            this.btnLine = new System.Windows.Forms.RadioButton();
            this.checkBoxFill = new System.Windows.Forms.CheckBox();
            this.trackBarBrush = new System.Windows.Forms.TrackBar();
            this.btnRectangle = new System.Windows.Forms.RadioButton();
            this.btnBrush = new System.Windows.Forms.RadioButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileImage = new System.Windows.Forms.SaveFileDialog();
            this.saveFileBVG = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBarBrightness)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrush)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(861, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel1
            // 
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(28, 17);
            this.statusLabel1.Text = "Text";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.layerToolStripMenuItem,
            this.colorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(861, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveImageFormatToolStripMenuItem,
            this.saveBVGFormatLayersToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveImageFormatToolStripMenuItem
            // 
            this.saveImageFormatToolStripMenuItem.Name = "saveImageFormatToolStripMenuItem";
            this.saveImageFormatToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveImageFormatToolStripMenuItem.Text = "Save Image format";
            this.saveImageFormatToolStripMenuItem.Click += new System.EventHandler(this.saveImageFormatToolStripMenuItem_Click);
            // 
            // saveBVGFormatLayersToolStripMenuItem
            // 
            this.saveBVGFormatLayersToolStripMenuItem.Name = "saveBVGFormatLayersToolStripMenuItem";
            this.saveBVGFormatLayersToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveBVGFormatLayersToolStripMenuItem.Text = "Save .BVG format(Layers)";
            this.saveBVGFormatLayersToolStripMenuItem.Click += new System.EventHandler(this.saveBVGFormatLayersToolStripMenuItem_Click);
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLayersListToolStripMenuItem,
            this.addLayerToolStripMenuItem,
            this.deleteLayerToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // openLayersListToolStripMenuItem
            // 
            this.openLayersListToolStripMenuItem.Name = "openLayersListToolStripMenuItem";
            this.openLayersListToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openLayersListToolStripMenuItem.Text = "Open Layers List";
            this.openLayersListToolStripMenuItem.Click += new System.EventHandler(this.openLayersListToolStripMenuItem_Click);
            // 
            // addLayerToolStripMenuItem
            // 
            this.addLayerToolStripMenuItem.Name = "addLayerToolStripMenuItem";
            this.addLayerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addLayerToolStripMenuItem.Text = "Add layer";
            this.addLayerToolStripMenuItem.Click += new System.EventHandler(this.addLayerToolStripMenuItem_Click);
            // 
            // deleteLayerToolStripMenuItem
            // 
            this.deleteLayerToolStripMenuItem.Name = "deleteLayerToolStripMenuItem";
            this.deleteLayerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.deleteLayerToolStripMenuItem.Text = "Delete layer";
            this.deleteLayerToolStripMenuItem.Click += new System.EventHandler(this.deleteLayerToolStripMenuItem_Click);
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
            this.panel1.Controls.Add(this.labelBright);
            this.panel1.Controls.Add(this.tBarBrightness);
            this.panel1.Controls.Add(this.btnRotateRight);
            this.panel1.Controls.Add(this.labelRotate);
            this.panel1.Controls.Add(this.btnRotateLeft);
            this.panel1.Controls.Add(this.btnColor20);
            this.panel1.Controls.Add(this.btnColor19);
            this.panel1.Controls.Add(this.btnColor18);
            this.panel1.Controls.Add(this.btnColor17);
            this.panel1.Controls.Add(this.btnColor16);
            this.panel1.Controls.Add(this.btnColor15);
            this.panel1.Controls.Add(this.btnColor14);
            this.panel1.Controls.Add(this.btnColor13);
            this.panel1.Controls.Add(this.btnColor12);
            this.panel1.Controls.Add(this.btnColor11);
            this.panel1.Controls.Add(this.labelChangeColor);
            this.panel1.Controls.Add(this.btnChangeColor);
            this.panel1.Controls.Add(this.btnColor10);
            this.panel1.Controls.Add(this.btnColor9);
            this.panel1.Controls.Add(this.btnColor8);
            this.panel1.Controls.Add(this.btnColor7);
            this.panel1.Controls.Add(this.btnColor6);
            this.panel1.Controls.Add(this.btnColor5);
            this.panel1.Controls.Add(this.btnColor4);
            this.panel1.Controls.Add(this.btnColor3);
            this.panel1.Controls.Add(this.btnColor2);
            this.panel1.Controls.Add(this.btnColor1);
            this.panel1.Controls.Add(this.labelColor);
            this.panel1.Controls.Add(this.panelCurrentColor);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 81);
            this.panel1.TabIndex = 2;
            // 
            // labelBright
            // 
            this.labelBright.AutoSize = true;
            this.labelBright.Location = new System.Drawing.Point(15, 11);
            this.labelBright.Name = "labelBright";
            this.labelBright.Size = new System.Drawing.Size(62, 15);
            this.labelBright.TabIndex = 29;
            this.labelBright.Text = "Brightness";
            // 
            // tBarBrightness
            // 
            this.tBarBrightness.AutoSize = false;
            this.tBarBrightness.Location = new System.Drawing.Point(5, 34);
            this.tBarBrightness.Maximum = 255;
            this.tBarBrightness.Minimum = -255;
            this.tBarBrightness.Name = "tBarBrightness";
            this.tBarBrightness.Size = new System.Drawing.Size(82, 32);
            this.tBarBrightness.TabIndex = 28;
            this.tBarBrightness.TickFrequency = 25;
            this.tBarBrightness.ValueChanged += new System.EventHandler(this.tBarBrightness_ValueChanged);
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.RotateRight;
            this.btnRotateRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRotateRight.Location = new System.Drawing.Point(139, 34);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(35, 35);
            this.btnRotateRight.TabIndex = 27;
            this.btnRotateRight.UseVisualStyleBackColor = true;
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            // 
            // labelRotate
            // 
            this.labelRotate.AutoSize = true;
            this.labelRotate.Location = new System.Drawing.Point(116, 11);
            this.labelRotate.Name = "labelRotate";
            this.labelRotate.Size = new System.Drawing.Size(41, 15);
            this.labelRotate.TabIndex = 26;
            this.labelRotate.Text = "Rotate";
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.RotateLeft;
            this.btnRotateLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRotateLeft.Location = new System.Drawing.Point(98, 34);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(35, 35);
            this.btnRotateLeft.TabIndex = 25;
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);
            // 
            // btnColor20
            // 
            this.btnColor20.BackColor = System.Drawing.Color.MediumPurple;
            this.btnColor20.Location = new System.Drawing.Point(726, 44);
            this.btnColor20.Name = "btnColor20";
            this.btnColor20.Size = new System.Drawing.Size(25, 25);
            this.btnColor20.TabIndex = 24;
            this.btnColor20.UseVisualStyleBackColor = false;
            this.btnColor20.Click += new System.EventHandler(this.btnColor20_Click);
            // 
            // btnColor19
            // 
            this.btnColor19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnColor19.Location = new System.Drawing.Point(699, 44);
            this.btnColor19.Name = "btnColor19";
            this.btnColor19.Size = new System.Drawing.Size(25, 25);
            this.btnColor19.TabIndex = 23;
            this.btnColor19.UseVisualStyleBackColor = false;
            this.btnColor19.Click += new System.EventHandler(this.btnColor19_Click);
            // 
            // btnColor18
            // 
            this.btnColor18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnColor18.Location = new System.Drawing.Point(672, 44);
            this.btnColor18.Name = "btnColor18";
            this.btnColor18.Size = new System.Drawing.Size(25, 25);
            this.btnColor18.TabIndex = 22;
            this.btnColor18.UseVisualStyleBackColor = false;
            this.btnColor18.Click += new System.EventHandler(this.btnColor18_Click);
            // 
            // btnColor17
            // 
            this.btnColor17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnColor17.Location = new System.Drawing.Point(645, 44);
            this.btnColor17.Name = "btnColor17";
            this.btnColor17.Size = new System.Drawing.Size(25, 25);
            this.btnColor17.TabIndex = 21;
            this.btnColor17.UseVisualStyleBackColor = false;
            this.btnColor17.Click += new System.EventHandler(this.btnColor17_Click);
            // 
            // btnColor16
            // 
            this.btnColor16.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnColor16.Location = new System.Drawing.Point(618, 44);
            this.btnColor16.Name = "btnColor16";
            this.btnColor16.Size = new System.Drawing.Size(25, 25);
            this.btnColor16.TabIndex = 20;
            this.btnColor16.UseVisualStyleBackColor = false;
            this.btnColor16.Click += new System.EventHandler(this.btnColor16_Click);
            // 
            // btnColor15
            // 
            this.btnColor15.BackColor = System.Drawing.Color.Gold;
            this.btnColor15.Location = new System.Drawing.Point(591, 44);
            this.btnColor15.Name = "btnColor15";
            this.btnColor15.Size = new System.Drawing.Size(25, 25);
            this.btnColor15.TabIndex = 19;
            this.btnColor15.UseVisualStyleBackColor = false;
            this.btnColor15.Click += new System.EventHandler(this.btnColor15_Click);
            // 
            // btnColor14
            // 
            this.btnColor14.BackColor = System.Drawing.Color.Pink;
            this.btnColor14.Location = new System.Drawing.Point(564, 44);
            this.btnColor14.Name = "btnColor14";
            this.btnColor14.Size = new System.Drawing.Size(25, 25);
            this.btnColor14.TabIndex = 18;
            this.btnColor14.UseVisualStyleBackColor = false;
            this.btnColor14.Click += new System.EventHandler(this.btnColor14_Click);
            // 
            // btnColor13
            // 
            this.btnColor13.BackColor = System.Drawing.Color.Peru;
            this.btnColor13.Location = new System.Drawing.Point(537, 44);
            this.btnColor13.Name = "btnColor13";
            this.btnColor13.Size = new System.Drawing.Size(25, 25);
            this.btnColor13.TabIndex = 17;
            this.btnColor13.UseVisualStyleBackColor = false;
            this.btnColor13.Click += new System.EventHandler(this.btnColor13_Click);
            // 
            // btnColor12
            // 
            this.btnColor12.BackColor = System.Drawing.Color.Silver;
            this.btnColor12.Location = new System.Drawing.Point(510, 44);
            this.btnColor12.Name = "btnColor12";
            this.btnColor12.Size = new System.Drawing.Size(25, 25);
            this.btnColor12.TabIndex = 16;
            this.btnColor12.UseVisualStyleBackColor = false;
            this.btnColor12.Click += new System.EventHandler(this.btnColor12_Click);
            // 
            // btnColor11
            // 
            this.btnColor11.BackColor = System.Drawing.Color.White;
            this.btnColor11.Location = new System.Drawing.Point(483, 44);
            this.btnColor11.Name = "btnColor11";
            this.btnColor11.Size = new System.Drawing.Size(25, 25);
            this.btnColor11.TabIndex = 15;
            this.btnColor11.UseVisualStyleBackColor = false;
            this.btnColor11.Click += new System.EventHandler(this.btnColor11_Click);
            // 
            // labelChangeColor
            // 
            this.labelChangeColor.AutoSize = true;
            this.labelChangeColor.Location = new System.Drawing.Point(760, 49);
            this.labelChangeColor.Name = "labelChangeColor";
            this.labelChangeColor.Size = new System.Drawing.Size(83, 15);
            this.labelChangeColor.TabIndex = 14;
            this.labelChangeColor.Text = "Change colors";
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.ChangeColor;
            this.btnChangeColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnChangeColor.Location = new System.Drawing.Point(782, 11);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(35, 35);
            this.btnChangeColor.TabIndex = 13;
            this.btnChangeColor.UseVisualStyleBackColor = true;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // btnColor10
            // 
            this.btnColor10.BackColor = System.Drawing.Color.BlueViolet;
            this.btnColor10.Location = new System.Drawing.Point(726, 16);
            this.btnColor10.Name = "btnColor10";
            this.btnColor10.Size = new System.Drawing.Size(25, 25);
            this.btnColor10.TabIndex = 12;
            this.btnColor10.UseVisualStyleBackColor = false;
            this.btnColor10.Click += new System.EventHandler(this.btnColor10_Click);
            // 
            // btnColor9
            // 
            this.btnColor9.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnColor9.Location = new System.Drawing.Point(699, 16);
            this.btnColor9.Name = "btnColor9";
            this.btnColor9.Size = new System.Drawing.Size(25, 25);
            this.btnColor9.TabIndex = 11;
            this.btnColor9.UseVisualStyleBackColor = false;
            this.btnColor9.Click += new System.EventHandler(this.btnColor9_Click);
            // 
            // btnColor8
            // 
            this.btnColor8.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnColor8.Location = new System.Drawing.Point(672, 16);
            this.btnColor8.Name = "btnColor8";
            this.btnColor8.Size = new System.Drawing.Size(25, 25);
            this.btnColor8.TabIndex = 10;
            this.btnColor8.UseVisualStyleBackColor = false;
            this.btnColor8.Click += new System.EventHandler(this.btnColor8_Click);
            // 
            // btnColor7
            // 
            this.btnColor7.BackColor = System.Drawing.Color.Green;
            this.btnColor7.Location = new System.Drawing.Point(645, 16);
            this.btnColor7.Name = "btnColor7";
            this.btnColor7.Size = new System.Drawing.Size(25, 25);
            this.btnColor7.TabIndex = 9;
            this.btnColor7.UseVisualStyleBackColor = false;
            this.btnColor7.Click += new System.EventHandler(this.btnColor7_Click);
            // 
            // btnColor6
            // 
            this.btnColor6.BackColor = System.Drawing.Color.Yellow;
            this.btnColor6.Location = new System.Drawing.Point(618, 16);
            this.btnColor6.Name = "btnColor6";
            this.btnColor6.Size = new System.Drawing.Size(25, 25);
            this.btnColor6.TabIndex = 8;
            this.btnColor6.UseVisualStyleBackColor = false;
            this.btnColor6.Click += new System.EventHandler(this.btnColor6_Click);
            // 
            // btnColor5
            // 
            this.btnColor5.BackColor = System.Drawing.Color.OrangeRed;
            this.btnColor5.Location = new System.Drawing.Point(591, 16);
            this.btnColor5.Name = "btnColor5";
            this.btnColor5.Size = new System.Drawing.Size(25, 25);
            this.btnColor5.TabIndex = 7;
            this.btnColor5.UseVisualStyleBackColor = false;
            this.btnColor5.Click += new System.EventHandler(this.btnColor5_Click);
            // 
            // btnColor4
            // 
            this.btnColor4.BackColor = System.Drawing.Color.Red;
            this.btnColor4.Location = new System.Drawing.Point(564, 16);
            this.btnColor4.Name = "btnColor4";
            this.btnColor4.Size = new System.Drawing.Size(25, 25);
            this.btnColor4.TabIndex = 6;
            this.btnColor4.UseVisualStyleBackColor = false;
            this.btnColor4.Click += new System.EventHandler(this.btnColor4_Click);
            // 
            // btnColor3
            // 
            this.btnColor3.BackColor = System.Drawing.Color.Brown;
            this.btnColor3.Location = new System.Drawing.Point(537, 16);
            this.btnColor3.Name = "btnColor3";
            this.btnColor3.Size = new System.Drawing.Size(25, 25);
            this.btnColor3.TabIndex = 5;
            this.btnColor3.UseVisualStyleBackColor = false;
            this.btnColor3.Click += new System.EventHandler(this.btnColor3_Click);
            // 
            // btnColor2
            // 
            this.btnColor2.BackColor = System.Drawing.Color.Gray;
            this.btnColor2.Location = new System.Drawing.Point(510, 16);
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.Size = new System.Drawing.Size(25, 25);
            this.btnColor2.TabIndex = 4;
            this.btnColor2.UseVisualStyleBackColor = false;
            this.btnColor2.Click += new System.EventHandler(this.btnColor2_Click);
            // 
            // btnColor1
            // 
            this.btnColor1.BackColor = System.Drawing.Color.Black;
            this.btnColor1.Location = new System.Drawing.Point(483, 16);
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.Size = new System.Drawing.Size(25, 25);
            this.btnColor1.TabIndex = 3;
            this.btnColor1.UseVisualStyleBackColor = false;
            this.btnColor1.Click += new System.EventHandler(this.btnColor1_Click);
            // 
            // labelColor
            // 
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(432, 11);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(36, 15);
            this.labelColor.TabIndex = 2;
            this.labelColor.Text = "Color";
            // 
            // panelCurrentColor
            // 
            this.panelCurrentColor.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelCurrentColor.Location = new System.Drawing.Point(433, 30);
            this.panelCurrentColor.Name = "panelCurrentColor";
            this.panelCurrentColor.Size = new System.Drawing.Size(35, 35);
            this.panelCurrentColor.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHighlight);
            this.groupBox1.Controls.Add(this.btnGrater);
            this.groupBox1.Controls.Add(this.btnTriangle);
            this.groupBox1.Controls.Add(this.gbLabel);
            this.groupBox1.Controls.Add(this.btnEllipse);
            this.groupBox1.Controls.Add(this.btnLine);
            this.groupBox1.Controls.Add(this.checkBoxFill);
            this.groupBox1.Controls.Add(this.trackBarBrush);
            this.groupBox1.Controls.Add(this.btnRectangle);
            this.groupBox1.Controls.Add(this.btnBrush);
            this.groupBox1.Location = new System.Drawing.Point(180, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnHighlight
            // 
            this.btnHighlight.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnHighlight.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.Highlight;
            this.btnHighlight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighlight.Location = new System.Drawing.Point(6, 54);
            this.btnHighlight.Name = "btnHighlight";
            this.btnHighlight.Size = new System.Drawing.Size(25, 25);
            this.btnHighlight.TabIndex = 9;
            this.btnHighlight.TabStop = true;
            this.btnHighlight.Tag = "Highlight";
            this.btnHighlight.UseVisualStyleBackColor = true;
            // 
            // btnGrater
            // 
            this.btnGrater.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnGrater.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.Grater;
            this.btnGrater.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrater.Location = new System.Drawing.Point(6, 31);
            this.btnGrater.Name = "btnGrater";
            this.btnGrater.Size = new System.Drawing.Size(25, 25);
            this.btnGrater.TabIndex = 8;
            this.btnGrater.TabStop = true;
            this.btnGrater.Tag = "Grater";
            this.btnGrater.UseVisualStyleBackColor = true;
            // 
            // btnTriangle
            // 
            this.btnTriangle.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnTriangle.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.Triangle;
            this.btnTriangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTriangle.Location = new System.Drawing.Point(201, 33);
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.Size = new System.Drawing.Size(30, 30);
            this.btnTriangle.TabIndex = 7;
            this.btnTriangle.TabStop = true;
            this.btnTriangle.Tag = "Triangle";
            this.btnTriangle.UseVisualStyleBackColor = true;
            // 
            // gbLabel
            // 
            this.gbLabel.AutoSize = true;
            this.gbLabel.Location = new System.Drawing.Point(2, 13);
            this.gbLabel.Name = "gbLabel";
            this.gbLabel.Size = new System.Drawing.Size(186, 15);
            this.gbLabel.TabIndex = 1;
            this.gbLabel.Text = "Tools    Brush                       Figures ";
            // 
            // btnEllipse
            // 
            this.btnEllipse.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnEllipse.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.Ellipse;
            this.btnEllipse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEllipse.Location = new System.Drawing.Point(165, 33);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(30, 30);
            this.btnEllipse.TabIndex = 6;
            this.btnEllipse.TabStop = true;
            this.btnEllipse.Tag = "Ellipse";
            this.btnEllipse.UseVisualStyleBackColor = true;
            // 
            // btnLine
            // 
            this.btnLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnLine.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.Line;
            this.btnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLine.Location = new System.Drawing.Point(93, 33);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(30, 30);
            this.btnLine.TabIndex = 5;
            this.btnLine.TabStop = true;
            this.btnLine.Tag = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            // 
            // checkBoxFill
            // 
            this.checkBoxFill.AutoSize = true;
            this.checkBoxFill.Location = new System.Drawing.Point(141, 64);
            this.checkBoxFill.Name = "checkBoxFill";
            this.checkBoxFill.Size = new System.Drawing.Size(41, 19);
            this.checkBoxFill.TabIndex = 4;
            this.checkBoxFill.Text = "Fill";
            this.checkBoxFill.UseVisualStyleBackColor = true;
            // 
            // trackBarBrush
            // 
            this.trackBarBrush.AutoSize = false;
            this.trackBarBrush.Location = new System.Drawing.Point(39, 59);
            this.trackBarBrush.Maximum = 5;
            this.trackBarBrush.Minimum = 1;
            this.trackBarBrush.Name = "trackBarBrush";
            this.trackBarBrush.Size = new System.Drawing.Size(40, 20);
            this.trackBarBrush.TabIndex = 3;
            this.trackBarBrush.Value = 1;
            this.trackBarBrush.Scroll += new System.EventHandler(this.trackBarBrush_Scroll);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnRectangle.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.Rectangle;
            this.btnRectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRectangle.Location = new System.Drawing.Point(129, 33);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(30, 30);
            this.btnRectangle.TabIndex = 2;
            this.btnRectangle.TabStop = true;
            this.btnRectangle.Tag = "Rectangle";
            this.btnRectangle.UseVisualStyleBackColor = true;
            // 
            // btnBrush
            // 
            this.btnBrush.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnBrush.BackgroundImage = global::GraphicsEditor_v3.Properties.Resources.Curve;
            this.btnBrush.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBrush.Location = new System.Drawing.Point(42, 31);
            this.btnBrush.Name = "btnBrush";
            this.btnBrush.Size = new System.Drawing.Size(32, 32);
            this.btnBrush.TabIndex = 0;
            this.btnBrush.TabStop = true;
            this.btnBrush.Tag = "Brush";
            this.btnBrush.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 105);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(861, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(861, 392);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 522);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBarBrightness)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrush)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private Panel panel1;
        private Splitter splitter1;
        private ToolStripMenuItem layerToolStripMenuItem;
        private ToolStripMenuItem addLayerToolStripMenuItem;
        private GroupBox groupBox1;
        private RadioButton btnRectangle;
        private Label gbLabel;
        private RadioButton btnBrush;
        private TrackBar trackBarBrush;
        private CheckBox checkBoxFill;
        private RadioButton btnLine;
        private RadioButton btnEllipse;
        private RadioButton btnTriangle;
        private ToolStripMenuItem colorToolStripMenuItem;
        private Panel panelCurrentColor;
        private Label labelColor;
        private Label labelChangeColor;
        private Button btnChangeColor;
        private Button btnColor10;
        private Button btnColor9;
        private Button btnColor8;
        private Button btnColor7;
        private Button btnColor6;
        private Button btnColor5;
        private Button btnColor4;
        private Button btnColor3;
        private Button btnColor2;
        private Button btnColor1;
        private Button btnColor20;
        private Button btnColor19;
        private Button btnColor18;
        private Button btnColor17;
        private Button btnColor16;
        private Button btnColor15;
        private Button btnColor14;
        private Button btnColor13;
        private Button btnColor12;
        private Button btnColor11;
        private RadioButton btnGrater;
        private Button btnRotateRight;
        private Label labelRotate;
        private Button btnRotateLeft;
        private ToolStripMenuItem openToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem saveImageFormatToolStripMenuItem;
        private ToolStripMenuItem saveBVGFormatLayersToolStripMenuItem;
        private SaveFileDialog saveFileImage;
        private Label labelBright;
        private TrackBar tBarBrightness;
        private SaveFileDialog saveFileBVG;
        private ToolStripMenuItem deleteLayerToolStripMenuItem;
        private ToolStripMenuItem openLayersListToolStripMenuItem;
        private PictureBox pictureBox1;
        private RadioButton btnHighlight;
    }
}