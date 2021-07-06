#if UNITY_STANDALONE_WIN

using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;



namespace SatorImaging.AppWindowUtility
{
    public static class WinApi
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct DwmMargin
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }



        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Vector2 GetWindowsMousePosition()
        {
            POINT pos;
            if (GetCursorPos(out pos)) return new Vector2(pos.X, pos.Y);
            return Vector2.zero;
        }



        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        [DllImport("user32.dll")]
        public static extern uint SendMessageTimeout(IntPtr hWnd, uint Msg, uint wParam, uint lParam, uint fuFlags, uint uTimeout, out uint lpdwResult);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChild, string lpszClass, string lpszWindow);



        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowName);

        public static IntPtr CurrentWindowHandle = IntPtr.Zero;
        public static IntPtr GetUnityWindowHandle()
        {
            if (CurrentWindowHandle == IntPtr.Zero)
            {
                CurrentWindowHandle = FindWindow(null, Application.productName);
            }
            return CurrentWindowHandle;

        }



        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public static bool IsWindowActive()
        {
            return GetUnityWindowHandle() == GetForegroundWindow();
        }



        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong); /*x uint o int unchecked*/
        [DllImport("user32.dll")]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT rect);

        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);



        [Flags()]
        public enum SetWindowPosFlags : uint
        {
            AsynchronousWindowPosition = 0x4000,
            DeferErase = 0x2000,
            DrawFrame = 0x0020,
            FrameChanged = 0x0020,
            HideWindow = 0x0080,
            DoNotActivate = 0x0010,
            DoNotCopyBits = 0x0100,
            IgnoreMove = 0x0002,
            DoNotChangeOwnerZOrder = 0x0200,
            DoNotRedraw = 0x0008,
            DoNotReposition = 0x0200,
            DoNotSendChangingEvent = 0x0400,
            IgnoreResize = 0x0001,
            IgnoreZOrder = 0x0004,
            ShowWindow = 0x0040,
            NoFlag = 0x0000,
        }






        [DllImport("Dwmapi.dll")]
        public static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref DwmMargin margins);
        public static void SetDwmTransparent(bool enable)
        {
            var margins = new DwmMargin()
            {
                cxLeftWidth = enable ? -1 : 0,
            };
            DwmExtendFrameIntoClientArea(GetUnityWindowHandle(), ref margins);
        }





        public const int GWL_STYLE = -16;
        public const uint WS_POPUP = 0x80000000;
        public const uint WS_BORDER = 0x00800000;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_CAPTION = 0x00C00000;
        public const uint WS_THICKFRAME = 0x00040000;
        public const int GWL_EXSTYLE = -20;
        public const uint WS_EX_LAYERED = 0x00080000;
        public const uint WS_EX_TRANSPARENT = 0x00000020;
        public const uint WS_EX_DLGMODALFRAME = 0x00000001;
        public const uint WS_EX_WINDOWEDGE = 0x00000100;
        public const uint WS_EX_CLIENTEDGE = 0x00000200;
        public const uint WS_EX_STATICEDGE = 0x00020000;


        public const int LWA_COLORKEY = 0x00000001;
        public const int LWA_ALPHA = 0x00000002;





        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);




        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetLayeredWindowAttributes(IntPtr hwnd, out uint crKey, out byte bAlpha, out uint dwFlags);






    }//class
}//namespace
#endif
