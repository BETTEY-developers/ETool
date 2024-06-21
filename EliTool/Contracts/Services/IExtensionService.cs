using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExtensionSDK;
using EliTool.Models;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace EliTool.Contracts.Services;

public interface IExtensionService
{
    public Task Unload();

    public Task<bool> Load();

    public Extension GetExtension(string name);

    public List<Extension> Extensions
    {
        get; set;
    }
    StorageFolder ApplicationExtensionFolder { set; get; }
    StorageFolder ApplicationExtensionUnpackageFolder { set; get; }
    StorageFile ExtensionManifest { set; get; }
}
