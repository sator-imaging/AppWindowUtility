#if UNITY_STANDALONE_WIN

using System;
using UnityEngine;



namespace SatorImaging.AppWindowUtility
{
    public class Windows : IPlatformDependent
    {
        static IntPtr hWnd;

        static uint defaultWindowStyle;
        static uint defaultExWindowStyle;

        // unity's shader error color.
        static uint defaultKeyingColor = 0xFF00FF;
        static int borderWidth;
        static int titleBarHeight;


        private static bool isAlwaysOnTop = false;
        private static bool isTransparent = false;
        private static bool isFrameVisible = true;
        private static bool isClickThrough = false;
        private static bool isWallpaper = false;

        private static bool isInitialized = false;
        private static WinApi.RECT lastClientRect = new WinApi.RECT { left = 32, top = 64, right = 640, bottom = 480 };



        // features supported
        public bool AlwaysOnTopSupported { get; } = true;
        public bool TransparentSupported { get; } = true;
        public bool FrameVisibilitySupported { get; } = true;
        public bool ClickThroughSupported { get; } = true;
        public bool AsWallpaperSupported { get; } = true;
        public bool KeyingColorSupported { get; } = true;
        public bool WindowOpacitySupported { get; } = true;
        public bool WindowPlacementSupported { get; } = true;



        public Windows()
        {
            if (isInitialized) return;

            hWnd = WinApi.GetUnityWindowHandle();
            defaultWindowStyle = WinApi.GetWindowLong(WinApi.GetUnityWindowHandle(), WinApi.GWL_STYLE);
            defaultExWindowStyle = WinApi.GetWindowLong(WinApi.GetUnityWindowHandle(), WinApi.GWL_EXSTYLE);

            // initialize client rect.
            WinApi.GetClientRect(hWnd, out lastClientRect);

            // calculate title bar and frame size
            WinApi.RECT windowRect;
            WinApi.GetWindowRect(hWnd, out windowRect);
            WinApi.RECT clientRect;
            WinApi.GetClientRect(hWnd, out clientRect);
            borderWidth = (windowRect.right - windowRect.left) - clientRect.right;
            borderWidth = (int)(borderWidth * 0.5f);
            titleBarHeight = (windowRect.bottom - windowRect.top) - clientRect.bottom - borderWidth;


            //Debug.Log($"{typeof(AppWindowUtility).FullName}.Initialize: Title Bar {titleBarHeight}px / Border {borderWidth}px");

            isInitialized = true;
        }//




        public void ResetStyle()
        {
            WinApi.SetWindowLong(hWnd, WinApi.GWL_STYLE, defaultWindowStyle);
            WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, defaultExWindowStyle);
        }//





        public bool GetAlwaysOnTop() => isAlwaysOnTop;
        public void SetAlwaysOnTop(bool enable)
        {
            WinApi.SetWindowPos(
                hWnd,
                enable ? WinApi.HWND_TOPMOST : WinApi.HWND_NOTOPMOST,
                0, 0, 0, 0,
                WinApi.SetWindowPosFlags.IgnoreMove | WinApi.SetWindowPosFlags.IgnoreResize
            );

            isAlwaysOnTop = enable;
        }//


        public bool GetTransparent() => isTransparent;
        public void SetTransparent(bool enable)
        {
            if (enable)
            {
                SetFrameVisibility(false);

                var currExStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_EXSTYLE);
                WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, currExStyle & ~WinApi.WS_EX_LAYERED);
                WinApi.SetDwmTransparent(true);

            }
            else
            {
                SetFrameVisibility(true);
                WinApi.SetDwmTransparent(false);
            }

            isTransparent = enable;
        }//




        public void SetKeyingColor(byte red, byte green, byte blue)
        {
            var currExStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_EXSTYLE);
            WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, currExStyle | WinApi.WS_EX_LAYERED);// | WindowsApi.WS_EX_TRANSPARENT);

            var color = (uint)(blue + (green << 8) + (red << 16));
            WinApi.SetLayeredWindowAttributes(hWnd, color, 0xFF, WinApi.LWA_COLORKEY);
        }//




        public bool GetFrameVisibility() => isFrameVisible;
        public void SetFrameVisibility(bool visible)
        {
            if (AppWindowUtility.FullScreen) return;
            if (visible == isFrameVisible) return;

            if (visible)
            {
                // must be done BEFORE SetWindowLong
                MoveWindowRelative(-borderWidth, -titleBarHeight);
                //ResizeWindowRelative(borderWidth * 2, titleBarHeight + borderWidth);

                WinApi.SetWindowLong(hWnd, WinApi.GWL_STYLE, defaultWindowStyle);

                // to make uGUI correct.
                WinApi.SetWindowPos(hWnd, IntPtr.Zero,
                    lastClientRect.left - borderWidth,
                    lastClientRect.top - titleBarHeight,
                    lastClientRect.right - lastClientRect.left + borderWidth * 2,
                    lastClientRect.bottom - lastClientRect.top + titleBarHeight + borderWidth,
                    WinApi.SetWindowPosFlags.FrameChanged | WinApi.SetWindowPosFlags.IgnoreMove
                );

            }
            else
            {
                // store last client size for showing frame again
                if (!AppWindowUtility.FullScreen)
                {
                    WinApi.GetClientRect(hWnd, out lastClientRect);
                    //Debug.Log($"SetFrameVisibility: Client Rect stored.");
                }

                var currStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_STYLE);
                WinApi.SetWindowLong(hWnd, WinApi.GWL_STYLE, currStyle & ~WinApi.WS_BORDER & ~WinApi.WS_THICKFRAME & ~WinApi.WS_CAPTION);

                //// must be done AFTER SetWindowLong
                MoveWindowRelative(borderWidth, titleBarHeight);
                // must be change window size 2 times to work correctly, idk why.
                WinApi.SetWindowPos(hWnd, IntPtr.Zero,
                    lastClientRect.left - borderWidth,
                    lastClientRect.top - titleBarHeight,
                    lastClientRect.right,
                    lastClientRect.bottom + 1,
                    WinApi.SetWindowPosFlags.FrameChanged | WinApi.SetWindowPosFlags.IgnoreMove
                );
                WinApi.SetWindowPos(hWnd, IntPtr.Zero,
                    lastClientRect.left - borderWidth,
                    lastClientRect.top - titleBarHeight,
                    lastClientRect.right,
                    lastClientRect.bottom - 1,
                    WinApi.SetWindowPosFlags.FrameChanged | WinApi.SetWindowPosFlags.IgnoreMove
                );

            }

            isFrameVisible = visible;

        }//





        public bool GetClickThrough() => isClickThrough;
        public void SetClickThrough(bool enable)
        {
            if (enable)
            {
                var currExStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_EXSTYLE);
                WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, currExStyle | WinApi.WS_EX_LAYERED | WinApi.WS_EX_TRANSPARENT);
            }
            else
            {
                WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, defaultExWindowStyle);
            }

            isClickThrough = enable;
        }//



        public void SetWindowOpacity(byte opacity)
        {
            ////////if(0xFF == opacity)
            {
                var currExStyle = WinApi.GetWindowLong(hWnd, WinApi.GWL_EXSTYLE);
                WinApi.SetWindowLong(hWnd, WinApi.GWL_EXSTYLE, currExStyle | WinApi.WS_EX_LAYERED);
                WinApi.SetLayeredWindowAttributes(hWnd, defaultKeyingColor, opacity, WinApi.LWA_ALPHA);
            }

            /* do not reset for combination with Transparent
            else
            {
                ResetStyle();
            }
            */
        }//



        public bool GetAsWallpaper() => isWallpaper;
        public void SetAsWallpaper(bool enable)
        {
        }//



        public void MoveWindowRelative(int relativeX, int relativeY)
        {
            if (AppWindowUtility.FullScreen) return;

            WinApi.RECT rect;
            WinApi.GetWindowRect(hWnd, out rect);
            WinApi.SetWindowPos(hWnd, IntPtr.Zero,
                rect.left + relativeX,
                rect.top + relativeY,
                rect.right - rect.left,
                rect.bottom - rect.top,
                WinApi.SetWindowPosFlags.NoFlag //ApiWindows.SetWindowPosFlags.IgnoreResize
            );
        }//



        public void ResizeWindowRelative(int relativeWidth, int relativeHeight)
        {
            if (AppWindowUtility.FullScreen) return;

            WinApi.RECT rect;
            WinApi.GetWindowRect(hWnd, out rect);
            WinApi.SetWindowPos(hWnd, IntPtr.Zero,
                rect.left,
                rect.top,
                rect.right - rect.left + relativeWidth,
                rect.bottom - rect.top + relativeHeight,
                WinApi.SetWindowPosFlags.NoFlag //ApiWindows.SetWindowPosFlags.IgnoreMove
            );

        }//





    }//class
}//namespace
#endif
