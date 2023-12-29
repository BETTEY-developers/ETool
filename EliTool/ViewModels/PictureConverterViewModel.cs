

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
using EliTool.Helpers;
using EliTool.Views.Dialogs.DeveloperTools;
using Windows.Media.Streaming.Adaptive;

namespace EliTool.ViewModels;

public partial class OptionUnit : ObservableObject
{
    public enum SaveMultiplicativeType
    {
        [EliTool.Core.Common.EnumText(CultureString = "zh-hans-cn", TextString = "一倍")]
        [EliTool.Core.Common.EnumText(CultureString = "zh-cn", TextString = "一倍")]
        [EliTool.Core.Common.EnumText(CultureString = "en-us", TextString = "Original")]
        Once,
        [EliTool.Core.Common.EnumText(CultureString = "zh-hans-cn", TextString = "二倍")]
        [EliTool.Core.Common.EnumText(CultureString = "zh-cn", TextString = "二倍")]
        [EliTool.Core.Common.EnumText(CultureString = "en-us", TextString = "Double")]
        Twice,
        [EliTool.Core.Common.EnumText(CultureString = "zh-hans-cn", TextString = "四倍")]
        [EliTool.Core.Common.EnumText(CultureString = "zh-cn", TextString = "四倍")]
        [EliTool.Core.Common.EnumText(CultureString = "en-us", TextString = "Fourfold")]
        Fourth,
        [EliTool.Core.Common.EnumText(CultureString = "zh-hans-cn", TextString = "八倍")]
        [EliTool.Core.Common.EnumText(CultureString = "zh-cn", TextString = "八倍")]
        [EliTool.Core.Common.EnumText(CultureString = "en-us", TextString = "Octuple")]
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

    SaveMultiplicativeType _saveMultiplicative = SaveMultiplicativeType.Once;
    public SaveMultiplicativeType SaveMultiplicative
    {
        get => _saveMultiplicative;
        set
        {
            _saveMultiplicative = value;
            OnPropertyChanged(nameof(SaveMultiplicative));
        }
    }

    bool _nonTransparent = false;
    public bool NonTransparent
    {
        get => _nonTransparent;
        set
        {
            SetProperty(ref _nonTransparent, value);
            OnPropertyChanged(nameof(NonTransparent));
        }
    }

    Color _nonTransparentColor;
    public Color NonTransparentColor
    {
        get => _nonTransparentColor;
        set
        {
            _nonTransparentColor = value;
            OnPropertyChanged(nameof(NonTransparentColor));
        }
    }

    static int _currentUID = 0;

    int _uid;
    public int UID
    {
        get => _uid;
    }

    [RelayCommand]
    public async void EditOption()
    {
        var editer = new PictureConverterOptionEditer();
        editer.SetIn(this);

        var dresult = await DialogHelper.PageContentDialog(
            "编辑选项",
           editer,
           Primary: "确定",
           Close: "取消"
            ).ShowAsync();

        if (dresult != ContentDialogResult.Primary)
            return;

        var result = editer.Result as OptionUnit;

        Name = result.Name;
        NameRule = result.NameRule;
        SaveMultiplicative = result.SaveMultiplicative;
        NonTransparent = result.NonTransparent;
        NonTransparentColor = result.NonTransparentColor;
    }

    public string NontransparentStatusToUIGlyph(bool NonTransparent)
    {
        return NonTransparent ? "\uEC61" : "\uEB90";
    }

    public Brush NontransparentStatusToUIColor(bool NonTransparent)
    {
        return NonTransparent ? (Brush)App.Current.Resources["SystemFillColorSuccessBrush"] : (Brush)App.Current.Resources["SystemFillColorCriticalBrush"];
    }

    public OptionUnit(OptionUnit obj) : this()
    {
        Name = obj.Name;
        NameRule = obj.NameRule;
        SaveMultiplicative = obj.SaveMultiplicative;
        NonTransparent = obj.NonTransparent;
        NonTransparentColor = obj.NonTransparentColor;
    }

    public OptionUnit()
    {
        _currentUID++;
        _uid = _currentUID;
    }
}

public class PictureConverterOptions : ObservableObject
{
    public string _optionCollectionName = "Name";
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
    static PictureConverterOptions options = new();

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
        PicturePath = new BitmapImage();
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
        await stream.ReadAsync(data, 0, data.Length);

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

    [RelayCommand]
    public void RemoveOption(int UID)
    {
        OptionUnit remove = new();
        foreach (var removeOption in Options.OptionUnits)
        {
            if (removeOption.UID == UID)
                remove = removeOption;
        }
        options.OptionUnits.Remove(remove);
    }

    [RelayCommand]
    public async void ChangeTitle()
    {
        PictureConverterTitleEditer editer = new();
        editer.SetIn(Options.OptionCollectionName);
        var dresult = await DialogHelper.PageContentDialog(
            "更改名称",
           editer,
           Primary: "确定",
           Close: "取消"
            ).ShowAsync();

        if (dresult != ContentDialogResult.Primary)
            return;

        Options.OptionCollectionName = editer.Result;
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
