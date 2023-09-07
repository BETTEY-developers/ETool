namespace EliTool.DataModels;

public class Root
{
    public int Version { get; set; }
    public List<ControlInfoGroup> ControlInfoGroups { get; set; }
}

public class ControlInfoGroup
{
    public string Title { get; set; }
    public string ImagePath { get; set; }
    public List<ControlInfoDataItem> ControlInfos { get; set; }
}

public class ControlInfoDataItem
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string ImagePath { get; set; }
    public string ClickPath { get; set; }
}
