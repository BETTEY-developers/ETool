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
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EliTool.Controls;

public sealed class KeyValuePairConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        StackPanel itemlist = new StackPanel();
        foreach (var kvp in (ObservableCollection<KeyValuePairStringItem>)value)
        {
            itemlist.Children.Add(new HyperlinkButton
            {
                Content = kvp.Key,
                NavigateUri = new Uri(kvp.Value)
            });
        }
        return itemlist;
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}

public sealed partial class KeyValuePairStringItem : ContentControl
{
    public KeyValuePairStringItem()
    {
        
    }

    public string Key
    {
        get
        {
            return (string)GetValue(KeyProperty);
        }
        set
        {
            SetValue(KeyProperty, value);
        }
    }

    public string Value
    {
        get
        {
            return (string)GetValue(ValueProperty);
        }
        set
        {
            SetValue(ValueProperty, value);
        }
    }

    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register("Key", typeof(ObservableCollection<KeyValuePairStringItem>), typeof(KeyValuePairStringItem), new PropertyMetadata(null));

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(ObservableCollection<KeyValuePairStringItem>), typeof(KeyValuePairStringItem), new PropertyMetadata(null));
}
public sealed partial class OperatorBar : UserControl
{
    public OperatorBar()
    {
        this.InitializeComponent();
        ResetValue<ObservableCollection<KeyValuePairStringItem>>(GithubSourcesProperty);
        ResetValue<ObservableCollection<KeyValuePairStringItem>>(DocumentationsProperty);
        SetValue(PageLinkProperty, "");
    }

    private void ResetValue<T>(DependencyProperty prop) where T : class, new()
    {
        SetValue(prop, new T());
    }

    public ObservableCollection<KeyValuePairStringItem> Documentations
    {
        get
        {
            return (ObservableCollection<KeyValuePairStringItem>)GetValue(DocumentationsProperty);
        }
        set
        {
            SetValue(DocumentationsProperty, value);
        }
    }

    public static readonly DependencyProperty DocumentationsProperty =
        DependencyProperty.Register("Documentations", typeof(ObservableCollection<KeyValuePairStringItem>), typeof(OperatorBar), new PropertyMetadata(new ObservableCollection<KeyValuePairStringItem>()));

    public ObservableCollection<KeyValuePairStringItem> GithubSources
    {
        get
        {
            return (ObservableCollection<KeyValuePairStringItem>)GetValue(GithubSourcesProperty);
        }
        set
        {
            SetValue(GithubSourcesProperty, value);
        }
    }

    public static readonly DependencyProperty GithubSourcesProperty =
        DependencyProperty.Register("GithubSources", typeof(ObservableCollection<KeyValuePairStringItem>), typeof(OperatorBar), new PropertyMetadata(new ObservableCollection<KeyValuePairStringItem>()));

    public string PageLink
    {
        get
        {
            return (string)GetValue(PageLinkProperty);
        }
        set
        {
            SetValue(PageLinkProperty, value);
        }
    }

    public static readonly DependencyProperty PageLinkProperty =
        DependencyProperty.Register("PageLink", typeof(string), typeof(OperatorBar), new PropertyMetadata(null));

    [RelayCommand]
    public void CopyLink()
    {
        DataPackage data = new DataPackage();
        data.SetText(PageLink);
        Clipboard.SetContent(data);
        CopyTip.IsOpen = true;
    }

    private void DropDownButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
