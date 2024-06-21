using EliTool.ExtensionSDK.Model.Document;
using EliTool.ExtensionSDK.Model.Tool;

namespace EliTool.Model;

public class Root
{
    public int Version
    {
        get; set;
    } = 0;

    public ToolGroupCollection ToolInfoGroups
    {
        get; set;
    } = new();

    public DocumentGroupCollection DocumentGroups
    {
        get; set;
    } = new();
}
