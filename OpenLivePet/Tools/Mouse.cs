using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenLivePet.Tools
{
    internal static class Mouse
    {
        [StructLayout(LayoutKind.Sequential,Pack =1)]
        public struct POINT
        {
            public int X;
            public int Y;
        }
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        private static extern IntPtr GetCursorPos(out POINT p);
        public static Point GetCursorPosition()
        {
            GetCursorPos(out POINT p);
            return new(p.X,p.Y);
        }
    }
}
