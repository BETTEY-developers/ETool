using EliTool.BasePackage.Contracts.Services;
using EliTool.Contracts.Services;
using EliTool.ViewModels;
using EliTool.Views.ExternViews;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views;

public sealed partial class ExternViewPage : Page
{
    public ExternViewViewModel ViewModel
    {
        get;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        base.OnNavigatedTo(e);
    }

    public ExternViewPage()
    {
        ViewModel = App.GetService<ExternViewViewModel>();
        InitializeComponent();
    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        App.GetService<INavigationService>().Frame.Navigate(typeof(ExternDetailPage), e.ClickedItem, new DrillInNavigationTransitionInfo());
    }
}
