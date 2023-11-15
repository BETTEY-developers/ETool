using System;
using System.Collections.Generic;

namespace EliTool.ExternSDK.Model;

public class Root
{
    public int Version
    {
        get; set;
    }
    public List<PageInfoGroup> ControlInfoGroups
    {
        get; set;
    }
}

public class PageInfoGroup
{
    public string Title
    {
        get; set;
    }
    public string ImagePath
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
    public string ImagePath
    {
        get; set;
    }
    public Type ClickType
    {
        get; set;
    }
    public Type PageType
    {
        get; set;
    }
    public override string ToString()
    {
        return Title;
    }
}
