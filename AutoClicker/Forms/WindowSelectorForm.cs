using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoClicker.Forms
{
    public partial class WindowSelectorForm : Form
    {
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public string SelectedWindowTitle { get; private set; }

        public WindowSelectorForm()
        {
            InitializeComponent();
            LoadWindows();
        }
        private void LoadWindows()
        {
            List<string> windowTitles = new List<string>();
            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    StringBuilder title = new StringBuilder(256);
                    GetWindowText(hWnd, title, 256);
                    string windowTitle = title.ToString().Trim();
                    if (!string.IsNullOrEmpty(windowTitle))
                    {
                        windowTitles.Add(windowTitle);
                    }
                }
                return true;
            }, IntPtr.Zero);

            windowListBox.Items.AddRange(windowTitles.ToArray());
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (windowListBox.SelectedItem != null)
            {
                SelectedWindowTitle = windowListBox.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a window", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void WindowListBox_DoubleClick(object sender, EventArgs e)
        {
            SelectButton_Click(sender, e);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}