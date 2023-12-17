using System;
using System.Collections.Generic;
using EliTool.BasePackage.Contracts.Services;
using EliTool.BasePackage.Services;
using EliTool.ExternSDK;
using EliTool.ExternSDK.Model;
using SimpleExtern.Common;
using SimpleExtern.ViewModels;
using SimpleExtern.Views;

namespace SimpleExtern;

public class Main : ExternBase
{
    private static ServiceRegister _services = new();

    public override string Name => "SimpleExtern";

    public override string DisplayName => "A Simple Extern";

    public override string Description => "For a test";

    public override string Version => "v1.0";

    public override string Author => "ETool Team";

    public override string AuthorUrl => "nullptr";

    public override string IconPath => "Assets\\SimpleExtern.png";

    public override IInfo GetInfo() => this;

    public override void Install()
    {
        // Initialize in it

        // Register Services
        _services.Configure<IPageService, PageService>();

        // Register Pages
        PageService ser = (PageService)GetService<IPageService>();
        ser.AddDependence<TestPageViewModel, TestPage>();
    }
    public override void Uninstall() { }

    public static T GetService<T>()
        where T : class
    {
        if (_services.GetServiceToObject<T>() is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within IMain.Install");
        }

        return service;
    }

    public override SettingCollection GetExternSettingsCollection() => new SettingCollection();
    public override PageInfoGroup GetExternPageGroup()
    {
        return new()
        {
            Title = "SimpleExtern",
            Id = "SimpleExtern",
            Image = new("Assets\\SimpleExtern.png", true),
            ControlInfos = new List<PageInfoDataItem>()
            {
                new PageInfoDataItem()
                {
                    PageViewModel = typeof(TestPageViewModel),
                    Page = typeof(TestPage),
                    Title = "TestPage",
                    Subtitle = "TestPage",
                    Image = new("\\Assets\\SimpleExtern.png", true)
                }
            }
        };
    }
}
