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

namespace EliTool.Helpers;

public class ExternIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var ser = App.GetService<IExternService>();
        var obj = value as ExternManifestInfo;
        BitmapImage img = new BitmapImage(new Uri(ser.ApplicationExternUnpackageFolder.Path + "\\" + obj.Name + "\\" + obj.Name + "\\" + obj.IconPath));
        return (ImageSource)img;
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
