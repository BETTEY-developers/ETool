using EliTool.Models;
using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views.Extern;

public sealed partial class ExternDetailPage : Page
{
    public ExternDetailViewModel ViewModel
    {
        get;
    }

    public ExternDetailPage()
    {
        ViewModel = App.GetService<ExternDetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        ViewModel.DisplayExtern = (ExternInfo)e.Parameter;
    }
}
