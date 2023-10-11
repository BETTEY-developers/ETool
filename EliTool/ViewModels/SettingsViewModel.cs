using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using EliTool.Contracts.Services;
using EliTool.Helpers;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel;
using Windows.Storage;

namespace EliTool.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;

    private ElementTheme _elementTheme;
    public bool _themeChanged = false;
    public int ElementTheme
    {
        get
        {
            return (int)_themeSelectorService.Theme;
        }
        set
        {
            _themeSelectorService.SetThemeAsync((ElementTheme)value);
            OnPropertyChanged(nameof(ElementTheme));
        }
    }

    public int SearchItemType
    {
        get
        {
            return int.Parse(ApplicationData.Current.LocalSettings.Values["SearchItem"].ToString());
        }
        set
        {
            ApplicationData.Current.LocalSettings.Values["SearchItem"] = value;
            OnPropertyChanged(nameof(SearchItemType));
        }
    }

    [ObservableProperty]
    private string _versionDescription;

    public SettingsViewModel(IThemeSelectorService themeSelectorService)
    {
        _themeSelectorService = themeSelectorService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();
    }

    public void SelectSearchItemType(object sender, SelectionChangedEventArgs e)
    {
        if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("SearchItemStyle"))
        {
            ApplicationData.Current.LocalSettings.Values.Add("SearchItemStyle", (sender as ComboBox).SelectedItem);
        }

    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
