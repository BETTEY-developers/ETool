using CommunityToolkit.WinUI.Controls;
using EliTool.ViewModels;
using Microsoft.UI.Xaml;
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
        ViewModel.Page = this;
        InitializeComponent();
    }

    private void Segmented_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CodeTextBox == null)
            return;
        if (((e.AddedItems[0] as SegmentedItem).Content as string) == "C#")
            CodeTextBox.IsReadOnly = true;
        else
            CodeTextBox.IsReadOnly = false;
    }

    private void CopyCS_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TeachingTip tt = new TeachingTip()
        {
            Target = (FrameworkElement)sender,
            Content = "Copied!",
            IsOpen = true,
        };
    }
}

