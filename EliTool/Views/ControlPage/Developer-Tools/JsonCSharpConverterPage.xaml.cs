using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace EliTool.Views.ControlPage.DeveloperTools;

public sealed partial class JsonCSharpConverterPage : Page
{
    public JsonCSharpConverterViewModel ViewModel
    {
        get;
    }

    public JsonCSharpConverterPage()
    {
        ViewModel = App.GetService<JsonCSharpConverterViewModel>();
        InitializeComponent();
    }
}
