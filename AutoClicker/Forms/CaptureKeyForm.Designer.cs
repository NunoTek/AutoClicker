namespace AutoClicker.Forms
{
    partial class KeyCaptureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyCaptureForm));
            this.capturedKeyLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // capturedKeyLabel
            // 
            this.capturedKeyLabel.AutoSize = true;
            this.capturedKeyLabel.Location = new System.Drawing.Point(12, 20);
            this.capturedKeyLabel.Name = "capturedKeyLabel";
            this.capturedKeyLabel.Size = new System.Drawing.Size(158, 16);
            this.capturedKeyLabel.TabIndex = 0;
            this.capturedKeyLabel.Text = "Appuyez sur une touche...";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 50);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(260, 30);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // KeyCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 100);
            this.Controls.Add(this.capturedKeyLabel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeyCaptureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Capture de touche";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label capturedKeyLabel;
        private System.Windows.Forms.Button okButton;
    }
}