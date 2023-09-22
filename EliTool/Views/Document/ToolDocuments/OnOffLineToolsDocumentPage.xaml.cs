using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views.Document;

public sealed partial class OnOffLineToolsDocumentPage : Page
{
    public OnOffLineToolsDocumentViewModel ViewModel
    {
        get;
    }

    public OnOffLineToolsDocumentPage()
    {
        ViewModel = App.GetService<OnOffLineToolsDocumentViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        base.OnNavigatedTo(e);
    }
}
