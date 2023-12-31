﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.Contracts.Services;
using EliTool.Services;
using Microsoft.UI.Xaml;
using Windows.UI.Core.Preview;
using Windows.UI.WindowManagement;

namespace EliTool.Activation;
internal class CloseActivationHandler : ActivationHandler<EventArgs>
{
    protected async override Task HandleInternalAsync(EventArgs args)
    {
        var externservice = App.GetService<IExternService>();
        
        await externservice.Unload();
    }
}
