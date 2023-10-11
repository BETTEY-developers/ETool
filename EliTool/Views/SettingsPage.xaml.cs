using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }

    private void SearchItemTypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(e.RemovedItems.Count > 0)
        {
            ChangedTip.IsOpen = true;
        }
    }
}
