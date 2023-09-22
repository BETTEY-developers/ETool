using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views.Document;

public sealed partial class OfflineToolsDocumentPage : Page
{
    public OfflineToolsDocumentViewModel ViewModel
    {
        get;
    }

    public OfflineToolsDocumentPage()
    {
        ViewModel = App.GetService<OfflineToolsDocumentViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        base.OnNavigatedTo(e);
    }
}
