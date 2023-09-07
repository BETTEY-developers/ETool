using System.Collections.ObjectModel;
using System.Reflection;
using EliTool.DataModels;
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

    public ObservableCollection<ControlInfoDataItem> ControlInfos { get; set; }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        List<ControlInfoDataItem> items = new List<ControlInfoDataItem>();
        foreach (var v in (await ViewModel.GetControlInfosAsync()).ControlInfoGroups)
            foreach (var s in v.ControlInfos) ControlInfos.Add(s);
        
        base.OnNavigatedTo(e);
    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        (WindowEx.Current.Content as Frame).Navigate(Assembly.GetExecutingAssembly().GetType((e.ClickedItem as ControlInfoDataItem).ClickPath));
    }
}
