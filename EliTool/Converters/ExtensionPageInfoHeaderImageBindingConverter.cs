using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExtensionSDK.Media;
using Microsoft.UI.Xaml.Data;

namespace EliTool.Converters;

public class ExtensionPageInfoHeaderImageBindingConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        Image image = value as Image;
        return image.AsWinUIImageSource();
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
