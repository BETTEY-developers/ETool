using System;
using Microsoft.UI.Xaml.Controls;

namespace EliTool.ExtensionSDK.Model;

public class ToolInfo
{
    public string Title
    {
        get; set;
    }
    public string Subtitle
    {
        get; set;
    }
    public Media.Image HeaderImage
    {
        get; set;
    }
    public Type PageViewModel
    {
        get; set;
    }
    public Type Page
    {
        get; set;
    }
    public ToolLinkMode LinkMode
    {
        get; set;
    }
    public override string ToString()
    {
        return Title;
    }
}
