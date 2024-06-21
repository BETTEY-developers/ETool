using System;
using System.Diagnostics;
using System.Reflection;
using EliTool.ExtensionSDK._Internal;
using EliTool.ExtensionSDK.Common;
using EliTool.ExtensionSDK.Model;
using EliTool.ExtensionSDK.Model.Document;
using EliTool.ExtensionSDK.Model.Tool;

namespace EliTool.ExtensionSDK;

public abstract class ExtensionBase : IMain
{
    internal _InternalSystem InternalSystem
    {
        get; 
        private set;
    }

    public abstract string Name
    {
        get;
    }

    public abstract SettingCollection GetExternSettingsCollection();
    public abstract ExtensionInfo GetInfo();
    public abstract void Install();
    public abstract void Uninstall();
    public abstract ToolGroupCollection GetExtensionToolGroups();
    public abstract DocumentGroupCollection GetExtensionDocumentGroups();

    public ExtensionBase(IContainer container)
    {
        InternalSystem = _InternalSystem._InternalSystem_Factory.Get_InternalSystem(container);
    }

    public static ExtensionBase GetCurrentExtension()
    {
        var dass = Assembly.GetCallingAssembly();
        var assem = new StackTrace().GetFrame(1).GetMethod().DeclaringType.Assembly;
        return (ExtensionBase)assem
            .GetType(assem.GetName().Name + ".Main")
            .GetProperty("Instance", System.Reflection.BindingFlags.Static).GetValue(null);
    }
}
