using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoClicker.Helpers
{
    public static class KeyboardHelper
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int KEYEVENTF_KEYDOWN = 0x0000;
        private const int KEYEVENTF_KEYUP = 0x0002;

        public static void SimulateKeyPress(Key key, ModifierKeys modifiers)
        {
            if ((modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                PressKey(KeyInterop.VirtualKeyFromKey(Key.LeftCtrl));

            if ((modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                PressKey(KeyInterop.VirtualKeyFromKey(Key.LeftAlt));

            if ((modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                PressKey(KeyInterop.VirtualKeyFromKey(Key.LeftShift));

            PressKey(KeyInterop.VirtualKeyFromKey(key));
        }

        private static void PressKey(int virtualKey)
        {
            keybd_event((byte)virtualKey, 0, KEYEVENTF_KEYDOWN, 0);
            Task.Delay(100).Wait();
            keybd_event((byte)virtualKey, 0, KEYEVENTF_KEYUP, 0);
        }
    }
}