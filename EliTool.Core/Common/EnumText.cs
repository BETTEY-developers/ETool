using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliTool.Core.Common;
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class EnumText : Attribute
{
    public string CultureString { get; set; }

    public string TextString { get; set; }
}
