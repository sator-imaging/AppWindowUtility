namespace SatorImaging.AppWindowUtility
{
    public interface IPlatformDependent
    {
        bool AlwaysOnTopSupported { get; }
        bool GetAlwaysOnTop();
        void SetAlwaysOnTop(bool enable);

        bool TransparentSupported { get; }
        bool GetTransparent();
        void SetTransparent(bool enable);

        bool FrameVisibilitySupported { get; }
        bool GetFrameVisibility();
        void SetFrameVisibility(bool visible);

        bool ClickThroughSupported { get; }
        bool GetClickThrough();
        void SetClickThrough(bool enable);

        bool AsWallpaperSupported { get; }
        bool GetAsWallpaper();
        void SetAsWallpaper(bool enable);



        bool KeyingColorSupported { get; }
        void SetKeyingColor(byte red, byte green, byte blue);

        bool WindowOpacitySupported { get; }
        void SetWindowOpacity(byte opacity);


        bool WindowPlacementSupported { get; }
        void MoveWindowRelative(int pixelX, int pixelY);

    }//

}//namespace
