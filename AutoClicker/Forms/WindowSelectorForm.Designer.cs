using System.Windows.Forms;

namespace AutoClicker.Forms
{
    partial class WindowSelectorForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowSelectorForm));
            this.windowListBox = new System.Windows.Forms.ListBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // windowListBox
            // 
            this.windowListBox.ItemHeight = 16;
            this.windowListBox.Location = new System.Drawing.Point(12, 12);
            this.windowListBox.Name = "windowListBox";
            this.windowListBox.Size = new System.Drawing.Size(360, 292);
            this.windowListBox.TabIndex = 0;
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(12, 320);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(175, 30);
            this.selectButton.TabIndex = 1;
            this.selectButton.Text = "Select";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(197, 320);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(175, 30);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Annuler";
            // 
            // WindowSelectorForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.windowListBox);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a window";
            this.ResumeLayout(false);

        }

        private ListBox windowListBox;
        private Button selectButton;
        private Button cancelButton;

        #endregion
    }
}
