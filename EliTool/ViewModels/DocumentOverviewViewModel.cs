using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EliTool.Contracts.Services;
using EliTool.Views;

namespace EliTool.ViewModels;

public partial class DocumentOverviewViewModel : ObservableRecipient
{
    public DocumentOverviewViewModel()
    {
    }

    [RelayCommand]
    public void NavigationTo(string type)
    {
        App.GetService<INavigationService>().NavigateTo(type);
    }
}
