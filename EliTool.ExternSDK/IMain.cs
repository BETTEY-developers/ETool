using System;
using System.Collections.Generic;
using ETool.ExternSDK.Model;

namespace EliTool.ExternSDK;

public interface IMain : IInstall, IUninstall, IInfo
{
    public Dictionary<Type, Type> GetExternPageList();

    public SettingCollection GetExternSettingsCollection();

    public IInfo GetInfo();
}
