using Microsoft.UI.Xaml.Data;

namespace EliTool.Helpers;

public class UriConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        try
        {
            return new Uri((string)value);
        }
        catch
        {
            return new Uri("about:blank");
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
