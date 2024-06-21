using System.Collections.Generic;

namespace EliTool.ExtensionSDK.Model;

public class ToolGroup
{
    public string Title
    {
        get; set;
    }
    public Media.Image HeaderImage
    {
        get; set;
    }
    public string Id
    {
        get; set;
    }
    public List<ToolInfo> ToolInfos
    {
        get; set;
    } = new();
}
