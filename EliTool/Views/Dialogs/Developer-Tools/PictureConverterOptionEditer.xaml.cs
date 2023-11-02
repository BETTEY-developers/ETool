using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EliTool.Views.Dialogs.DeveloperTools;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>

public partial class PictureConverterOptionEditerViewModel : ObservableRecipient
{
    [ObservableProperty]
    private OptionUnit option = new OptionUnit();
}

public sealed partial class PictureConverterOptionEditer : Page
{
    public PictureConverterOptionEditerViewModel ViewModel { get; set; } = new PictureConverterOptionEditerViewModel();

    public OptionUnit Result => ViewModel.Option;

    public PictureConverterOptionEditer(OptionUnit option)
    {

        ViewModel.Option = new OptionUnit(option);
        this.InitializeComponent();
    }

    private void Colorpicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        CurrentColor.Background = new SolidColorBrush(args.NewColor);
        ViewModel.Option.NonTransparentColor = args.NewColor;
    }
}
