using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace EliTool.Helpers;
internal class BooleanToEnableStateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
