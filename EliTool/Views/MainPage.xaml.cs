using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Metadata;
using EliTool.BasePackage.Contracts.Services;
using EliTool.Contracts.Services;
using EliTool.Helpers;
using EliTool.Models;
using EliTool.Services;
using EliTool.ViewModels;
using EliTool.Views.ControlPage.DeveloperTools;
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

    public ObservableCollection<ToolInfo> ControlInfos { get; set; } = new ObservableCollection<ToolInfo>();

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        
        InitializeComponent();
    }

    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        foreach (var v in ViewModel.GetExtensionElements().ToolInfoGroups) foreach (var s in v.ToolInfos) ControlInfos.Add(s);
        
        base.OnNavigatedTo(e);
    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        try
        {
            
        }
        catch { }

        lock ((e.ClickedItem as ToolInfo).PageViewModel)
        {
        }
    }
}
