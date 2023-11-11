using System.Reflection;
using System.Xml.Linq;
using EliTool.Contracts.Services;
using EliTool.ExternSDK;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Media.Core;

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
        InfoDict[nameof(DisplayName)] = info.DisplayName;
        Self = this;
    }

    public string Name => InfoDict[nameof(Name)];

    public string Description => InfoDict[nameof(Description)];

    public string Version => InfoDict[nameof(Version)];

    public string Author => InfoDict[nameof(Author)];

    public string AuthorUrl => InfoDict[nameof(AuthorUrl)];

    public string IconPath => InfoDict[nameof(IconPath)];

    public string DisplayName => InfoDict[nameof(DisplayName)];

    public ExternManifestInfo Self
    {
        get; set;
    }
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

    public Assembly EntryAssembly
    {
        get; set; 
    }

    public ExternManifestInfo Manifest
    {
        get; set;
    }
}