using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views.Document;

public sealed partial class OnlineToolsDocumentPage : Page
{
    public OnlineToolsDocumentViewModel ViewModel
    {
        get;
    }

    public OnlineToolsDocumentPage()
    {
        ViewModel = App.GetService<OnlineToolsDocumentViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        base.OnNavigatedTo(e);
    }
}
