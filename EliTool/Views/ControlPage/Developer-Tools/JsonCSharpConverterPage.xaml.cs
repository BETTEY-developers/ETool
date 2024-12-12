using CommunityToolkit.WinUI.Controls;
using EliTool.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;

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
        CodeTextBox.DataContext = ViewModel;
    }

    private void Segmented_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CodeTextBox == null)
            return;
        if (((e.AddedItems[0] as SegmentedItem).Content as string) == "C#")
        {
            CodeTextBox.IsReadOnly = true;
            ViewModel.ToCS();
        }
        else
        {
            CodeTextBox.IsReadOnly = false;
            ViewModel.ToJson();
        }

    }

    private void CopyCS_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private void CollectionPreset_TextChanged(object sender, TextChangedEventArgs e)
    {
        foreach (var item in CollectionPresetType.Items)
        {
            if ((item as ListViewItem).Content as string == CollectionPreset.Text)
                goto SUC;
        }
        CollectionPresetType.SelectedIndex = -1;

    SUC:
        ViewModel.CollectionTypeFormat = CollectionPreset.Text;
        return;
    }

    private void CollectionPresetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(CollectionPresetType.SelectedIndex != -1)
            CollectionPreset.Text = (e.AddedItems[0] as ListViewItem).Content as string;
    }
}

