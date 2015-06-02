namespace WhoIsSpeaking
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblArxStatusHeader = new System.Windows.Forms.Label();
            this.lblArxStatus = new System.Windows.Forms.Label();
            this.chkUseArx = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSpeaking = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioSpell = new System.Windows.Forms.RadioButton();
            this.radioScroll = new System.Windows.Forms.RadioButton();
            this.radioOFF = new System.Windows.Forms.RadioButton();
            this.chkLEDAnimation = new System.Windows.Forms.CheckBox();
            this.chkLogitechColours = new System.Windows.Forms.CheckBox();
            this.picStartColour = new System.Windows.Forms.PictureBox();
            this.picEndColour = new System.Windows.Forms.PictureBox();
            this.numGradientSpeed = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numFadeSpeed = new System.Windows.Forms.NumericUpDown();
            this.chkWave = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numAnimationSpeed = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numDistanceFalloff = new System.Windows.Forms.NumericUpDown();
            this.numWaveSpeed = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstProfiles = new System.Windows.Forms.ListBox();
            this.cmdCancelProfileLoad = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.chkKeySaver = new System.Windows.Forms.CheckBox();
            this.numKeySaverTime = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.chkRandomColours = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCpuTemp = new System.Windows.Forms.Label();
            this.lblGPUTemp = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStartColour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEndColour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGradientSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFadeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAnimationSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceFalloff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaveSpeed)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKeySaverTime)).BeginInit();
            this.SuspendLayout();
            // 
            // lblArxStatusHeader
            // 
            this.lblArxStatusHeader.AutoSize = true;
            this.lblArxStatusHeader.Location = new System.Drawing.Point(133, 310);
            this.lblArxStatusHeader.Name = "lblArxStatusHeader";
            this.lblArxStatusHeader.Size = new System.Drawing.Size(76, 17);
            this.lblArxStatusHeader.TabIndex = 1;
            this.lblArxStatusHeader.Text = "Arx Status:";
            // 
            // lblArxStatus
            // 
            this.lblArxStatus.AutoSize = true;
            this.lblArxStatus.Location = new System.Drawing.Point(215, 310);
            this.lblArxStatus.Name = "lblArxStatus";
            this.lblArxStatus.Size = new System.Drawing.Size(92, 17);
            this.lblArxStatus.TabIndex = 2;
            this.lblArxStatus.Text = "disconnected";
            // 
            // chkUseArx
            // 
            this.chkUseArx.AutoSize = true;
            this.chkUseArx.Checked = true;
            this.chkUseArx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseArx.Location = new System.Drawing.Point(27, 309);
            this.chkUseArx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkUseArx.Name = "chkUseArx";
            this.chkUseArx.Size = new System.Drawing.Size(87, 21);
            this.chkUseArx.TabIndex = 3;
            this.chkUseArx.Text = "Use ARX";
            this.chkUseArx.UseVisualStyleBackColor = true;
            this.chkUseArx.CheckedChanged += new System.EventHandler(this.chkUseArx_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 470);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Speaking:";
            // 
            // lblSpeaking
            // 
            this.lblSpeaking.AutoSize = true;
            this.lblSpeaking.Location = new System.Drawing.Point(100, 470);
            this.lblSpeaking.Name = "lblSpeaking";
            this.lblSpeaking.Size = new System.Drawing.Size(0, 17);
            this.lblSpeaking.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioSpell);
            this.groupBox1.Controls.Add(this.radioScroll);
            this.groupBox1.Controls.Add(this.radioOFF);
            this.groupBox1.Location = new System.Drawing.Point(27, 347);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(331, 111);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ventrilo Keyboard LED";
            // 
            // radioSpell
            // 
            this.radioSpell.AutoSize = true;
            this.radioSpell.Location = new System.Drawing.Point(19, 81);
            this.radioSpell.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioSpell.Name = "radioSpell";
            this.radioSpell.Size = new System.Drawing.Size(111, 21);
            this.radioSpell.TabIndex = 2;
            this.radioSpell.Text = "Fading Scroll";
            this.radioSpell.UseVisualStyleBackColor = true;
            this.radioSpell.CheckedChanged += new System.EventHandler(this.radioSpell_CheckedChanged);
            // 
            // radioScroll
            // 
            this.radioScroll.AutoSize = true;
            this.radioScroll.Checked = true;
            this.radioScroll.Location = new System.Drawing.Point(19, 54);
            this.radioScroll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioScroll.Name = "radioScroll";
            this.radioScroll.Size = new System.Drawing.Size(125, 21);
            this.radioScroll.TabIndex = 1;
            this.radioScroll.TabStop = true;
            this.radioScroll.Text = "Sticky Gradient";
            this.radioScroll.UseVisualStyleBackColor = true;
            this.radioScroll.CheckedChanged += new System.EventHandler(this.radioScroll_CheckedChanged);
            // 
            // radioOFF
            // 
            this.radioOFF.AutoSize = true;
            this.radioOFF.Location = new System.Drawing.Point(19, 27);
            this.radioOFF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioOFF.Name = "radioOFF";
            this.radioOFF.Size = new System.Drawing.Size(48, 21);
            this.radioOFF.TabIndex = 0;
            this.radioOFF.Text = "Off";
            this.radioOFF.UseVisualStyleBackColor = true;
            this.radioOFF.CheckedChanged += new System.EventHandler(this.radioOFF_CheckedChanged);
            // 
            // chkLEDAnimation
            // 
            this.chkLEDAnimation.AutoSize = true;
            this.chkLEDAnimation.Checked = true;
            this.chkLEDAnimation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLEDAnimation.Location = new System.Drawing.Point(27, 39);
            this.chkLEDAnimation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkLEDAnimation.Name = "chkLEDAnimation";
            this.chkLEDAnimation.Size = new System.Drawing.Size(123, 21);
            this.chkLEDAnimation.TabIndex = 7;
            this.chkLEDAnimation.Text = "LED Animation";
            this.chkLEDAnimation.UseVisualStyleBackColor = true;
            this.chkLEDAnimation.CheckedChanged += new System.EventHandler(this.chkLEDAnimation_CheckedChanged);
            // 
            // chkLogitechColours
            // 
            this.chkLogitechColours.AutoSize = true;
            this.chkLogitechColours.Checked = true;
            this.chkLogitechColours.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLogitechColours.Location = new System.Drawing.Point(155, 39);
            this.chkLogitechColours.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkLogitechColours.Name = "chkLogitechColours";
            this.chkLogitechColours.Size = new System.Drawing.Size(173, 21);
            this.chkLogitechColours.TabIndex = 8;
            this.chkLogitechColours.Text = "Keep Logitech Colours";
            this.chkLogitechColours.UseVisualStyleBackColor = true;
            this.chkLogitechColours.CheckedChanged += new System.EventHandler(this.chkLogitechColours_CheckedChanged);
            // 
            // picStartColour
            // 
            this.picStartColour.Location = new System.Drawing.Point(233, 82);
            this.picStartColour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picStartColour.Name = "picStartColour";
            this.picStartColour.Size = new System.Drawing.Size(37, 39);
            this.picStartColour.TabIndex = 9;
            this.picStartColour.TabStop = false;
            this.picStartColour.Click += new System.EventHandler(this.picStartColour_Click);
            // 
            // picEndColour
            // 
            this.picEndColour.Location = new System.Drawing.Point(283, 82);
            this.picEndColour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picEndColour.Name = "picEndColour";
            this.picEndColour.Size = new System.Drawing.Size(37, 39);
            this.picEndColour.TabIndex = 10;
            this.picEndColour.TabStop = false;
            this.picEndColour.Click += new System.EventHandler(this.picEndColour_Click);
            // 
            // numGradientSpeed
            // 
            this.numGradientSpeed.Location = new System.Drawing.Point(137, 110);
            this.numGradientSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numGradientSpeed.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numGradientSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGradientSpeed.Name = "numGradientSpeed";
            this.numGradientSpeed.Size = new System.Drawing.Size(72, 22);
            this.numGradientSpeed.TabIndex = 11;
            this.numGradientSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGradientSpeed.ValueChanged += new System.EventHandler(this.numGradientSpeed_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Gradient Speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Fade Speed";
            // 
            // numFadeSpeed
            // 
            this.numFadeSpeed.Location = new System.Drawing.Point(137, 140);
            this.numFadeSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numFadeSpeed.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numFadeSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFadeSpeed.Name = "numFadeSpeed";
            this.numFadeSpeed.Size = new System.Drawing.Size(72, 22);
            this.numFadeSpeed.TabIndex = 13;
            this.numFadeSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFadeSpeed.ValueChanged += new System.EventHandler(this.numFadeSpeed_ValueChanged);
            // 
            // chkWave
            // 
            this.chkWave.AutoSize = true;
            this.chkWave.Checked = true;
            this.chkWave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWave.Location = new System.Drawing.Point(28, 212);
            this.chkWave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkWave.Name = "chkWave";
            this.chkWave.Size = new System.Drawing.Size(66, 21);
            this.chkWave.TabIndex = 15;
            this.chkWave.Text = "Wave";
            this.chkWave.UseVisualStyleBackColor = true;
            this.chkWave.CheckedChanged += new System.EventHandler(this.chkWave_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Animation Delay";
            // 
            // numAnimationSpeed
            // 
            this.numAnimationSpeed.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numAnimationSpeed.Location = new System.Drawing.Point(137, 78);
            this.numAnimationSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numAnimationSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numAnimationSpeed.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numAnimationSpeed.Name = "numAnimationSpeed";
            this.numAnimationSpeed.Size = new System.Drawing.Size(72, 22);
            this.numAnimationSpeed.TabIndex = 16;
            this.numAnimationSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numAnimationSpeed.ValueChanged += new System.EventHandler(this.numAnimationSpeed_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Wave Falloff";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "Effect Distance";
            // 
            // numDistanceFalloff
            // 
            this.numDistanceFalloff.DecimalPlaces = 1;
            this.numDistanceFalloff.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDistanceFalloff.Location = new System.Drawing.Point(137, 174);
            this.numDistanceFalloff.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numDistanceFalloff.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDistanceFalloff.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDistanceFalloff.Name = "numDistanceFalloff";
            this.numDistanceFalloff.Size = new System.Drawing.Size(72, 22);
            this.numDistanceFalloff.TabIndex = 20;
            this.numDistanceFalloff.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDistanceFalloff.ValueChanged += new System.EventHandler(this.numDistanceFalloff_ValueChanged);
            // 
            // numWaveSpeed
            // 
            this.numWaveSpeed.DecimalPlaces = 1;
            this.numWaveSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numWaveSpeed.Location = new System.Drawing.Point(229, 210);
            this.numWaveSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numWaveSpeed.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numWaveSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numWaveSpeed.Name = "numWaveSpeed";
            this.numWaveSpeed.Size = new System.Drawing.Size(91, 22);
            this.numWaveSpeed.TabIndex = 22;
            this.numWaveSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWaveSpeed.ValueChanged += new System.EventHandler(this.numWaveSpeed_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 436);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(381, 28);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadProfileToolStripMenuItem,
            this.saveProfileToolStripMenuItem,
            this.setAsDefaultToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadProfileToolStripMenuItem
            // 
            this.loadProfileToolStripMenuItem.Name = "loadProfileToolStripMenuItem";
            this.loadProfileToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.loadProfileToolStripMenuItem.Text = "Load Profile...";
            this.loadProfileToolStripMenuItem.Click += new System.EventHandler(this.loadProfileToolStripMenuItem_Click);
            // 
            // saveProfileToolStripMenuItem
            // 
            this.saveProfileToolStripMenuItem.Name = "saveProfileToolStripMenuItem";
            this.saveProfileToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.saveProfileToolStripMenuItem.Text = "Save Profile...";
            this.saveProfileToolStripMenuItem.Click += new System.EventHandler(this.saveProfileToolStripMenuItem_Click);
            // 
            // setAsDefaultToolStripMenuItem
            // 
            this.setAsDefaultToolStripMenuItem.Name = "setAsDefaultToolStripMenuItem";
            this.setAsDefaultToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.setAsDefaultToolStripMenuItem.Text = "Set as Default";
            this.setAsDefaultToolStripMenuItem.Click += new System.EventHandler(this.setAsDefaultToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lstProfiles
            // 
            this.lstProfiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstProfiles.FormattingEnabled = true;
            this.lstProfiles.ItemHeight = 16;
            this.lstProfiles.Location = new System.Drawing.Point(0, 0);
            this.lstProfiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstProfiles.Name = "lstProfiles";
            this.lstProfiles.Size = new System.Drawing.Size(381, 436);
            this.lstProfiles.TabIndex = 24;
            this.lstProfiles.Visible = false;
            this.lstProfiles.SelectedIndexChanged += new System.EventHandler(this.lstProfiles_SelectedIndexChanged);
            this.lstProfiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstProfiles_MouseDoubleClick);
            // 
            // cmdCancelProfileLoad
            // 
            this.cmdCancelProfileLoad.Location = new System.Drawing.Point(295, 470);
            this.cmdCancelProfileLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdCancelProfileLoad.Name = "cmdCancelProfileLoad";
            this.cmdCancelProfileLoad.Size = new System.Drawing.Size(75, 25);
            this.cmdCancelProfileLoad.TabIndex = 25;
            this.cmdCancelProfileLoad.Text = "cancel";
            this.cmdCancelProfileLoad.UseVisualStyleBackColor = true;
            this.cmdCancelProfileLoad.Visible = false;
            this.cmdCancelProfileLoad.Click += new System.EventHandler(this.cmdCancelProfileLoad_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Logitech Vent Monitor";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // chkKeySaver
            // 
            this.chkKeySaver.AutoSize = true;
            this.chkKeySaver.Checked = true;
            this.chkKeySaver.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeySaver.Location = new System.Drawing.Point(27, 262);
            this.chkKeySaver.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkKeySaver.Name = "chkKeySaver";
            this.chkKeySaver.Size = new System.Drawing.Size(118, 21);
            this.chkKeySaver.TabIndex = 26;
            this.chkKeySaver.Text = "Use Keysaver";
            this.chkKeySaver.UseVisualStyleBackColor = true;
            this.chkKeySaver.CheckedChanged += new System.EventHandler(this.chkKeySaver_CheckedChanged);
            // 
            // numKeySaverTime
            // 
            this.numKeySaverTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numKeySaverTime.Location = new System.Drawing.Point(229, 258);
            this.numKeySaverTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numKeySaverTime.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numKeySaverTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numKeySaverTime.Name = "numKeySaverTime";
            this.numKeySaverTime.Size = new System.Drawing.Size(91, 22);
            this.numKeySaverTime.TabIndex = 27;
            this.numKeySaverTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numKeySaverTime.ValueChanged += new System.EventHandler(this.numKeySaverTime_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(149, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Time (sec)";
            // 
            // chkRandomColours
            // 
            this.chkRandomColours.AutoSize = true;
            this.chkRandomColours.Checked = true;
            this.chkRandomColours.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandomColours.Location = new System.Drawing.Point(233, 138);
            this.chkRandomColours.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRandomColours.Name = "chkRandomColours";
            this.chkRandomColours.Size = new System.Drawing.Size(135, 21);
            this.chkRandomColours.TabIndex = 29;
            this.chkRandomColours.Text = "Random Colours";
            this.chkRandomColours.UseVisualStyleBackColor = true;
            this.chkRandomColours.CheckedChanged += new System.EventHandler(this.chkRandomColours_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 508);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "CPU Temp";
            // 
            // lblCpuTemp
            // 
            this.lblCpuTemp.AutoSize = true;
            this.lblCpuTemp.Location = new System.Drawing.Point(108, 508);
            this.lblCpuTemp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCpuTemp.Name = "lblCpuTemp";
            this.lblCpuTemp.Size = new System.Drawing.Size(31, 17);
            this.lblCpuTemp.TabIndex = 31;
            this.lblCpuTemp.Text = "N/A";
            // 
            // lblGPUTemp
            // 
            this.lblGPUTemp.AutoSize = true;
            this.lblGPUTemp.Location = new System.Drawing.Point(278, 508);
            this.lblGPUTemp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGPUTemp.Name = "lblGPUTemp";
            this.lblGPUTemp.Size = new System.Drawing.Size(31, 17);
            this.lblGPUTemp.TabIndex = 33;
            this.lblGPUTemp.Text = "N/A";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(194, 508);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 17);
            this.label10.TabIndex = 32;
            this.label10.Text = "GPU Temp";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 535);
            this.Controls.Add(this.lblGPUTemp);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblCpuTemp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkRandomColours);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numKeySaverTime);
            this.Controls.Add(this.chkKeySaver);
            this.Controls.Add(this.cmdCancelProfileLoad);
            this.Controls.Add(this.numWaveSpeed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numDistanceFalloff);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numAnimationSpeed);
            this.Controls.Add(this.chkWave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numFadeSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numGradientSpeed);
            this.Controls.Add(this.picEndColour);
            this.Controls.Add(this.picStartColour);
            this.Controls.Add(this.chkLogitechColours);
            this.Controls.Add(this.chkLEDAnimation);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblSpeaking);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkUseArx);
            this.Controls.Add(this.lblArxStatus);
            this.Controls.Add(this.lblArxStatusHeader);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lstProfiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Logitech Vent Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStartColour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEndColour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGradientSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFadeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAnimationSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceFalloff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaveSpeed)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKeySaverTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblArxStatusHeader;
        private System.Windows.Forms.Label lblArxStatus;
        private System.Windows.Forms.CheckBox chkUseArx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSpeaking;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioSpell;
        private System.Windows.Forms.RadioButton radioScroll;
        private System.Windows.Forms.RadioButton radioOFF;
        private System.Windows.Forms.CheckBox chkLEDAnimation;
        private System.Windows.Forms.CheckBox chkLogitechColours;
        private System.Windows.Forms.PictureBox picStartColour;
        private System.Windows.Forms.PictureBox picEndColour;
        private System.Windows.Forms.NumericUpDown numGradientSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numFadeSpeed;
        private System.Windows.Forms.CheckBox chkWave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numAnimationSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numDistanceFalloff;
        private System.Windows.Forms.NumericUpDown numWaveSpeed;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsDefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListBox lstProfiles;
        private System.Windows.Forms.Button cmdCancelProfileLoad;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox chkKeySaver;
        private System.Windows.Forms.NumericUpDown numKeySaverTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkRandomColours;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCpuTemp;
        private System.Windows.Forms.Label lblGPUTemp;
        private System.Windows.Forms.Label label10;

    }
}

