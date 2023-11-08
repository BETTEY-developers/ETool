using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using EliTool.Views.Dialogs.DeveloperTools;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EliTool.Views.Dialogs.DeveloperTools;

public partial class PictureConverterOptionEditerViewModel : ObservableRecipient
{
    [ObservableProperty]
    private string title = "";
}

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PictureConverterTitleEditer : Page, IResultDialog<string>
{
    private PictureConverterOptionEditerViewModel ViewModel
    {
        get; set;
    } = new();

    public PictureConverterTitleEditer()
    {
        this.InitializeComponent();
    }

    public void SetIn(string title) => ViewModel.Title = title;

    public string Result => ViewModel.Title;
}
