# <center><strong>DirectUI</strong></center>
* DirectUI 是什么
百度百科：DirectUI意为直接在父窗口上绘图(Paint on parent dc directly)。即子窗口不以窗口句柄的形式创建(windowless)，只是逻辑上的窗口，绘制在父窗口之上。微软的“DirectUI”技术广泛的应用于Windows XP,Vista,Windows 7，如浏览器左侧的TaskPanel,控制面板导航界面，Media Player播放器，即时通讯工具MSN Messager等。
DirectUI好处在于可以很方便的构建高效，绚丽的，非常易于扩展的界面。国外如微软，国内如腾讯，百度等公司的客户端产品多采用这种方式来组织界面，从而很好的将界面和逻辑分离，同时易于实现各种超炫的界面效果如换色，换肤，透明等。 DirectUI 旨在满足客户端界面快速开发的需要，同时融入业界前沿的皮肤技术，为用户创建更加高效，专业的界面。
* 我的DirectUI可以做什么？
当前的DirectUI库是基于directUI的基本思想，再组合上winform的原生控件构建方式然后封装Direct2D为GDI+的调用方式。只实现了基础控件DUIControl(控件类)、DUIEditableControl(可编辑组件)、DUIScaleableControl(可缩放组件)、DUIScrollableControl(可滚动组件)这四大类组件的封装。只要你有winform和GDI+的基础就可以轻易的驾驭这个库
* 简单的构建代码如下：
```C#
DUINativeControl dUINativeControl = new DUINativeControl() { Dock = DockStyle.Fill };
DUIControl dUIControl = new DUIControl();
public Form1()
{
    InitializeComponent();
    this.Controls.Add(dUINativeControl);
    dUINativeControl.DUIControls.Add(dUIControl);
    dUIControl.Paint += (s, e) =>
    {
        e.Graphics.DrawString("随便写点", dUIControl.Font, DUIBrushes.Red, PointF.Empty);
    };
}

```
简直和写Winform一毛一样有木有，就连调用Direct2d也像操作GDI+一样是不是？
# 演示
* DUIControl

![avatar](https://github.com/ft9788501/DirectUI/blob/master/SampleGif/DUIControl.gif?raw=true)

* DUIEditableControl

![avatar](https://github.com/ft9788501/DirectUI/blob/master/SampleGif/DUIEditableControl.gif?raw=true)

* DUIScaleableControl

![avatar](https://github.com/ft9788501/DirectUI/blob/master/SampleGif/DUIScaleableControl.gif?raw=true)

* DUIScrollableControl

![avatar](https://github.com/ft9788501/DirectUI/blob/master/SampleGif/DUIScrollableControl.gif?raw=true)

* Cubes

![avatar](https://github.com/ft9788501/DirectUI/blob/master/SampleGif/Cubes.gif?raw=true)

