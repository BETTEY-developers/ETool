using EliTool.Contracts.Services;
using EliTool.Services;
using EliTool.ViewModels;

using Microsoft.UI.Xaml;

namespace EliTool.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;

    public DefaultActivationHandler(INavigationService navigationService)
    {
        _navigationService = navigationService;

    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame?.Content == null;
    }

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _navigationService.NavigateTo(typeof(MainViewModel).FullName!, args.Arguments);

        var ser = (ExternService)App.GetService<IExternService>();
        await ser.Load();

        await Task.CompletedTask;
    }
}