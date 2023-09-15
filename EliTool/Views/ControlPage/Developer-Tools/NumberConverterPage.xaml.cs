using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace EliTool.Views;

public sealed partial class NumberConverterPage : Page
{
    public NumberConverterViewModel ViewModel
    {
        get;
    }

    public NumberConverterPage()
    {
        ViewModel = App.GetService<NumberConverterViewModel>();
        InitializeComponent();
    }
}
