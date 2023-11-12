using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace EliTool.Views.Extern;

public sealed partial class ExternDetailPage : Page
{
    public ExternDetailViewModel ViewModel
    {
        get;
    }

    public ExternDetailPage()
    {
        ViewModel = App.GetService<ExternDetailViewModel>();
        InitializeComponent();
    }
}
