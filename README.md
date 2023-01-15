App Window Utility
==================

This utility is for Unity to configure application window style.
With this utility, you can make your application window transparent, frameless and more.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/Opacity.gif?raw=true)




Copyright
=========

Copyright &copy; 2022-2023 Sator Imaging, all rights reserved.



License
=======

<p>
<details>
<summary>The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.</summary>

```text
MIT License

Copyright (c) 2022-2023 Sator Imaging

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

</details>
</p>




Features
========

Features are tested with Unity 2020.3 LTS and HDRP 10.3.2 on Windows 10 64-bit.


#### Prerequisites

`using SatorImaging.AppWindowUtility;` is required to make the following examples work.



Sample
======

Here is a sample code. See the following sections for more options.


```csharp
using UnityEngine;
using SatorImaging.AppWindowUtility;

public class MyTest : MonoBehaviour
{
    void Start()
    {
        AppWindowUtility.Transparent = true;
    }
}
```



## Transparent Window

`bool AppWindowUtility.Transparent { get; set; }`

This will make application window transparent. In other words, you can see behind through application window.

Transparency is based on Unity's rendering result. So that you need to set camera background to **Solid Color** with alpha is set to zero.

> NOTE: If you use High-Definition Render Pipeline (HDRP) or something based on Scriptable Render Pipeline (SRP), Color Frame Buffer setting needs to be set to RGB 16bit (or 8bit if available). Unity's default is RGB 10bit that Operating System doesn't support.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/Transparent.gif?raw=true)



## Window Opacity

`AppWindowUtility.SetWindowOpacity(byte opacity)`

This will set overall window opacity. It works with see-thru window.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/Opacity.gif?raw=true)



## `WindowGrabber` Component

With Transparent enabled, window has no title bar so that you need a way to move window.

`WindowGrabber` adds an ability to move window by dragging any area of application window.
To add this feature, create empty GameObject and attach `WindowGrabber` component.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/MoveWindow_WindowGrabber.png?raw=true)

> You can still use uGUI controls if WindowGrabber is used.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/MoveWindow.gif?raw=true)



## Full Screen Mode

`bool AppWindowUtility.FullScreen { get; set; }`

This will make window full screen or not.

> NOTE: Use this instead of Unity's built-in `UnityEngine.Screen.SetResolution(width, height, isFullScreen)` and `UnityEngine.Screen.fullScreen` to work better with App Window Utility.




## Always on Top

`bool AppWindowUtility.AlwaysOnTop { get; set; }`

This will make application window stay on top of other windows while another application has focus.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/AlwaysOnTop.gif?raw=true)



## Click-Thru Mode

`bool AppWindowUtility.ClickThrough { get; set; }`

This make application window non-clickable.

> NOTE: You must implement a way to disable this feature not using mouse click. If not implemented, you cannot touch your application anymore.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/ClickThru_B.gif?raw=true)



## Window Frame Visibility

`bool AppWindowUtility.FrameVisibility { get; set; }`

This will set window frame visibile or invisible.

> NOTE: When enable transparent window, window frame will be automatically hidden.



## Color-Keying Window

`AppWindowUtility.SetKeyingColor(byte red, byte green, byte blue)`

This will **Keying** specified color from application window.
It's strongly recommended to use `AppWindowUtility.Transparent` instead of this.



## Additive Composition Mode

On Windows, applying `AppWindowUtility.SetKeyingColor(0, 0, 0)` and then `AppWindowUtility.Transparent = true` will change window composition mode to "Additive".

> NOTE: This behaviour could be a bug of Windows.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/AdditiveComposition.gif?raw=true)



Important Notes
===============


## Player Settings

**Use DXGI Flip Model Swapchain for D3D11** must be turned off to work correctly.

> If App Window Utility doesn't work as you expected, see Player Settings below for reference.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/Notes_PlayerSettings.png?raw=true)



## Using with High-Definition Render Pipeline (HDRP)

**Color Buffer Format** must be RGB 16bit (or something supported on Host OS) to work correctly.
Other renderer based on Scriptable Render Pipeline (SRP) needs setting like this too.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/Notes_HDRP.png?raw=true)



## Transparent and Frame Visibility

If you apply `Transparent = true` and then `FrameVisibility = true`, it will remove background filling from application window. Result is below, as you can see, background is filled with window frame color.

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/Notes_TransparentThenShowFrame.gif?raw=true)
