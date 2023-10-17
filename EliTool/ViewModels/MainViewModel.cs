using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Helpers;
using EliTool.Models;
using Windows.Storage;

namespace EliTool.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    public MainViewModel()
    {
    }

    private static Root LoadedRoot = null;

    public async Task<Root> GetControlInfos()
    {
        if (LoadedRoot == null)
        {
            var resource = "ControlInfos.json".GetLocalizedRaw("Files/Assets/Control/Info");

            StorageFile storageFile = await StorageFile.GetFileFromPathAsync(resource.ValueAsString);

            LoadedRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(await FileIO.ReadTextAsync(storageFile)) ;
        }
        return LoadedRoot;
    }
}
