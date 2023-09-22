using System.Collections.ObjectModel;
using System.Reflection;
using EliTool.Contracts.Services;
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

    public ObservableCollection<ControlInfoDataItem> ControlInfos { get; set; } = new ObservableCollection<ControlInfoDataItem>();

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        
        InitializeComponent();
    }

    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        List<ControlInfoDataItem> items = new List<ControlInfoDataItem>();
        foreach (var v in ViewModel.GetControlInfosAsync().ControlInfoGroups) foreach (var s in v.ControlInfos) ControlInfos.Add(s);
        
        base.OnNavigatedTo(e);
    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        Frame.Navigate(App.GetService<IPageService>().GetPageType((e.ClickedItem as ControlInfoDataItem).ClickPath));
    }
}
