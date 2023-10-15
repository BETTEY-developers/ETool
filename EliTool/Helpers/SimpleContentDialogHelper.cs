using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace EliTool.Helpers;
public class SimpleContentDialogHelper
{
    public static ContentDialog InitContentDialog(
        string title, 
        string content,
        ContentDialogButton DefaultButton = ContentDialogButton.Primary,
        string? Primary = null, 
        string? Secondary = null, 
        string? Close = null)
    {

        ContentDialog dialog = new ContentDialog();

        // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = title;
        dialog.PrimaryButtonText = Primary;
        dialog.DefaultButton = DefaultButton;
        dialog.Content = content.Replace("$LineBreak$", "\n");
        dialog.SecondaryButtonText = Secondary;
        dialog.CloseButtonText = Close;

        return dialog;
    }
}
