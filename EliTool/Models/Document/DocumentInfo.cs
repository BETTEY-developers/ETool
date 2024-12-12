using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliTool.ExtensionSDK.Model.Document;
public class DocumentInfo
{
    public string Title
    {
        get; set;
    }
    public Type Page
    {
        get; set;
    }
    public override string ToString()
    {
        return Title;
    }
}
