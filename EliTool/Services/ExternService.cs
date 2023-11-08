using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EliTool.Contracts.Services;
using EliTool.ExternSDK;
using EliTool.Models;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace EliTool.Services;

internal class ExternService : IExternService
{
    StorageFolder _ApplicationExternFolder = null;
    StorageFolder _ApplicationExternUnpackageFolder = null;
    StorageFile _ExternManifest = null;

    public List<ExternInfo> Externs
    {
        get; set;
    }

    public ExternService()
    {
        
    }

    public async Task Load()
    {
        await GetExternFolder();
        await GetExternManifest();
        await GetExterns();
        await Unpackages();

        (Externs??new List<ExternInfo>()).ForEach(x =>
        {
            x.EntryInstance = (IMain)Assembly.LoadFrom(_ApplicationExternUnpackageFolder.Path + "\\" + x.Name + "\\" + x.Name + ".dll").CreateInstance(x.Name + ".Main");
            x.Manifest = new(x.EntryInstance);
            x.EntryInstance.Install();
        });
    }

    public async Task Unload()
    {
        Externs.ForEach(x =>
        {
            x.EntryInstance.Uninstall();
        });
    }

    private async Task GetExternFolder()
    {
        try
        {
            _ApplicationExternFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Externs");
        }
        catch
        {
            _ApplicationExternFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Externs");
        }
    }

    private async Task GetExternManifest()
    {
        try
        {
            _ExternManifest = await _ApplicationExternFolder.CreateFileAsync("ExternManifest.json");
        }
        catch
        {
            _ExternManifest = await _ApplicationExternFolder.GetFileAsync("ExternManifest.json");
        }
    }

    private async Task Unpackages()
    {
        try
        {
            _ApplicationExternUnpackageFolder = await _ApplicationExternFolder.CreateFolderAsync("Unpackage");
        }
        catch
        {
            _ApplicationExternUnpackageFolder = await _ApplicationExternFolder.GetFolderAsync("Unpackage");
        }

        foreach (var externitem in Externs??new())
        {
            try
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(_ApplicationExternFolder.Path + "\\" + externitem.Name + ".ete", _ApplicationExternFolder.Path + "\\" + "Unpackage\\" + externitem.Name + "\\");
            }
            catch { }
        }
    }

    private async Task GetExterns()
    {
        try
        {
            Externs = JsonSerializer.Deserialize<List<ExternInfo>>(await FileIO.ReadTextAsync(_ExternManifest));
        }
        catch { }
    }

    public List<Page> GetExternPages() => throw new NotImplementedException();
    public List<SettingCollection> GetSettingItems() => throw new NotImplementedException();
    public Dictionary<string, string> GetInfoManifest() => throw new NotImplementedException();
}
