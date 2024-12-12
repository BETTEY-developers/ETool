using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Contracts.Services;
using EliTool.Helpers;
using EliTool.Model;
using EliTool.Models;
using EliTool.Services;
using Windows.Storage;

namespace EliTool.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    public MainViewModel()
    {
    }

    private static Root LoadedRoot = null;

    public async Task<Root> GetExtensionElements()
    {
        //LoadedRoot = new Root();
        //LoadedRoot.Version = 1;
        //foreach(var externitem in App.GetService<IExtensionService>().Extensions??new())
        //{
        //    LoadedRoot.ToolInfoGroups.AddRange(externitem.EntryInstance.GetExtensionToolGroups());
        //    LoadedRoot.DocumentGroups.AddRange(externitem.EntryInstance.GetExtensionDocumentGroups());
        //}
        //return LoadedRoot;
        if (LoadedRoot == null)
        {
            var resource = "ControlInfos.json".GetLocalizedRaw("Files/Assets/Control/Info");


            StorageFile storageFile = await StorageFile.GetFileFromPathAsync(resource.ValueAsString);

            LoadedRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(await FileIO.ReadTextAsync(storageFile));
        }
        return LoadedRoot;
    }
}
