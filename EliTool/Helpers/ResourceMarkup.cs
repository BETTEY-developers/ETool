using Microsoft.UI.Xaml.Markup;

namespace EliTool.Helpers;

[MarkupExtensionReturnType(ReturnType = typeof(string))]
public class ResourceMarkup : MarkupExtension
{
    public string ResourceKey
    {
        get; set; 
    }

    public string Sub
    {
        get; set;
    } = "";

    protected override object ProvideValue()
    {
        return ResourceExtensions.GetLocalized(ResourceKey, Sub == "" ? "Resources" : Sub);
    }
}
