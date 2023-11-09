using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExternSDK;
using EliTool.Models;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace EliTool.Contracts.Services;

public interface IExternService
{
    public Task Unload();

    public Task Load();

    public ExternInfo GetExtern(string name);

    public List<ExternInfo> Externs
    {
        get; set;
    }
    StorageFolder ApplicationExternFolder { set; get; }
    StorageFolder ApplicationExternUnpackageFolder { set; get; }
    StorageFile ExternManifest { set; get; }
}
