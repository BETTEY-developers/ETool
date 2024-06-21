using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Helpers;
using EliTool.Models;
using Microsoft.UI.Xaml.Controls;

namespace EliTool.ViewModels;

public partial class ExternDetailViewModel : ObservableRecipient
{
    public ExternDetailViewModel()
    {
    }

    public void Load()
    {
        
    }

    [ObservableProperty]
    Extension displayExtern = new Extension();

    [ObservableProperty]
    ObservableCollection<Page> pages = new ObservableCollection<Page>();

    public ObservableCollection<ToolInfo> SubPage()
    {
        List<ToolInfo> rl = 
                displayExtern
                .EntryInstance
                .GetExtensionToolGroups()
                .MergeItem<ToolGroup, ToolGroup>((o, g) =>
                {
                    foreach (var item in g.ToolInfos)
                        o.ToolInfos.Add(item);
                })
                .ToolInfos;

        return new(rl.SkipLast(int.Min(rl.Count-1, 8)));
    }
}
