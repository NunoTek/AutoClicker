using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoClicker.Forms
{
    public partial class KeyCaptureForm : Form
    {
        private Key _capturedKey = Key.None;
        private ModifierKeys _capturedModifiers;

        public Key CapturedKey => _capturedKey;
        public ModifierKeys CapturedModifiers => _capturedModifiers;

        public KeyCaptureForm()
        {
            InitializeComponent();
            KeyPreview = true; // Permet au formulaire de capturer les touches avant les contrôles enfants
            this.KeyDown += KeyCaptureForm_KeyDown;
        }

        private void KeyCaptureForm_KeyDown(object sender, KeyEventArgs e)
        {
            _capturedKey = KeyInterop.KeyFromVirtualKey((int)e.KeyCode);
            _capturedModifiers = System.Windows.Input.ModifierKeys.None;

            if (e.Control) _capturedModifiers |= System.Windows.Input.ModifierKeys.Control;
            if (e.Alt) _capturedModifiers |= System.Windows.Input.ModifierKeys.Alt;
            if (e.Shift) _capturedModifiers |= System.Windows.Input.ModifierKeys.Shift;

            // Mise à jour du texte pour afficher la touche capturée
            capturedKeyLabel.Text = $"Captured key: {_capturedKey} {(_capturedModifiers != System.Windows.Input.ModifierKeys.None ? $"+ {_capturedModifiers}" : "")}";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Vérifiez si les touches spéciales (flèches, etc.) doivent être gérées ici
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                KeyCaptureForm_KeyDown(this, new KeyEventArgs(keyData)); // Simule l'événement KeyDown
                return true; // Indique que la touche a été traitée
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (_capturedKey != Key.None)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
