using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EliTool.Controls.Primitives;
using EliTool.ExtensionSDK._Internal;
using EliTool.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;

using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EliTool.Controls;

internal interface IExampleUnit
{
    public string GetExampleText();
}

public class FormatOption : DependencyObject, IExampleUnit
{
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

    // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(FormatOption), new PropertyMetadata(""));



    public string Description
    {
        get
        {
            return (string)GetValue(DescriptionProperty);
        }
        set
        {
            SetValue(DescriptionProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register("Description", typeof(string), typeof(FormatOption), new PropertyMetadata(""));



    public string FormatFlag
    {
        get
        {
            return (string)GetValue(FormatFlagProperty);
        }
        set
        {
            SetValue(FormatFlagProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for FormatFlag.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FormatFlagProperty =
        DependencyProperty.Register("FormatFlag", typeof(string), typeof(FormatOption), new PropertyMetadata(""));

    public string GetExampleText() => FormatFlag;
}

public partial class ItemsPresenter : ObservableRecipient, IExampleUnit
{
    [ObservableProperty]
    private bool _isFormatFlag;

    [ObservableProperty]
    private string _displayText;

    public string GetExampleText()
    {
        if (_isFormatFlag)
            return $"${_displayText}$";

        return _displayText;
    }
}

[ContentProperty(Name = "FormatOptions")]
public sealed partial class FormatTextBox : UserControl
{
    private bool _isFlyoutShowing = false;
    public FormatTextBoxViewModel ViewModel = new();

    protected override void OnPointerEntered(PointerRoutedEventArgs e)
    {
        Edit.Visibility = Visibility.Visible;
        base.OnPointerEntered(e);
    }

    protected override void OnPointerExited(PointerRoutedEventArgs e)
    {
        if(!_isFlyoutShowing)
            Edit.Visibility = Visibility.Collapsed;

        base.OnPointerExited(e);
    }

    public FormatTextBox()
    {
        this.InitializeComponent();
        ViewModel.DisplayItems.CollectionChanged += (sender, args) =>
        {
            if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                (args.NewItems[0] as ItemsPresenter).PropertyChanged += (sender, args) =>
                {
                    ObservableCollection<ItemsPresenter> items = ViewModel.DisplayItems;
                    StringBuilder sb = new();
                    foreach (var item in items)
                    {
                        sb.Append(item.GetExampleText());
                    }
                    ExampleText.Text = sb.ToString();
                };
            }
            ObservableCollection<ItemsPresenter> items = ViewModel.DisplayItems;
            StringBuilder sb = new();
            foreach (var item in items)
            {
                sb.Append(item.GetExampleText());
            }
            ExampleText.Text = sb.ToString();
        };
    }

    private void DisplayItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        Debug.WriteLine(e.NewItems);
    }

    public ObservableCollection<FormatOption> FormatOptions
    {
        get
        {
            try
            {
                return (ObservableCollection<FormatOption>)GetValue(FormatOptionsProperty);
            }
            catch
            {
                return new ObservableCollection<FormatOption>();
            }
        }
        set
        {
            SetValue(FormatOptionsProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for FormatOptions.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FormatOptionsProperty =
        DependencyProperty.Register("FormatOptions", typeof(ObservableCollection<FormatOption>), typeof(FormatTextBox), new PropertyMetadata(new ObservableCollection<FormatOption>()));

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        _isFlyoutShowing = true;
        EditFlyout.ShowAsync();
    }

    private void EditFlyout_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
    {
        _isFlyoutShowing = false;
        Edit.Visibility = Visibility.Collapsed;
    }

    private void SegmentTextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
        ViewModel.DisplayItems.Add(new()
        {
            DisplayText = "1",
            IsFormatFlag = false,
        });
    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        var opt = (e.ClickedItem as FormatOption);
        ViewModel.DisplayItems.Add(new()
        {
            DisplayText = opt.FormatFlag,
            IsFormatFlag = true
        });
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if(textBox.Text == "")
        {
            ViewModel.DisplayItems.Remove(textBox.Tag as ItemsPresenter);
        }
    }

    private void Item_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        GridViewItem item = sender as GridViewItem;
        (item.FindName("DeleteButton") as Button).Visibility = Visibility.Visible;
        (item.FindName("FormatDisplay") as StackPanel).Spacing = 12;
    }

    private void Item_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        GridViewItem item = sender as GridViewItem;
        (item.FindName("DeleteButton") as Button).Visibility = Visibility.Collapsed;
        (item.FindName("FormatDisplay") as StackPanel).Spacing = 0;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.DisplayItems.Remove((sender as Button).Tag as ItemsPresenter);
    }
}
