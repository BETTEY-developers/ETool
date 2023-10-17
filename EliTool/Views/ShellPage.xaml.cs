using System.Collections.ObjectModel;

using EliTool.Contracts.Services;
using EliTool.Helpers;
using EliTool.Models;
using EliTool.Services;
using EliTool.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;

namespace EliTool.Views;

// TODO: Update NavigationViewItem titles and icons in ShellPage.xaml.
public sealed partial class ShellPage : Page
{
    public ShellViewModel ViewModel
    {
        get;
    }

    public NavigationView NavigationControlProperty => NavigationViewControl;

    public static ShellPage Instance { get; private set; }

    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();

        ViewModel.NavigationService.Frame = NavigationFrame;
        ViewModel.NavigationViewService.Initialize(NavigationViewControl);

        // TODO: Set the title bar icon by updating /Assets/WindowIcon.ico.
        // A custom title bar is required for full window theme and Mica support.
        // https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
        App.MainWindow.Activated += MainWindow_Activated;
        AppTitleBarText.Text = "AppDisplayName".GetLocalized();
        Instance = this;
    }

    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TitleBarHelper.UpdateTitleBar(RequestedTheme);

        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
        NavigationViewControl.Header = null;
        foreach (var ControlGroup in App.GetService<MainViewModel>().GetControlInfos().Result.ControlInfoGroups)
        {
            NavigationViewItem item = new NavigationViewItem();
            item.Icon = new ImageIcon() { Source = new BitmapImage(new Uri(ControlGroup.ImagePath)) };
            item.Content = ControlGroup.Title;
            item.Name = ControlGroup.Id;
            foreach (var ControlInfo in ControlGroup.ControlInfos)
            {
                NavigationViewItem childitem = new NavigationViewItem();
                childitem.Content = ControlInfo.Title;
                childitem.Icon = new ImageIcon() { Source = new BitmapImage(new Uri(ControlInfo.ImagePath)) };
                NavigationHelper.SetNavigateTo(childitem, ControlInfo.ClickPath);
                childitem.Tag = ControlGroup.Id;
                item.MenuItems.Add(childitem);
            }
            NavigationViewControl.MenuItems.Add(item);
        }

        if (Environment.GetCommandLineArgs().Length > 1)
        {
            string str = string.Join('.', Environment.GetCommandLineArgs()[1].Split("//")[1].Split("/"));
            NavigationFrame.Navigate(Type.GetType("EliTool." + str));
        }
    }
    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";

        AppTitleBarText.Foreground = (SolidColorBrush)App.Current.Resources[resource];
        App.AppTitlebar = AppTitleBarText as UIElement;
    }

    private void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
    {
        AppTitleBar.Margin = new Thickness()
        {
            Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2 : 1),
            Top = AppTitleBar.Margin.Top,
            Right = AppTitleBar.Margin.Right,
            Bottom = AppTitleBar.Margin.Bottom
        };
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }

    private void NavigationViewControl_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if ((args.SelectedItem as NavigationViewItem)!.Tag != null)
        {
            try
            {
                (sender.FindName((string)(args.SelectedItem as NavigationViewItem)!.Tag) as NavigationViewItem).IsExpanded = true;
            }
            catch { }
        }
    }

    private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        if (args.ChosenSuggestion == null)
        {
            ObservableCollection<SearchGroup> groups = new();

            foreach (var group in App.GetService<MainViewModel>().GetControlInfos().Result.ControlInfoGroups)
            {
                if (group.ControlInfos.Any(x => x.Title.ToLower().Contains(args.QueryText.ToLower())))
                {
                    SearchGroup controlInfoGroup = new();
                    controlInfoGroup.Id = group.Id;
                    controlInfoGroup.Title = group.Title;
                    ObservableCollection<SearchItem> items = new();
                    foreach (var item in group.ControlInfos)
                    {
                        if (item.Title.ToLower().Contains(args.QueryText.ToLower()))
                            items.Add(SearchItem.FromInfoData(item));
                    }
                    controlInfoGroup.Items = items;
                    groups.Add(controlInfoGroup);
                }
            }

            App.GetService<INavigationService>().NavigateTo("EliTool.ViewModels.SearchResultViewViewModel");
            SearchResultViewPage.Instance.ViewModel.Groups = groups;
            SearchResultViewPage.Instance.ReAddAllItem();
        }
        else
        {
            var item = args.ChosenSuggestion as ControlInfoDataItem;
            if (item.ClickPath == "NoResult")
                return;

            App.GetService<INavigationService>().NavigateTo(item.ClickPath);
        }
    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if(args.Reason != AutoSuggestionBoxTextChangeReason.UserInput)
            return;

        List<ControlInfoDataItem> result = new();

        foreach (var toolgroup in App.GetService<MainViewModel>().GetControlInfos().Result.ControlInfoGroups)
        {
            foreach (var item in toolgroup.ControlInfos)
            {
                if (item.Title.ToLower().Contains(sender.Text.ToLower()))
                {
                    result.Add(item);
                }
            }
        }
        if (result.Count == 0)
        {
            result.Add(new ControlInfoDataItem() { ImagePath = this.Resources["NoResult"] as string, Title = "没有结果", ClickPath = "NoResult" });
        }

        sender.ItemsSource = result;
    }

    private DataTemplate GetItemTemplate()
    {
        return (DataTemplate)CommunityToolkit.WinUI.FrameworkElementExtensions.FindResource(this,Enum.GetName(typeof(Core.Common.SearchItemType),ApplicationData.Current.LocalSettings.Values["SearchItem"]??0) + "-SearchItem");
    }
}
