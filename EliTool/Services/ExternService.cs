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
    public const string ExternDomainName = "ExternDomain";

    public StorageFolder ApplicationExternFolder { set; get; } = null;
    public StorageFolder ApplicationExternUnpackageFolder { set; get; } = null;
    public StorageFile ExternManifest { set; get; } = null;
    
    public AppDomain ExternDomain
    {
        get; set;
    }

    public List<Extern> Externs
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

        (Externs??new List<Extern>()).ForEach(x =>
        {
            x.EntryAssembly = Assembly.LoadFrom(ApplicationExternUnpackageFolder.Path + "\\" + x.Name + "\\" + x.Name + ".dll");
            x.EntryInstance = (IMain)x.EntryAssembly.CreateInstance(x.Name + ".Main");
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
            ApplicationExternFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Externs");
        }
        catch
        {
            ApplicationExternFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Externs");
        }
    }

    private async Task GetExternManifest()
    {
        try
        {
            ExternManifest = await ApplicationExternFolder.CreateFileAsync("ExternManifest.json");
        }
        catch
        {
            ExternManifest = await ApplicationExternFolder.GetFileAsync("ExternManifest.json");
        }
    }

    private async Task Unpackages()
    {
        try
        {
            ApplicationExternUnpackageFolder = await ApplicationExternFolder.CreateFolderAsync("Unpackage");
        }
        catch
        {
            ApplicationExternUnpackageFolder = await ApplicationExternFolder.GetFolderAsync("Unpackage");
        }

        foreach (var externitem in Externs??new())
        {
            try
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(ApplicationExternFolder.Path + "\\" + externitem.Name + ".ete", ApplicationExternFolder.Path + "\\" + "Unpackage\\" + externitem.Name + "\\");
            }
            catch { }
        }
    }

    private async Task GetExterns()
    {
        try
        {
            Externs = JsonSerializer.Deserialize<List<Extern>>(await FileIO.ReadTextAsync(ExternManifest));
        }
        catch { }
    }

    public Extern GetExtern(string name)
    {
        return Externs.FirstOrDefault(x =>
        {
            return x.Name == name;
        }, new Extern());
    }
}
