using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AutoClicker
{
    public static class MouseHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }

        private const uint INPUT_MOUSE = 0;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static Point GetMousePosition()
        {
            GetCursorPos(out Point currentMousePoint);
            return currentMousePoint;
        }

        public static void MoveMouseTo(int x, int y)
        {
            SetCursorPos(x, y);
            Task.Delay(50).Wait(); // Petit délai pour s'assurer que le curseur est bien positionné
        }

        public static void ClickAt(int x, int y, bool leftClick = false)
        {
            MoveMouseTo(x, y);
            Task.Delay(50).Wait();

            INPUT[] inputs = new INPUT[2];
            inputs[0].type = INPUT_MOUSE;
            inputs[1].type = INPUT_MOUSE;

            if (leftClick)
            {
                inputs[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
                inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;
            }
            else
            {
                inputs[0].mi.dwFlags = MOUSEEVENTF_RIGHTDOWN;
                inputs[1].mi.dwFlags = MOUSEEVENTF_RIGHTUP;
            }

            // Envoi des événements de clic
            SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT)));
            Task.Delay(50).Wait();
        }
    }
}