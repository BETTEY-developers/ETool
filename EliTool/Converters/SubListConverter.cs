using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace EliTool.Converters;

internal class SubListConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        Binding b = parameter as Binding;
        string[] pathpart = b.Path.Path.Split(".");
        Type ct = value.GetType();
        object co = b.Source;
        foreach (var p in pathpart)
        {
            var pp = co.GetType().GetProperty(p);
            co = pp.GetValue(co);
            ct = co.GetType();
        }
        ICollection<object> c = (ICollection<object>)co;
        return System.Convert.ChangeType(c.SkipLast(int.Parse(value as string)), targetType);
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
