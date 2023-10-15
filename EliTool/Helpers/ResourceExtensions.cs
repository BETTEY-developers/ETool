using System.Diagnostics;
using Microsoft.Windows.ApplicationModel.Resources;

namespace EliTool.Helpers;

public static class ResourceExtensions
{
    private static readonly ResourceLoader _resourceLoader = new();
    private static Dictionary<string, ResourceCandidate> _resources = new();

    static ResourceExtensions()
    {
        var map = new ResourceManager().MainResourceMap;
        for (uint i = 0; i < map.ResourceCount; i++)
        {
            try
            {
                _resources.Add(map.GetValueByIndex(i).Key, map.GetValueByIndex(i).Value);
            }
            catch { Debug.Print(i.ToString()); }
        }
    }

    public static string GetLocalized(this string resourceKey,string culture = "Resources") => /*_resourceLoader.GetString(resourceKey)*/ _resources[culture + "/" + resourceKey].ValueAsString;
}
