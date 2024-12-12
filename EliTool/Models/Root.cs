using EliTool.ExtensionSDK.Model.Document;
using EliTool.ExtensionSDK.Model.Tool;
using Newtonsoft.Json;

namespace EliTool.Model;

public class Root
{
    public int Version
    {
        get; set;
    } = 0;

    public List<ToolGroup> ToolInfoGroups
    {
        get; set;
    } = new();

    public List<DocumentGroup> DocumentGroups
    {
        get; set;
    } = new();
}
