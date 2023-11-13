using System;
using System.Collections.Generic;
using EliTool.BasePackage.Contacts.Services;
using EliTool.BasePackage.Services;
using EliTool.ExternSDK;
using ETool.ExternSDK.Model;
using SimpleExtern.Common;
using SimpleExtern.ViewModels;
using SimpleExtern.Views;

namespace SimpleExtern;

public class Main : IMain
{
    private static ServiceRegister _services = new();

    public string Name => "SimpleExtern";

    public string DisplayName => "A Simple Extern";

    public string Description => "For a test";

    public string Version => "v1.0";

    public string Author => "ETool Team";

    public string AuthorUrl => "nullptr";

    public string IconPath => "Assets\\SimpleExtern.png";

    public IInfo GetInfo() => this;

    public void Install()
    {
        // Initialize in it

        // Register Services
        _services.Configure<IPageService, PageService>();

        // Register Pages
        PageService ser = (PageService)Main.GetService<IPageService>();
        ser.AddDependence<TestPageViewModel, TestPage>();
    }
    public void Uninstall() { }

    public static T GetService<T>()
        where T : class
    {
        if (_services.GetServiceToObject<T>() is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within IMain.Install");
        }

        return service;
    }

    public Dictionary<Type, Type> GetExternPageList() => new Dictionary<Type, Type>()
    {
        [typeof(TestPageViewModel)] = typeof(TestPage)
    };

    public SettingCollection GetExternSettingsCollection() => new SettingCollection();
}
