using System.Collections.ObjectModel;
using System.Reflection;
using EliTool.BasePackage.Contracts.Services;
using EliTool.Contracts.Services;
using EliTool.Helpers;
using EliTool.Models;
using EliTool.Services;
using EliTool.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public ObservableCollection<PageInfoDataItem> ControlInfos { get; set; } = new ObservableCollection<PageInfoDataItem>();

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        
        InitializeComponent();
    }

    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        List<PageInfoDataItem> items = new List<PageInfoDataItem>();
        foreach (var v in ViewModel.GetControlInfos().ControlInfoGroups) foreach (var s in v.ControlInfos) ControlInfos.Add(s);
        
        base.OnNavigatedTo(e);
    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        Frame.Navigate((e.ClickedItem as PageInfoDataItem).PageType);
    }
}
