using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace EliTool.Views;

public sealed partial class DocumentOverviewPage : Page
{
    public DocumentOverviewViewModel ViewModel
    {
        get;
    }

    public DocumentOverviewPage()
    {
        ViewModel = App.GetService<DocumentOverviewViewModel>();
        InitializeComponent();
    }
}
