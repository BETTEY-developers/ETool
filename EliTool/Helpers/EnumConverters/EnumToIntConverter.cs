using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Windows.Security.Authentication.Identity.Core;

namespace EliTool.Helpers.EnumConverters;
public class EnumToIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) => (int)value;
    public object ConvertBack(object value, Type targetType, object parameter, string language) => System.Enum.Parse(Assembly.GetEntryAssembly().GetType((string)parameter), ((int)value).ToString());
}
