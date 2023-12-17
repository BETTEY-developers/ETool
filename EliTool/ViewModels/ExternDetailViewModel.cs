using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
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
    Extern displayExtern = new Extern();

    [ObservableProperty]
    ObservableCollection<Page> pages = new ObservableCollection<Page>();

    public ObservableCollection<PageInfoDataItem> SubPage()
    {
        var rl = DisplayExtern.GetPageGroup().ControlInfos;

        return new(rl.SkipLast(int.Min(rl.Count-1, 8)));
    }
}
