using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using EliTool.BasePackage.Contracts.Services;
using EliTool.Contracts.Services;
using EliTool.ExtensionSDK;
using EliTool.Helpers;
using EliTool.Models;

using Microsoft.Win32;

using Windows.Foundation.Metadata;
using Windows.Storage;

namespace EliTool.Services;

internal class ExtensionService : IExtensionService
{
    public const string ExtensionDomainName = "ExtensionDomain";

    public StorageFolder ApplicationExtensionFolder { set; get; } = null;
    public StorageFolder ApplicationExtensionUnpackageFolder { set; get; } = null;
    public StorageFile ExtensionManifest { set; get; } = null;
    
    public AppDomain ExtensionDomain
    {
        get; set;
    }

    public List<Extension> Extensions
    {
        get; set;
    }

    private static ExtensionService Instance
    {
        get; set;
    }

    private static bool _inited
    {
        get; set;
    }

    public ExtensionService()
    {
        if (_inited)
        {
            Extensions = Instance.Extensions;
            ExtensionManifest = Instance.ExtensionManifest;
            ApplicationExtensionFolder = Instance.ApplicationExtensionFolder;
            ApplicationExtensionUnpackageFolder = Instance.ApplicationExtensionUnpackageFolder;
        }
    }

    public async Task<bool> Load()
    {
        await GetExtensionFolder();
        Debug.WriteLine(ApplicationExtensionFolder.Path);
        await GetExtensionManifest();
        await GetExtensions();
        await Unpackages();
        Extensions ??= new();
        Extensions.ForEach(x =>
        {
            x.EntryAssembly = Assembly.LoadFile(ApplicationExtensionUnpackageFolder.Path + "\\" + x.Name + "\\" + x.Name + ".dll");
            x.EntryInstance = (IMain)x.EntryAssembly.CreateInstance(x.Name + ".Main", false, BindingFlags.Default, null, [App.Current], null, null);
            x.Manifest = new(x.EntryInstance.GetInfo());
            x.EntryInstance.Install();
        });

#if DEBUG
        Debug.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);

        string depath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "DebugExtension.enable");
        if (Path.Exists(depath))
        {
            Extension x = new();

            string[] deContent = File.ReadAllLines(depath);
            x.EntryAssembly = Assembly.LoadFile(deContent[0]);
            x.EntryInstance = (IMain)x.EntryAssembly.CreateInstance(deContent[1] + ".Main", false, BindingFlags.Default, null, [App.Current], null, null);
            x.Manifest = new(x.EntryInstance.GetInfo());
            x.EntryInstance.Install();
            Extensions.Add(x);
        }
#endif
        

        await RegisterPages();

        Instance = this;

        _inited = true;

        return true;
    }

    public async Task Unload()
    {
        Extensions.ForEach(x =>
        {
            x.EntryInstance.Uninstall();
        });
        Instance = this;
    }

    private async Task GetExtensionFolder()
    {
        try
        {
            ApplicationExtensionFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Extensions");
        }
        catch
        {
            ApplicationExtensionFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Extensions");
        }
    }

    private async Task GetExtensionManifest()
    {
        try
        {
            ExtensionManifest = await ApplicationExtensionFolder.CreateFileAsync("ExtensionManifest.json");
        }
        catch
        {
            ExtensionManifest = await ApplicationExtensionFolder.GetFileAsync("ExtensionManifest.json");
        }
    }

    private async Task Unpackages()
    {
        try
        {
            ApplicationExtensionUnpackageFolder = await ApplicationExtensionFolder.CreateFolderAsync("Unpackage");
        }
        catch
        {
            ApplicationExtensionUnpackageFolder = await ApplicationExtensionFolder.GetFolderAsync("Unpackage");
        }

        foreach (var externitem in Extensions??new())
        {
            try
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(ApplicationExtensionFolder.Path + "\\" + externitem.Name + ".ete", ApplicationExtensionFolder.Path + "\\" + "Unpackage\\" + externitem.Name + "\\");
            }
            catch { }
        }
    }



    private async Task GetExtensions()
    {
        try
        {
            string content = await FileIO.ReadTextAsync(ExtensionManifest);
            Extensions = JsonSerializer.Deserialize<List<Extension>>(content);
        }
        catch { }
    }

    private async Task RegisterPages()
    {
        var ser = App.GetService<IPageService>();
        foreach (var externitem in Extensions?? new())
        {
            externitem
                .EntryInstance
                .GetExtensionToolGroups()
                .MergeItem<ToolGroup, ToolGroup>((o, g) =>
                {
                    foreach (var item in g.ToolInfos)
                        o.ToolInfos.Add(item);
                })
                .ToolInfos
                .ForEach(x =>
                {
                    ser.AddDependence(x.PageViewModel, x.Page);
                });
        }
    }

    public Extension GetExtension(string name)
    {
        return Extensions.FirstOrDefault(x =>
        {
            return x.Name == name;
        }, new Extension());
    }
}
