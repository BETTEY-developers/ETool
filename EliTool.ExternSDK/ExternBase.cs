using EliTool.ExternSDK._Internal;
using EliTool.ExternSDK.Common;
using EliTool.ExternSDK.Model;

namespace EliTool.ExternSDK;

public class ExternBase : IMain
{
    public static ExternBase Main
    {
        get;
        private set;
    }

    internal static _InternalSystem InternalSystem
    {
        get; 
        private set;
    }

    public virtual string Name
    {
        get;
    }
    public virtual string DisplayName
    {
        get;
    }
    public virtual string Description
    {
        get;
    }
    public virtual string Version
    {
        get;
    }
    public virtual string Author
    {
        get;
    }
    public virtual string AuthorUrl
    {
        get;
    }
    public virtual string IconPath
    {
        get;
    }

    public virtual PageInfoGroup GetExternPageGroup() => null;
    public virtual SettingCollection GetExternSettingsCollection() => null;
    public virtual IInfo GetInfo() => null;
    public virtual void Install() { }
    public virtual void Uninstall() { }

    public ExternBase(IAppContext appContext)
    {
        InternalSystem = _InternalSystem._InternalSystem_Factory.Get_InternalSystem(appContext);
        Main = this;
    }
}
