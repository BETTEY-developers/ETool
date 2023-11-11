namespace EliTool.ExternSDK;

public interface IInfo
{
    public string Name
    {
        get;
    }

    public string DisplayName
    {
        get;
    }

    public string Description
    {
        get;
    }

    public string Version
    {
        get;
    }

    public string Author
    {
        get;
    }

    public string AuthorUrl
    {
        get;
    }

    public string IconPath
    {
        get;
    }
}
