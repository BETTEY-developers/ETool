using System.Collections.Generic;

namespace EliTool.ExternSDK;

public interface IMain : IInstall, IUninstall, IInfo
{
    public List<string> GetExternPageList();

    public List<string> GetExternSettingsList();

    public IInfo GetInfo();
}
