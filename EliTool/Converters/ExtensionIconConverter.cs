using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.Contracts.Services;
using EliTool.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace EliTool.Converters;

public class ExtensionIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var ser = App.GetService<IExtensionService>();
        var obj = value as ExtensionManifestInfo;
        var img = new BitmapImage(new Uri(Path.Combine(ser.ApplicationExtensionUnpackageFolder.Path, obj.Name, obj.Name, obj.IconPath)));
        return img;
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}

public class ExtensionImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var ser = App.GetService<IExtensionService>();
        var path = value as string;
        var img = new BitmapImage(new Uri(Path.Combine(ser.ApplicationExtensionUnpackageFolder.Path, path)));
        return img;
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}

public class ExtensionResourceHelper
{
    public static string GetExtensionResourceRealPath(string resourcePath, string externName)
    {
        var ser = App.GetService<IExtensionService>();
        return Path.Combine(ser.ApplicationExtensionUnpackageFolder.Path, externName, externName, resourcePath);
    }
}
