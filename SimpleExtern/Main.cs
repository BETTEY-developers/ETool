using System.Collections.Generic;
using System.Diagnostics;
using EliTool.ExternSDK;
using Windows.Storage;
using Windows.UI.Popups;

namespace SimpleExtern;

public class Main : IMain
{
    public string Name => "A Simple Extern";

    public string Description => "For a test";

    public string Version => "v1.0";

    public string Author => "ETool Team";

    public string AuthorUrl => "nullptr";

    public string IconPath => "Assets\\SimpleExtern.png";

    public List<string> GetExternPageList() => new List<string>() { "SimpleExtern.TestPage" };
    public List<string> GetExternSettingsList() => new List<string> { "" };
    public IInfo GetInfo() => this;
    public void Install() => ApplicationData.Current.LocalFolder.CreateFileAsync("OK.txt");
    public void Uninstall() { }
}
