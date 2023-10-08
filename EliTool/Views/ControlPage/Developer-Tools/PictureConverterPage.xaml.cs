using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views.ControlPage.DeveloperTools;

public sealed partial class PictureConverterPage : Page
{
    public PictureConverterViewModel ViewModel
    {
        get;
    }

    public PictureConverterPage()
    {
        ViewModel = App.GetService<PictureConverterViewModel>();
        ViewModel.Page = this;
        InitializeComponent();
    }

    private void Page_ProcessKeyboardAccelerators(Microsoft.UI.Xaml.UIElement sender, ProcessKeyboardAcceleratorEventArgs args)
    {
        if(ViewModel.AcceleratorActions.ContainsKey((args.Modifiers, args.Key)))
            ViewModel.AcceleratorActions[(args.Modifiers, args.Key)]();
    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ContentArea.Focus(Microsoft.UI.Xaml.FocusState.Pointer);
#if DEBUG
        ViewModel.Options.OptionUnits.Add(new OptionUnit()
        {
            Name = "Test",
            NameRule = "$t$-$rn$ $s$",
            NonTransparent = true,
            NonTransparentColor = Microsoft.UI.Colors.Salmon
        });
#endif

    }

    private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        
    }
}
