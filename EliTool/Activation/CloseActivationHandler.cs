using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.Contracts.Services;
using EliTool.Services;
using Windows.UI.WindowManagement;

namespace EliTool.Activation;
internal class CloseActivationHandler : ActivationHandler<AppWindowClosedEventArgs>
{
    protected async override Task HandleInternalAsync(AppWindowClosedEventArgs args)
    {
        var externservice = App.GetService<IExternService>();
        
        await externservice.Unload();
    }
}
