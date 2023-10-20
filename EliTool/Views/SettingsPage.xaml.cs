using System.Timers;
using EliTool.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.System;

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
        if (e.RemovedItems.Count <= 0)
            return;

        ChangedTip.Target = (FrameworkElement)sender;
        ChangedTip.IsOpen = true;
    }

    private void SettingsCard_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Launcher.LaunchUriAsync(new("https://github.com/BETTEY-developers/ETool/issues/new/choose"));
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.RemovedItems.Count <= 0)
            return;

        ChangedTip.Target = (FrameworkElement)sender;
        ChangedTip.IsOpen = true;
    }
}
