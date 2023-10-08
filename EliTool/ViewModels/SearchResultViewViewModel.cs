using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Views;

namespace EliTool.ViewModels;

public partial class SearchResultViewViewModel : ObservableRecipient
{
    public SearchResultViewViewModel()
    {
    }

    [ObservableProperty]
    ObservableCollection<SearchGroup> groups = new ObservableCollection<SearchGroup>();
}
