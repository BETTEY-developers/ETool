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
    public Uri HeaderImage
    {
        get; set;
    }
    public string PageViewModel
    {
        get; set;
    }
    public string Page
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
