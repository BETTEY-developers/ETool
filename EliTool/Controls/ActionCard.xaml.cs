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
using Windows.Storage.Search;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EliTool.Controls;

public sealed partial class ActionCard : UserControl
{
    public ActionCard()
    {
        this.InitializeComponent();
    }

    public string Title
    {
        get
        {
            return (string)GetValue(TitleProperty);
        }
        set
        {
            SetValue(TitleProperty, value);
        }
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(ActionCard), new PropertyMetadata(null));

    public ButtonBase Action
    {
        get
        {
            return (ButtonBase)GetValue(ActionProperty);
        }
        set
        {
            SetValue(ActionProperty, value);
            ButtonBase button = value;
            button.HorizontalAlignment = HorizontalAlignment.Right;
            ActionBar.Children.Add(value);
        }
    }

    public static readonly DependencyProperty ActionProperty =
        DependencyProperty.Register("Action", typeof(ButtonBase), typeof(ActionCard), new PropertyMetadata(null));

    public UIElement ActiveCardContent
    {
        get
        {
            return (UIElement)GetValue(ActiveCardContentProperty);
        }
        set
        {
            SetValue(ActiveCardContentProperty, value);
        }
    }

    public static readonly DependencyProperty ActiveCardContentProperty =
        DependencyProperty.Register("ActiveCardContent", typeof(UIElement), typeof(ActionCard), new PropertyMetadata(null));

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
    }
}
