using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Models;
using Windows.Storage;

namespace EliTool.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    public MainViewModel()
    {
    }

    Root LoadedRoot = null;

    public Root GetControlInfos()
    {
        if (LoadedRoot == null)
        {
            var fservice = App.GetService<Core.Contracts.Services.IFileService>();

            LoadedRoot = fservice.Read<Root>(string.Join('\\', Process.GetCurrentProcess().MainModule.FileName.Split('\\')[..^1]) + "\\Assets\\Control\\Info", "ControlInfos.json");
        }
        return LoadedRoot;
    }
}
