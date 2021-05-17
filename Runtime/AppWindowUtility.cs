using UnityEngine;


namespace SatorImaging.AppWindowUtility
{
    public static class AppWindowUtility
    {
        public static IPlatformDependent platform;


        public static bool AlwaysOnTop
        {
            get { return (platform == null) ? false : platform.GetAlwaysOnTop(); }
            set { platform?.SetAlwaysOnTop(value); }
        }

        public static bool Transparent
        {
            get { return (platform == null) ? false : platform.GetTransparent(); }
            set { platform?.SetTransparent(value); }
        }

        public static bool FrameVisibility
        {
            get { return (platform == null) ? true : platform.GetFrameVisibility(); }
            set { platform?.SetFrameVisibility(value); }
        }

        public static bool ClickThrough
        {
            get { return (platform == null) ? false : platform.GetClickThrough(); }
            set { platform?.SetClickThrough(value); }
        }

        public static bool AsWallpaper
        {
            get { return (platform == null) ? false : platform.GetAsWallpaper(); }
            set { platform?.SetAsWallpaper(value); }
        }



        private static int[] lastScreenSize = new int[] { 640, 480 };
        private static bool isFullScreen = (Screen.width == Screen.currentResolution.width && Screen.height == Screen.currentResolution.height);

        public static bool FullScreen
        {
            get { return isFullScreen; }
            set
            {
                if (isFullScreen == value) return;

                if (value)
                {
                    // unity turns window frame visible when returned from fullscreen.
                    // so match the status BEFORE going to full screen.
                    FrameVisibility = true;

                    lastScreenSize = new int[] { Screen.width, Screen.height };
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);// FullScreenMode.FullScreenWindow);
                }
                else
                {
                    Screen.SetResolution(lastScreenSize[0], lastScreenSize[1], false);// FullScreenMode.Windowed);
                }

                isFullScreen = value;

                // no way to control SetResolution update timing. it's done at next frame.
                // so that FrameVisibility executed BEFORE full screen mode turned OFF.
                // to make it better, simply, reset transparent state.
                if (!isFullScreen) Transparent = false;

            }//set
        }//




        public static void SetKeyingColor(byte red, byte green, byte blue)
              => platform?.SetKeyingColor(red, green, blue);

        public static void SetWindowOpacity(byte opacity)
              => platform?.SetWindowOpacity(opacity);




        public static void MoveWindowRelative(int pixelX, int pixelY)
              => platform?.MoveWindowRelative(pixelX, pixelY);



    }//class
}//namespace
