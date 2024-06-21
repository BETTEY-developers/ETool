using System;
using System.Collections.Generic;
using EliTool.ExtensionSDK.Model;
using EliTool.ExtensionSDK.Model.Document;
using EliTool.ExtensionSDK.Model.Tool;

namespace EliTool.ExtensionSDK;

public interface IMain : IInstall, IUninstall
{
    public ToolGroupCollection GetExtensionToolGroups();

    public SettingCollection GetExternSettingsCollection();

    public ExtensionInfo GetInfo();

    public DocumentGroupCollection GetExtensionDocumentGroups();
}
