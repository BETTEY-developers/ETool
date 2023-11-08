using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExternSDK;
using Microsoft.UI.Xaml.Controls;

namespace EliTool.Contracts.Services;

public interface IExternService
{
    public List<Page> GetExternPages();

    public List<SettingCollection> GetSettingItems();

    public Dictionary<string, string> GetInfoManifest();

    public Task Unload();

    public Task Load();
}
