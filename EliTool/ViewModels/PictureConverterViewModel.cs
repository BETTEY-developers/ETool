

using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EliTool.Core.Contracts.Services;
using EliTool.Views.ControlPage.DeveloperTools;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;

using KeyboardAcceleratorActions = System.Collections.Generic.Dictionary<(Windows.System.VirtualKeyModifiers, Windows.System.VirtualKey), System.Action>;

namespace EliTool.ViewModels;

public class OptionUnit : ObservableObject
{
    public enum SaveMultiplicativeType
    {
        Once,
        Twice,
        Fourth,
        Eighth
    }

    string _name;
    public string Name
    {
        get => _name;
        set
        {
            SetProperty(ref _name, value);
            OnPropertyChanged(nameof(Name));
        }
    }

    string _nameRule;
    public string NameRule
    {
        get => _nameRule;
        set
        {
            SetProperty(ref _nameRule, value);
            OnPropertyChanged(nameof(NameRule));
        }
    }

    public SaveMultiplicativeType _saveMultiplicative;
    public SaveMultiplicativeType SaveMultiplicative
    {
        get => _saveMultiplicative;
        set
        {
            SetProperty(ref _saveMultiplicative, value);
            OnPropertyChanged(nameof(SaveMultiplicative));
        }
    }

    public bool _nonTransparent = false;
    public bool NonTransparent
    {
        get => _nonTransparent;
        set
        {
            SetProperty(ref _nonTransparent, value);
            OnPropertyChanged(nameof(NonTransparent));
        }
    }

    public Color _nonTransparentColor;
    public Color NonTransparentColor
    {
        get => _nonTransparentColor;
        set
        {
            _nonTransparentColor = value;
            OnPropertyChanged(nameof(NonTransparentColor));
        }
    }
}

public class PictureConverterOptions : ObservableObject
{
    

    public string _optionCollectionName;
    public string OptionCollectionName
    {
        get
        {
            return _optionCollectionName;
        }
        set
        {
            _optionCollectionName = value;
            OnPropertyChanged(nameof(OptionCollectionName));
        }
    }

    public ObservableCollection<OptionUnit> _optionUnits = new ObservableCollection<OptionUnit>();
    public ObservableCollection<OptionUnit> OptionUnits
    {
        get
        {
            return _optionUnits;
        }
        set
        {
            _optionUnits = value;
            OnPropertyChanged(nameof(OptionUnits));
        }
    }


}
public partial class PictureConverterViewModel : ObservableRecipient
{
    

    public PictureConverterViewModel()
    {
        AcceleratorActions = new KeyboardAcceleratorActions
        {
            [(Windows.System.VirtualKeyModifiers.Control, Windows.System.VirtualKey.V)] = PastePicture
        };
    }

    public PictureConverterPage Page
    {
        get; set; 
    }

    [ObservableProperty]
    BitmapImage picturePath = new ();

    [ObservableProperty]
    PictureConverterOptions options = new();

    public OptionUnit ClickedItem
    {
        set; get;
    } = new OptionUnit();

    public KeyboardAcceleratorActions AcceleratorActions { get; set; }
    
    public async void PastePicture()
    {
        var package = Clipboard.GetContent();
        if (!package.Contains(StandardDataFormats.Bitmap))
            return;

        await PicturePath.SetSourceAsync(await (await package.GetBitmapAsync()).OpenReadAsync());
    }

    [RelayCommand]
    public async void PickPicture()
    {
        var filepicker = new Windows.Storage.Pickers.FileOpenPicker();

        filepicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

        filepicker.FileTypeFilter.Add(".png");
        filepicker.FileTypeFilter.Add(".jpeg");
        filepicker.FileTypeFilter.Add(".jpg");
        filepicker.FileTypeFilter.Add(".bmp");
        filepicker.FileTypeFilter.Add(".tiff");
        filepicker.FileTypeFilter.Add(".tif");
        filepicker.FileTypeFilter.Add(".heic");
        filepicker.FileTypeFilter.Add(".dib");

        filepicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;

        WinRT.Interop.InitializeWithWindow.Initialize(filepicker, App.MainWindow.GetWindowHandle());

        var file = await filepicker.PickSingleFileAsync();

        if (file == null)
            return;

        BitmapImage image = new BitmapImage();
        await image.SetSourceAsync(await file.OpenReadAsync());
        PicturePath = image;
    }

    [RelayCommand]
    public void ResetPicture()
    {

    }

    [RelayCommand]
    public async void SelectOptions()
    {
        var filepicker = new Windows.Storage.Pickers.FileOpenPicker();

        filepicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

        filepicker.FileTypeFilter.Add(".conf");

        WinRT.Interop.InitializeWithWindow.Initialize(filepicker, App.MainWindow.GetWindowHandle());

        var file = await filepicker.PickSingleFileAsync();

        if (file == null)
            return;

        var stream = await file.OpenStreamForReadAsync();
        byte[] data = new byte[stream.Length];
    stream.ReadAsync(data, 0, data.Length);

        string json = Encoding.UTF8.GetString(data);

        ContentDialog dialog = new ContentDialog();
        dialog.XamlRoot = Page.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "选择配置";
        dialog.PrimaryButtonText = "选择";
        dialog.SecondaryButtonText = "不选择";
        dialog.CloseButtonText = "取消";
        dialog.DefaultButton = ContentDialogButton.Primary;
        dialog.Content = "选择配置会覆盖原有配置，确定选择吗";

        var result = await dialog.ShowAsync();

        if(result == ContentDialogResult.Secondary || result == ContentDialogResult.None)
            return;

        try
        {
            Options = JsonSerializer.Deserialize<PictureConverterOptions>(json);
        }
        catch
        {
            dialog.Content = "选取失败";
            dialog.PrimaryButtonText = "确定";
            dialog.SecondaryButtonText = null;
            dialog.DefaultButton = ContentDialogButton.Primary;
            await dialog.ShowAsync();
        }

    }

    [RelayCommand]
    public void ResetOptions()
    {
    
    }

#if DEBUG
    [RelayCommand]
    public void AddItem()
    {
        Options.OptionUnits.Add(new OptionUnit()
        {
            Name = "Test1",
            NameRule = "$t$-$rn$ $s$",
            NonTransparent = true,
            NonTransparentColor = Microsoft.UI.Colors.Salmon
        });
    }
#endif
}
