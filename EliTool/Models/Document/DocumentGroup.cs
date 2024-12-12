using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExtensionSDK.Model;
using Microsoft.UI.Xaml.Controls;

namespace EliTool.ExtensionSDK.Model.Document;
public class DocumentGroup
{
    public string Title
    {
        get; set;
    }
    public Symbol HeaderSymbol
    {
        get; set;
    }
    public string Id
    {
        get; set;
    }
    public List<DocumentInfo> DocumentInfos
    {
        get; set;
    }
}
