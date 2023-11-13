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
        displayExtern.GetPages().Values.ToList().ForEach(pages.Add);
    }

    [ObservableProperty]
    Extern displayExtern = new Extern();

    [ObservableProperty]
    ObservableCollection<Page> pages = new ObservableCollection<Page>();
}
