using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;

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
}
