using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Printing3D;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EliTool.Controls;

public sealed class SwitchViewCase : DependencyObject
{
    public object Threshold
    {
        get
        {
            return (object)GetValue(ThresholdProperty);
        }
        set
        {
            SetValue(ThresholdProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Threshold.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ThresholdProperty =
        DependencyProperty.Register("Threshold", typeof(object), typeof(SwitchViewCase), new PropertyMetadata(new()));

    public UIElement Content
    {
        get
        {
            return (UIElement)GetValue(ContentProperty);
        }
        set
        {
            SetValue(ContentProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register("Content", typeof(UIElement), typeof(SwitchViewCase), new PropertyMetadata(new()));
}

public class SwitchValueToContentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        SwitchView view = value as SwitchView;
        object s = view.SwitchValue;
        foreach (var switchcase in view.Cases)
        {
            if (switchcase.Threshold.Equals(System.Convert.ChangeType(view.SwitchValue, switchcase.Threshold.GetType())))
            {
                return switchcase.Content;
            }
        }//这里看起来Cases是有值的

        return new Grid();
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}

public sealed partial class SwitchView : UserControl
{
    public SwitchView()
    {
        this.InitializeComponent();
        Cases?.Clear();
    }

    public object SwitchValue
    {
        get
        {
            return (object)GetValue(SwitchValueProperty);
        }
        set
        {
            SetValue(SwitchValueProperty, value);
        }
    }

    public static readonly DependencyProperty SwitchValueProperty =
        DependencyProperty.Register("SwitchValue", typeof(object), typeof(SwitchView), new PropertyMetadata(new()));

    
    public ICollection<SwitchViewCase> Cases
    {
        get
        {
            return (ICollection<SwitchViewCase>)GetValue(CasesProperty);
        }
    }

    public static readonly DependencyProperty CasesProperty =
        DependencyProperty.Register("Cases", typeof(ICollection<SwitchViewCase>), typeof(SwitchView), new PropertyMetadata(new Collection<SwitchViewCase>()));
}
