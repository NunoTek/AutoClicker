using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoClicker
{
    public static class ScreenHelper
    {
        public static Color GetPixelColor(int x, int y, Bitmap screenshot = null)
        {
            //if (screenshot == null) throw new InvalidOperationException("La capture d'écran n'a pas encore été prise.");

            if (screenshot == null)
            {
                screenshot = TakeScreenshot();
            }

            if (x < 0 || x >= screenshot.Width || y < 0 || y >= screenshot.Height)
                throw new ArgumentOutOfRangeException("Les coordonnées du pixel sont hors de la plage de l'image.");

            return screenshot.GetPixel(x, y);
        }

        public static Bitmap TakeScreenshot()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            return TakeScreenshot(0, 0, bounds.Width, bounds.Height, false);
        }



        [DllImport("User32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("User32.dll")]
        private static extern IntPtr GetCursor();

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern int DrawIcon(IntPtr hdc, int X, int Y, IntPtr hIcon);


        public static Bitmap TakeScreenshot(int x, int y, int width, int height, bool includeCursor)
        {
            // Capture the screen
            var hdcSrc = GetWindowDC(GetDesktopWindow());

            // Create a bitmap to hold the screenshot
            var bmpScreenShot = new Bitmap(width, height);
             
            // Create graphics object for drawing on the bitmap
            using (Graphics g = Graphics.FromImage(bmpScreenShot))
            {
                IntPtr hdcDest = g.GetHdc();

                // Copy the screen area to the bitmap
                int result = BitBlt(hdcDest, 0, 0, width, height,
                                    hdcSrc, x, y, TernaryRasterOperations.SRCCOPY);

                if (includeCursor)
                {
                    CURSORINFO ci;
                    GetCursorInfo(out ci);

                    // Calculate cursor position relative to the screenshot area
                    int cursorX = ci.ptScreenPos.X - x;
                    int cursorY = ci.ptScreenPos.Y - y;

                    IntPtr hIcon = GetCursor();
                    if (hIcon != IntPtr.Zero)
                    {
                        DrawIcon(hdcDest, cursorX, cursorY, hIcon);
                    }
                }

                g.ReleaseHdc(hdcDest);

                return bmpScreenShot;
            }
        }

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern int BitBlt(IntPtr hObject, int nXDest, int nYDest,
                                          int nWidth, int nHeight, IntPtr hObjSource,
                                          int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            // Other raster operations here if needed
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public int cbSize;
            public CURSOR_SHOWSTATE flags;
            public IntPtr hCursor;
            public POINT ptScreenPos;
        }

        public enum CURSOR_SHOWSTATE : uint
        {
            CURSOR_SHOWING = 0x1,
            CURSOR_HIDDEN = 0x2
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

    }
}
