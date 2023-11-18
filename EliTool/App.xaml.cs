using DocumentViews = EliTool.Views.Document;

using EliTool.Activation;
using EliTool.BasePackage.Contracts.Services;
using EliTool.BasePackage.Services;
using EliTool.Contracts.Services;
using EliTool.Controls;
using EliTool.Core.Contracts.Services;
using EliTool.Core.Services;
using EliTool.Helpers;
using EliTool.Models;
using EliTool.Services;
using EliTool.ViewModels;
using EliTool.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

using Windows.UI.Core.Preview;
using Windows.UI.WindowManagement;

namespace EliTool;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public static UIElement? AppTitlebar { get; set; }

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();
            services.AddTransient<ActivationHandler<EventArgs>, CloseActivationHandler>();

            // Other Activation Handlers

            // Services
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<IExternService, ExternService>();


            // Core Services
            services.AddSingleton<Core.Contracts.Services.IFileService, Core.Services.FileService>();

            // Views and ViewModels
            services.AddTransient<ExternDetailViewModel>();
            services.AddTransient<Views.ExternViews.ExternDetailPage > ();
            services.AddTransient<ExternViewViewModel>();
            services.AddTransient<ExternViewPage>();
            services.AddTransient<LoadingViewModel>();
            services.AddTransient<LoadingPage>();
            services.AddTransient<SearchResultViewViewModel>();
            services.AddTransient<SearchResultViewPage>();
            services.AddTransient<PictureConverterViewModel>();
            services.AddTransient<Views.ControlPage.DeveloperTools.PictureConverterPage>();
            services.AddTransient<OnOffLineToolsDocumentViewModel>();
            services.AddTransient<DocumentViews.OnOffLineToolsDocumentPage>();
            services.AddTransient<OnlineToolsDocumentViewModel>();
            services.AddTransient<DocumentViews.OnlineToolsDocumentPage>();
            services.AddTransient<OfflineToolsDocumentViewModel>();
            services.AddTransient<DocumentViews.OfflineToolsDocumentPage>();
            services.AddTransient<DocumentOverviewViewModel>();
            services.AddTransient<DocumentOverviewPage>();
            services.AddTransient<NumberConverterViewModel>();
            services.AddTransient<NumberConverterPage>();
            services.AddTransient<JsonCSharpConverterViewModel>();
            services.AddTransient<Views.ControlPage.DeveloperTools.JsonCSharpConverterPage> ();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<ControlItemTemplate_>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        UnhandledException += App_UnhandledException;
    }

    private void RegisteryPages()
    {
        var ser = (PageService)App.GetService<IPageService>();
        ser.AddDependence<MainViewModel, MainPage>();
        ser.AddDependence<SettingsViewModel, SettingsPage>();
        ser.AddDependence<JsonCSharpConverterViewModel, Views.ControlPage.DeveloperTools.JsonCSharpConverterPage>();
        ser.AddDependence<NumberConverterViewModel, NumberConverterPage>();
        ser.AddDependence<DocumentOverviewViewModel, DocumentOverviewPage>();
        ser.AddDependence<OfflineToolsDocumentViewModel, Views.Document.OfflineToolsDocumentPage>();
        ser.AddDependence<OnlineToolsDocumentViewModel, Views.Document.OnlineToolsDocumentPage>();
        ser.AddDependence<OnOffLineToolsDocumentViewModel, Views.Document.OnOffLineToolsDocumentPage>();
        ser.AddDependence<PictureConverterViewModel, Views.ControlPage.DeveloperTools.PictureConverterPage>();
        ser.AddDependence<SearchResultViewViewModel, SearchResultViewPage>();
        ser.AddDependence<LoadingViewModel, LoadingPage>();
        ser.AddDependence<ExternViewViewModel, ExternViewPage>();
        ser.AddDependence<ExternDetailViewModel, Views.ExternViews.ExternDetailPage>();
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        ExternService ser = new ExternService();
        await ser.Load();

        RegisteryPages();

        AppDomain.CurrentDomain.DomainUnload += Exit;

        App.GetService<IActivationService>().ActivateAsync(args);
    }

    private async void Exit(object? sender, EventArgs e)
    {
        await App.GetService<ActivationHandler<EventArgs>>().HandleAsync(e);
    }
}
