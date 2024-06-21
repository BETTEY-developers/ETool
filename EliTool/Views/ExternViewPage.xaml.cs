using System.IO.Compression;
using System.Text.Json;
using CommunityToolkit.Mvvm.Input;
using EliTool.BasePackage.Contracts.Services;
using EliTool.Contracts.Services;
using EliTool.Helpers;
using EliTool.Models;
using EliTool.ViewModels;
using EliTool.Views.ExternViews;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Windows.Storage;

namespace EliTool.Views;

public sealed partial class ExternViewPage : Page
{
    public ExternViewViewModel ViewModel
    {
        get;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        base.OnNavigatedTo(e);
    }

    public ExternViewPage()
    {
        ViewModel = App.GetService<ExternViewViewModel>();
        InitializeComponent();
    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        App.GetService<INavigationService>().Frame.Navigate(typeof(ExternDetailPage), e.ClickedItem, new DrillInNavigationTransitionInfo());
    }

    [RelayCommand]
    public async void PickExtern()
    {
        var file = await Helpers.DialogHelper.OpenSingleFilePicker(new List<string>() { ".ete" });
        if (file == null)
            return;
        try
        {
            using var zip = ZipFile.Open(file.Path, ZipArchiveMode.Read);
        }
        catch (InvalidDataException)
        {
            DialogHelper.StringContentDialog("非法文件", "此扩展文件格式非法，请联系开发者。", ContentDialogButton.Primary, Primary: "确定");
            return;
        }

        var t = await App.GetService<IExtensionService>().ApplicationExtensionFolder.TryGetItemAsync(file.Name);
        if (t != null && t.IsOfType(StorageItemTypes.File))
        {
            await t.DeleteAsync();
        }

        await file.CopyAsync(App.GetService<IExtensionService>().ApplicationExtensionFolder);

        var content = await FileIO.ReadTextAsync(App.GetService<IExtensionService>().ExtensionManifest);
        var is_empty = false;

        if (content == "")
        {
            content = "[{\"Name\": \"\"}]";
            is_empty = true;
        }
        
        var es = JsonSerializer.Deserialize<List<Extension>>(content);

        if (is_empty)
            es.Clear();
        es.Add(new()
        {
            Name = file.DisplayName
        });

        var stream = await file.OpenStreamForWriteAsync();
        stream.Seek(0, SeekOrigin.Begin);
        JsonSerializer.Serialize(stream, es);

        stream.Close();

        await App.GetService<IExtensionService>().Load();

        ViewModel.ExternInfos.Clear();
        (App.GetService<IExtensionService>().Extensions ?? new List<Extension>()).ForEach(ViewModel.ExternInfos.Add);
    }
}
