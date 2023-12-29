using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Pickers.Provider;

namespace EliTool.Helpers;
public class DialogHelper
{
    private static ContentDialog _ContentDialogFactory(
        string title, 
        object content,
        ContentDialogButton DefaultButton,
        string? Primary, 
        string? Secondary, 
        string? Close)
    {

        ContentDialog dialog = new ContentDialog();

        // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = title;
        dialog.PrimaryButtonText = Primary;
        dialog.DefaultButton = DefaultButton;
        dialog.Content = content;
        dialog.SecondaryButtonText = Secondary;
        dialog.CloseButtonText = Close;

        return dialog;
    }

    public static ContentDialog StringContentDialog(
        string title,
        string content,
        ContentDialogButton DefaultButton = ContentDialogButton.Primary,
        string? Primary = null,
        string? Secondary = null,
        string? Close = null) => _ContentDialogFactory(title, content.Replace("$LineBreak$", Environment.NewLine), DefaultButton, Primary, Secondary, Close);
    
    public static ContentDialog PageContentDialog(
        string title,
        Page content,
        ContentDialogButton DefaultButton = ContentDialogButton.Primary,
        string? Primary = null,
        string? Secondary = null,
        string? Close = null) => _ContentDialogFactory(title, content, DefaultButton, Primary, Secondary, Close);


    private static FileOpenPicker _OpenFilePickerDialogFactory(
        IList<string> ChooseType,
        PickerViewMode ViewMode = PickerViewMode.List,
        PickerLocationId Location = PickerLocationId.Unspecified,
        string CommitButton = "")
    {
        FileOpenPicker fileOpenPicker = new();

        foreach (var item in ChooseType)
        {
            fileOpenPicker.FileTypeFilter.Add(item);
        }

        WinRT.Interop.InitializeWithWindow.Initialize(
            fileOpenPicker, 
            MainWindow.Current.GetWindowHandle()
        );

        fileOpenPicker.CommitButtonText = CommitButton;
        fileOpenPicker.SuggestedStartLocation = Location;

        return fileOpenPicker;
    }

    //private static FileSavePicker _SaveFilePickerDialogFactory(
    //    string Title,
    //    PickerViewMode ViewMode = PickerViewMode.List,
    //    PickerLocationId Location = PickerLocationId.Unspecified,
    //    string CommitButton = "")
    //{
    //    FileSavePicker fileOpenPicker = new();

    //    fileOpenPicker.

    //    WinRT.Interop.InitializeWithWindow.Initialize(
    //        fileOpenPicker,
    //        MainWindow.Current.GetWindowHandle()
    //    );

    //    fileOpenPicker.CommitButtonText = CommitButton;
    //    fileOpenPicker.SuggestedStartLocation = Location;

    //    return fileOpenPicker;
    //}

    public static async Task<StorageFile> OpenSingleFilePicker(
        IList<string> ChooseType,
        PickerViewMode ViewMode = PickerViewMode.List,
        PickerLocationId Location = PickerLocationId.Unspecified,
        string CommitButton = "") => await _OpenFilePickerDialogFactory(ChooseType, ViewMode, Location, CommitButton).PickSingleFileAsync();

    public static async Task<IReadOnlyList<StorageFile>> OpenMultipleFilesPicker(
        IList<string> ChooseType,
        PickerViewMode ViewMode = PickerViewMode.List,
        PickerLocationId Location = PickerLocationId.Unspecified,
        string CommitButton = "") => await _OpenFilePickerDialogFactory(ChooseType, ViewMode, Location, CommitButton).PickMultipleFilesAsync();
}
