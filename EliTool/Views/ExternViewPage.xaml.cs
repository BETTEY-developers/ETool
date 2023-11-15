using EliTool.BasePackage.Contracts.Services;
using EliTool.Contracts.Services;
using EliTool.ViewModels;
using EliTool.Views.ExternViews;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace EliTool.Views;

public sealed partial class ExternViewPage : Page
{
    public ExternViewViewModel ViewModel
    {
        get;
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
