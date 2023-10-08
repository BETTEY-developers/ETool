using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace EliTool.Helpers;

class BrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) => new SolidColorBrush((Color)value);
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
