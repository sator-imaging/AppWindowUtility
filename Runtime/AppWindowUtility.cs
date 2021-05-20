using UnityEngine;


namespace SatorImaging.AppWindowUtility
{
    public static class AppWindowUtility
    {
        public static IPlatformDependent platform;


        public static bool AlwaysOnTopSupported { get => platform?.AlwaysOnTopSupported ?? false; }
        public static bool AlwaysOnTop
        {
            get => platform?.GetAlwaysOnTop() ?? false;
            set => platform?.SetAlwaysOnTop(value);
        }

        public static bool TransparentSupported { get => platform?.TransparentSupported ?? false; }
        public static bool Transparent
        {
            get => platform?.GetTransparent() ?? false;
            set => platform?.SetTransparent(value);
        }

        public static bool FrameVisiblitySupported { get => platform?.FrameVisibilitySupported ?? false; }
        public static bool FrameVisibility
        {
            get => platform?.GetFrameVisibility() ?? true;
            set => platform?.SetFrameVisibility(value);
        }

        public static bool ClickThroughSupported { get => platform?.ClickThroughSupported ?? false; }
        public static bool ClickThrough
        {
            get => platform?.GetClickThrough() ?? false;
            set => platform?.SetClickThrough(value);
        }

        public static bool AsWallpaperSupported { get => platform?.AsWallpaperSupported ?? false; }
        public static bool AsWallpaper
        {
            get => platform?.GetAsWallpaper() ?? false;
            set => platform?.SetAsWallpaper(value);
        }



        public static bool    KeyingColorSupported { get => platform?.KeyingColorSupported ?? false; }
        public static void SetKeyingColor(byte red, byte green, byte blue)
              => platform?.SetKeyingColor(red, green, blue);

        public static bool    WindowOpacitySupported { get => platform?.WindowOpacitySupported ?? false; }
        public static void SetWindowOpacity(byte opacity)
              => platform?.SetWindowOpacity(opacity);




        public static bool WindowPlacementSupported { get => platform?.WindowPlacementSupported ?? false; }
        public static void MoveWindowRelative(int pixelX, int pixelY)
              => platform?.MoveWindowRelative(pixelX, pixelY);



        // fullscreen
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
                // so FrameVisibility done BEFORE full screen disabled no matter when it invoked in setter.
                // to make it better, simply, reset transparent state.
                if (!isFullScreen) Transparent = false;

            }//set
        }//




    }//class
}//namespace
