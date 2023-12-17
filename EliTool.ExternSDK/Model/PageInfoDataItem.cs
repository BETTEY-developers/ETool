using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;

namespace EliTool.ExternSDK.Model;

public class Root
{
    public int Version
    {
        get; set;
    } = 0;

    public List<PageInfoGroup> ControlInfoGroups
    {
        get; set;
    } = new();
}

public class PageInfoGroup
{
    public string Title
    {
        get; set;
    }
    public Media.Image Image
    {
        get; set;
    }
    public string Id
    {
        get; set;
    }
    public List<PageInfoDataItem> ControlInfos
    {
        get; set;
    }
}

public class PageInfoDataItem
{
    public string Title
    {
        get; set;
    }
    public string Subtitle
    {
        get; set;
    }
    public Media.Image Image
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
    public override string ToString()
    {
        return Title;
    }
}
