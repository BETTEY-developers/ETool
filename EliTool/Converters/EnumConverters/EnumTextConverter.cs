using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Windows.Globalization;

namespace EliTool.Helpers.EnumConverters;

public class EnumTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var attrs = value.GetType()
                                               .GetField(Enum.GetName(value.GetType(), value))
                                               .GetCustomAttributesData();
        foreach (var attr in attrs)
        {
            if ((attr.NamedArguments[0].TypedValue.Value as string) == ApplicationLanguages.PrimaryLanguageOverride.ToLower())
                return attr.NamedArguments[1].TypedValue.Value as string;
        }
        return "";
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
