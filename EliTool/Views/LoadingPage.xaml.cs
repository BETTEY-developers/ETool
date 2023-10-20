using EliTool.Contracts.Services;
using EliTool.Services;
using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace EliTool.Views;

public sealed partial class LoadingPage : Page
{
    public LoadingViewModel ViewModel
    {
        get;
    }

    public LoadingPage()
    {
        ViewModel = App.GetService<LoadingViewModel>();
        InitializeComponent();
    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var timer = DispatcherQueue.CreateTimer();
        timer.Interval = new(0,0,1);
        timer.Tick += (sender, e) =>
        {
            (App.MainWindow.Content as Frame).GoBack();
            App.GetService<INavigationService>().NavigateTo(typeof(MainViewModel).FullName!);
            timer.Stop();
        };
        timer.Start();
    }
}
