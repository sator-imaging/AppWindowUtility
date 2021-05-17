namespace SatorImaging.AppWindowUtility
{
    public interface IPlatformDependent
    {
        bool GetAlwaysOnTop();
        void SetAlwaysOnTop(bool enable);

        bool GetTransparent();
        void SetTransparent(bool enable);

        bool GetFrameVisibility();
        void SetFrameVisibility(bool visible);

        bool GetClickThrough();
        void SetClickThrough(bool enable);

        bool GetAsWallpaper();
        void SetAsWallpaper(bool enable);



        void SetKeyingColor(byte red, byte green, byte blue);
        void SetWindowOpacity(byte opacity);

        void MoveWindowRelative(int pixelX, int pixelY);

    }//

}//namespace
