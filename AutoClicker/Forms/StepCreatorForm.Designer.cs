using System.Drawing;
using System.Windows.Forms;

namespace AutoClicker
{
    partial class StepCreatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StepCreatorForm));
            this.stepsTextBox = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.doneButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.commandListBox = new System.Windows.Forms.ListBox();
            this.stepsTreeView = new System.Windows.Forms.TreeView();
            this.treeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStepButton = new System.Windows.Forms.Button();
            this.treeViewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // stepsTextBox
            // 
            this.stepsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stepsTextBox.Location = new System.Drawing.Point(12, 444);
            this.stepsTextBox.Multiline = true;
            this.stepsTextBox.Name = "stepsTextBox";
            this.stepsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.stepsTextBox.Size = new System.Drawing.Size(630, 198);
            this.stepsTextBox.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "StepFree Data Extractor";
            this.notifyIcon1.Visible = true;
            // 
            // doneButton
            // 
            this.doneButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doneButton.ImageKey = "(none)";
            this.doneButton.Location = new System.Drawing.Point(12, 652);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(630, 30);
            this.doneButton.TabIndex = 3;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // commandListBox
            // 
            this.commandListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.commandListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.commandListBox.FormattingEnabled = true;
            this.commandListBox.ItemHeight = 20;
            this.commandListBox.Items.AddRange(new object[] {
            "Actions:",
            "    Wait (CTRL+ALT+W)",
            "    Increment (CTRL+ALT+I)",
            "    Mouse Move (CTRL+ALT+M)",
            "    Mouse Left Click(CTRL+ALT+L)",
            "    Mouse Right Click (CTRL+ALT+R)",
            "    Keyboard Press (CTRL+ALT+K)",
            "    Activate Window (CTRL+ALT+F)",
            "    Write Counter (CTRL+ALT+C)",
            "Conditions:",
            "    Check Pixel Condition (CTRL+ALT+P)",
            "    Check Focused Window Title Condition (CTRL+ALT+T)",
            "Blocs conditionnels:",
            "    If",
            "    Else",
            "    While",
            "    While not",
            "Boucles:",
            "    For"});
            this.commandListBox.Location = new System.Drawing.Point(282, 12);
            this.commandListBox.Name = "commandListBox";
            this.commandListBox.Size = new System.Drawing.Size(360, 404);
            this.commandListBox.TabIndex = 4;
            this.commandListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.commandListBox_DrawItem);
            this.commandListBox.SelectedIndexChanged += new System.EventHandler(this.commandListBox_SelectedIndexChanged);
            this.commandListBox.DoubleClick += new System.EventHandler(this.commandListBox_DoubleClick);
            // 
            // stepsTreeView
            // 
            this.stepsTreeView.AllowDrop = true;
            this.stepsTreeView.ContextMenuStrip = this.treeViewContextMenu;
            this.stepsTreeView.Location = new System.Drawing.Point(12, 12);
            this.stepsTreeView.Name = "stepsTreeView";
            this.stepsTreeView.Size = new System.Drawing.Size(250, 340);
            this.stepsTreeView.TabIndex = 5;
            this.stepsTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stepsTreeView_MouseDown);
            // 
            // treeViewContextMenu
            // 
            this.treeViewContextMenu.ImageScalingSize = new System.Drawing.Size(21, 21);
            this.treeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameItem,
            this.copyItem,
            this.pasteItem});
            this.treeViewContextMenu.Name = "treeViewContextMenu";
            this.treeViewContextMenu.Size = new System.Drawing.Size(159, 56);
            // 
            // renameItem
            // 
            this.renameItem.Name = "renameItem";
            this.renameItem.Size = new System.Drawing.Size(158, 26);
            this.renameItem.Text = "Renommer";
            // 
            // copyItem
            // 
            this.copyItem.Name = "copyItem";
            this.copyItem.Size = new System.Drawing.Size(158, 26);
            this.copyItem.Text = "Copier";
            // 
            // pasteItem
            // 
            this.pasteItem.Name = "pasteItem";
            this.pasteItem.Size = new System.Drawing.Size(32, 19);
            this.pasteItem.Text = "Coller";
            // 
            // deleteStepButton
            // 
            this.deleteStepButton.Location = new System.Drawing.Point(12, 398);
            this.deleteStepButton.Name = "deleteStepButton";
            this.deleteStepButton.Size = new System.Drawing.Size(250, 30);
            this.deleteStepButton.TabIndex = 7;
            this.deleteStepButton.Text = "Delete step";
            this.deleteStepButton.Click += new System.EventHandler(this.deleteStepButton_Click);
            // 
            // StepCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 692);
            this.Controls.Add(this.stepsTreeView);
            this.Controls.Add(this.deleteStepButton);
            this.Controls.Add(this.commandListBox);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.stepsTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StepCreatorForm";
            this.Text = "Step Creation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StepCreatorForm_Closing);
            this.Load += new System.EventHandler(this.StepCreatorForm_Load);
            this.treeViewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox stepsTextBox;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ListBox commandListBox;
        private System.Windows.Forms.TreeView stepsTreeView;
        private System.Windows.Forms.Button deleteStepButton;
        private System.Windows.Forms.ContextMenuStrip treeViewContextMenu;
        private ToolStripMenuItem renameItem;
        private ToolStripMenuItem copyItem;
        private ToolStripMenuItem pasteItem;
    }
}