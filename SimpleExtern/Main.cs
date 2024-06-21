using System;
using System.Collections.Generic;
using EliTool.BasePackage.Contracts.Services;
using EliTool.BasePackage.Services;
using EliTool.ExtensionSDK;
using EliTool.ExtensionSDK._Internal;
using EliTool.ExtensionSDK.Model;
using EliTool.ExtensionSDK.Model.Document;
using EliTool.ExtensionSDK.Model.Tool;
using Microsoft.UI.Xaml.Controls;
using SimpleExtern.Common;
using SimpleExtern.ViewModels;
using SimpleExtern.Views;
using SimpleExtern.Views.Tool;

namespace SimpleExtern;

public class Main : ExtensionBase
{
    private struct Singleton
    {
        Main Instance;

        public Singleton(Main instance)
            => Instance = instance;

        public Main GetInstance() => Instance;
    }

    private static ServiceRegister _services = new();
    private static Singleton singleton;

    public override string Name => GetInfo().Name;

    public static ExtensionBase Instance => singleton.GetInstance();

    public Main(IContainer container) : base(container)
    {
        singleton = new(this);
    }

    public override ExtensionInfo GetInfo() => new()
    {
        Name = "SimpleExtern",
        Author = "ETool Team",
        DisplayName = "Simple Extern",
        AuthorUrl = "nullptr",
        Description = "For a test",
        IconPath = "\\Assets\\SimpleExtern.png",
        Version = "v1.0"
    };

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
    public override ToolGroupCollection GetExtensionToolGroups()
    {
        return new()
        {
            new()
            {
                Title = "SimpleExtern",
                Id = "SimpleExtern",
                HeaderImage = new("Assets\\SimpleExtern.png", true),
                ToolInfos = new List<ToolInfo>()
                {
                    new ToolInfo()
                    {
                        PageViewModel = typeof(TestPageViewModel),
                        Page = typeof(TestPage),
                        Title = "Test Page",
                        Subtitle = "For a test",
                        HeaderImage = new("Assets\\SimpleExtern.png", true),
                        LinkMode = ToolLinkMode.Static
                    }
                }
            }
        };
    }


    public override DocumentGroupCollection GetExtensionDocumentGroups() => new();
}
