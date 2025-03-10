namespace AutoClicker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.screenshotButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxSteps = new System.Windows.Forms.TextBox();
            this.loadStepsButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.compteurLabel = new System.Windows.Forms.Label();
            this.countLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.createFormBtn = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.clearLogsButton = new System.Windows.Forms.Button();
            this.counterValueTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // screenshotButton
            // 
            this.screenshotButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.screenshotButton.Location = new System.Drawing.Point(324, 242);
            this.screenshotButton.Name = "screenshotButton";
            this.screenshotButton.Size = new System.Drawing.Size(191, 30);
            this.screenshotButton.TabIndex = 2;
            this.screenshotButton.Text = "Take a screenshot";
            this.screenshotButton.UseVisualStyleBackColor = true;
            this.screenshotButton.Click += new System.EventHandler(this.screenshotButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(324, 278);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxSteps
            // 
            this.textBoxSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSteps.Location = new System.Drawing.Point(8, 48);
            this.textBoxSteps.Multiline = true;
            this.textBoxSteps.Name = "textBoxSteps";
            this.textBoxSteps.Size = new System.Drawing.Size(301, 350);
            this.textBoxSteps.TabIndex = 4;
            // 
            // loadStepsButton
            // 
            this.loadStepsButton.Location = new System.Drawing.Point(8, 12);
            this.loadStepsButton.Name = "loadStepsButton";
            this.loadStepsButton.Size = new System.Drawing.Size(301, 30);
            this.loadStepsButton.TabIndex = 5;
            this.loadStepsButton.Text = "Load steps";
            this.loadStepsButton.UseVisualStyleBackColor = true;
            this.loadStepsButton.Click += new System.EventHandler(this.loadStepsButton_Click);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(324, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(191, 30);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start (CTRL+ALT+A)";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(324, 98);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(191, 30);
            this.pauseButton.TabIndex = 7;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.continueButton.Enabled = false;
            this.continueButton.Location = new System.Drawing.Point(324, 134);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(191, 30);
            this.continueButton.TabIndex = 8;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // compteurLabel
            // 
            this.compteurLabel.AutoSize = true;
            this.compteurLabel.Location = new System.Drawing.Point(359, 51);
            this.compteurLabel.Name = "compteurLabel";
            this.compteurLabel.Size = new System.Drawing.Size(68, 16);
            this.compteurLabel.TabIndex = 9;
            this.compteurLabel.Text = "Compteur:";
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(459, 51);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(14, 16);
            this.countLabel.TabIndex = 10;
            this.countLabel.Text = "0";
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(324, 170);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(191, 30);
            this.stopButton.TabIndex = 12;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // createFormBtn
            // 
            this.createFormBtn.Location = new System.Drawing.Point(8, 404);
            this.createFormBtn.Name = "createFormBtn";
            this.createFormBtn.Size = new System.Drawing.Size(301, 30);
            this.createFormBtn.TabIndex = 13;
            this.createFormBtn.Text = "Create steps";
            this.createFormBtn.UseVisualStyleBackColor = true;
            this.createFormBtn.Click += new System.EventHandler(this.createFormBtn_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.Location = new System.Drawing.Point(8, 448);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(507, 150);
            this.logTextBox.TabIndex = 14;
            // 
            // clearLogsButton
            // 
            this.clearLogsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearLogsButton.Location = new System.Drawing.Point(324, 604);
            this.clearLogsButton.Name = "clearLogsButton";
            this.clearLogsButton.Size = new System.Drawing.Size(191, 30);
            this.clearLogsButton.TabIndex = 15;
            this.clearLogsButton.Text = "Effacer les logs";
            this.clearLogsButton.UseVisualStyleBackColor = true;
            this.clearLogsButton.Click += new System.EventHandler(this.clearLogsButton_Click);
            // 
            // counterValueTextBox
            // 
            this.counterValueTextBox.Location = new System.Drawing.Point(479, 48);
            this.counterValueTextBox.Name = "counterValueTextBox";
            this.counterValueTextBox.Size = new System.Drawing.Size(60, 22);
            this.counterValueTextBox.TabIndex = 11;
            this.counterValueTextBox.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 648);
            this.Controls.Add(this.clearLogsButton);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.createFormBtn);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.counterValueTextBox);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.compteurLabel);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.loadStepsButton);
            this.Controls.Add(this.textBoxSteps);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.screenshotButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "StepFree Data Extractor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button screenshotButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxSteps;
        private System.Windows.Forms.Button loadStepsButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label compteurLabel;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button createFormBtn;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Button clearLogsButton;
        private System.Windows.Forms.TextBox counterValueTextBox;
    }
}