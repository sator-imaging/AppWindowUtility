App Window Utility （日本語版マニュアル）
======================================

このライブラリは、Unity で作成されたアプリのウインドウスタイルをカスタマイズする為のものです。
このライブラリを使用することでアプリのウインドウを透過させたり、フレームを非表示にすること等が可能です。

![](https://github.com/sator-imaging/sator-imaging.github.io/blob/master/AppWindowUtility/images/Opacity.gif?raw=true)


- [ライセンス](#ライセンス)
- [機能](#機能)
    - [透過ウインドウ](#透過ウインドウ)
    - [ウインドウの不透明度](#ウインドウの不透明度)
    - [`WindowGrabber` コンポーネント](#windowgrabber-コンポーネント)
    - [フルスクリーンモード](#フルスクリーンモード)
    - [常に手前に表示（Always on Top）](#常に手前に表示always-on-top)
    - [クリックスルーモード](#クリックスルーモード)
    - [ウインドウフレームの表示](#ウインドウフレームの表示)
    - [カラーキーイングウインドウ](#カラーキーイングウインドウ)
    - [加算合成モード](#加算合成モード)
- [注意点](#注意点)
    - [プレイヤー設定（Player Settings）](#プレイヤー設定player-settings)
    - [High-Definition Render Pipeline (HDRP) と同時に使用する場合](#high-definition-render-pipeline-hdrp-と同時に使用する場合)
    - [透過ウインドウとウィンドウフレームの表示](#透過ウインドウとウィンドウフレームの表示)
- [制作・著作](#制作著作)



# ライセンス

著作権とライセンスの表示をアプリに含める必要があります。詳細は同梱の LICENSE ファイルを参照してください。



# 機能

以下の機能は Unity 2020.3 LTS と HDRP 10.3.2 を Windows 10 64-bit 上で実行して動作確認しています。


#### 事前準備

以下のサンプルを動作させるには、`using SatorImaging.AppWindowUtility;` を追加して名前空間を参照しておく必要があります。



## 透過ウインドウ

`bool AppWindowUtility.Transparent { get; set; }`

アプリを透過ウインドウに設定します。ウインドウの後ろにある別のウインドウが透けて見えるようになります。

透明度は Unity のレンダリング結果に依存しているので、カメラの `Background` を **Solid Color** に設定してアルファをゼロにする必要があります。

> 注： High-Definition Render Pipeline (HDRP) やその他の Scriptable Render Pipeline (SRP) ベースのレンダラーを使っている場合、Color Frame Buffer をデフォルトの RGB 10bit から RGB 16bit（8bit があればそちらでも可）に設定する必要があります。

<img src="https://dl.dropbox.com/s/sntvylmfgrrfw9w/Transparent.gif?dl=1" />



## ウインドウの不透明度

`AppWindowUtility.SetWindowOpacity(byte opacity)`

ウインドウ全体の不透明度を設定します。透過ウインドウと組み合わせて使うこともできます。

<img src="https://dl.dropbox.com/s/clu72kycyq2isvn/Opacity.gif?dl=1" />



## `WindowGrabber` コンポーネント

透過ウインドウが有効になっているとタイトルバーが消えてしまうので、ウインドウを移動することが出来なくなってしまいます。

`WindowGrabber` を空の `GameObject` に追加することで、アプリのウインドウのどこかをドラッグすれば移動できるようになります。

<img src="https://dl.dropbox.com/s/oxcnjfdkdshogf0/MoveWindow_WindowGrabber.png?dl=1" />



※ `WindowGrabber` が有効な間も uGUI を使うことが出来ます。  
<img src="https://dl.dropbox.com/s/etmsd3zb0muhltd/MoveWindow.gif?dl=1" />



## フルスクリーンモード

`bool AppWindowUtility.FullScreen { get; set; }`

ウインドウをフルスクリーンに設定します。

> 注： Unity 標準の `UnityEngine.Screen.SetResolution(width, height, isFullScreen)` ではなくコチラを使った方が App Window Utility との相性が良いです。




## 常に手前に表示（Always on Top）

`bool AppWindowUtility.AlwaysOnTop { get; set; }`

ウインドウがフォーカスを失っても、常に最前面に表示されるようになります。

<img src="https://dl.dropbox.com/s/sip8uw1d91osdii/AlwaysOnTop.gif?dl=1" />



## クリックスルーモード

`bool AppWindowUtility.ClickThrough { get; set; }`

アプリケーションがマウスのクリックを受け付けなくなります。

> 注： マウスを使わずにこの機能をオフにする方法を実装する必要があります。実装しなかった場合、二度とアプリに触れなくなります。

<img src="https://dl.dropbox.com/s/o27h63u7g5tg9mm/ClickThru_B.gif?dl=1" />



## ウインドウフレームの表示

`bool AppWindowUtility.FrameVisibility { get; set; }`

ウインドウのフレームを表示・非表示に設定します。

※ 透過ウインドウに設定すると、同時にフレームの表示もオフに設定されます。



## カラーキーイングウインドウ

`AppWindowUtility.SetKeyingColor(byte red, byte green, byte blue)`

アプリのウインドウ全体に対して、特定の色を透過させる機能を有効にします。
かなり特殊なので、`AppWindowUtility.Transparent` を使った方が良いです。



## 加算合成モード

Windows の場合、`AppWindowUtility.SetKeyingColor(0, 0, 0)` を設定した後に `AppWindowUtility.Transparent = true` を設定すると、透過ウインドウが加算合成モードになります。

※注※ これは Windows のバグの可能性があります。

<img src="https://dl.dropbox.com/s/nt5mmncsz6cfvh6/AdditiveComposition.gif?dl=1" />



# 注意点


## プレイヤー設定（Player Settings）

正しく動作させるためには、`Use DXGI Flip Model Swapchain for D3D11` をオフに設定する必要があります。

※ App Window Utility が期待通りに動作しない場合、以下のプレイヤー設定を参考にしてください。

<img src="https://dl.dropbox.com/s/72ii6o5dj7yxqtt/Notes_PlayerSettings.png?dl=1" />



## High-Definition Render Pipeline (HDRP) と同時に使用する場合

`Color Buffer Format` は必ず RGB 16bit（またはホスト OS が対応しているフォーマット）に設定する必要があります。
Scriptable Render Pipeline (SRP) ベースのレンダラーは同様の設定が必要になります。

<img src="https://dl.dropbox.com/s/d1qieutmog4npbw/Notes_HDRP.png?dl=1" />



## 透過ウインドウとウィンドウフレームの表示

`Transparent = true` を設定した後に `FrameVisibility = true` を設定すると、アプリの背景が非表示になった状態でウインドウフレームが表示されます。結果としてウインドウ全体がフレームの色で塗りつぶされることになります。

<img src="https://dl.dropbox.com/s/sr55jdguin250ic/Notes_TransparentThenShowFrame.gif?dl=1" />



# 制作・著作

Copyright &copy; 2022 Sator Imaging, all rights reserved.

VRM and UniVRM are licensed under the MIT License.
