using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace EliTool.ExtensionSDK.Navigate;

public class Navigate
{
    /// <summary>
    /// Navigate to page.
    /// </summary>
    /// <param name="pageViewmodel">Page viewmodel type.</param>
    public static void NavigateToType(Type pageViewmodel)
    {
        ExtensionBase.GetCurrentExtension().InternalSystem.Navigate(pageViewmodel.FullName);
    }

    /// <summary>
    /// Navigate to page.
    /// </summary>
    /// <param name="pageViewmodel">Page viewmodel type.</param>
    /// <param name="arg">Navigating to page for arguments</param>
    /// <param name="cleanstacks">Clean Navigate Stack</param>
    public static void NavigateToType(Type pageViewmodel, object? arg, bool cleanstacks = false)
    {
        //NavigateForArgsEvent(ExtensionBase.GetCurrentExtension(), (pageViewmodel, arg, cleanstacks));   
    }
}
