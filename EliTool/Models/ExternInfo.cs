using EliTool.ExternSDK;
using Newtonsoft.Json;

namespace EliTool.Models;

public class ExternManifestInfo : IInfo
{
    public Dictionary<string, string> InfoDict { get; set; } = new();

    public ExternManifestInfo(IInfo info)
    {
        InfoDict[nameof(Name)] = info.Name;
        InfoDict[nameof(Description)] = info.Description;
        InfoDict[nameof(Version)] = info.Version;
        InfoDict[nameof(Author)] = info.Author;
        InfoDict[nameof(AuthorUrl)] = info.AuthorUrl;
        InfoDict[nameof(IconPath)] = info.IconPath;
    }

    public string Name => InfoDict[nameof(Name)];

    public string Description => InfoDict[nameof(Description)];

    public string Version => InfoDict[nameof(Version)];

    public string Author => InfoDict[nameof(Author)];

    public string AuthorUrl => InfoDict[nameof(AuthorUrl)];

    public string IconPath => InfoDict[nameof(IconPath)];
}

public class ExternInfo
{

    public string Name
    {
        get; set;
    }

    public IMain EntryInstance
    {
        get; set;
    }

    public ExternManifestInfo Manifest
    {
        get; set;
    }
}