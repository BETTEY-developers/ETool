using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace EliTool.ExternSDK.Navigate;

public class Navigate
{
    /* private Nav event for ETool Main Program */
    private static TypedEventHandler<ExternBase, Type> NavigateEvent;
    private static TypedEventHandler<ExternBase, (Type, object?, bool)> NavigateForArgsEvent;


    /// <summary>
    /// Navigate Event. Extern *must not* add listener.
    /// </summary>
    public static event TypedEventHandler<ExternBase, Type> _Navigate {  add { NavigateEvent += value; } remove { NavigateEvent -= value; } }

    /// <summary>
    /// Navigate Event. Extern *must not* add listener.
    /// </summary>
    public static event TypedEventHandler<ExternBase, (Type, object?, bool)> _NavigateForArgs { add { NavigateForArgsEvent += value; } remove { NavigateForArgsEvent -= value; } }

    /// <summary>
    /// Navigate to page.
    /// </summary>
    /// <param name="pageViewmodel">Page viewmodel type.</param>
    public static void NavigateToType(Type pageViewmodel)
    {
        NavigateEvent(ExternBase.Main, pageViewmodel);
    }

    /// <summary>
    /// Navigate to page.
    /// </summary>
    /// <param name="pageViewmodel">Page viewmodel type.</param>
    /// <param name="arg">Navigating to page for arguments</param>
    /// <param name="cleanstacks">Clean Navigate Stack</param>
    public static void NavigateToType(Type pageViewmodel, object? arg, bool cleanstacks = false)
    {
        NavigateForArgsEvent(ExternBase.Main, (pageViewmodel, arg, cleanstacks));   
    }
}
