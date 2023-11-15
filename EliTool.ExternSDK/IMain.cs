using System;
using System.Collections.Generic;
using EliTool.ExternSDK.Model;

namespace EliTool.ExternSDK;

public interface IMain : IInstall, IUninstall, IInfo
{
    public PageInfoGroup GetExternPageGroup();

    public SettingCollection GetExternSettingsCollection();

    public IInfo GetInfo();
}
