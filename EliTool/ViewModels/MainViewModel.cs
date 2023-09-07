using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.DataModels;

namespace EliTool.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    public MainViewModel()
    {
    }

    Root LoadedRoot = null;

    public async Task<Root> GetControlInfosAsync()
    {
        return await Task.Run(() =>
        {
            if (LoadedRoot == null)
            {
                var fservice = App.GetService<Core.Services.FileService>();
                LoadedRoot = fservice.Read<Root>("ms-appx:///Assets/Control/Info", "ControlInfos.json");
            }
            return LoadedRoot;
        });
        
        
    }
}
