using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExtensionSDK._Internal;

namespace EliTool.ExtensionSDK.Model.Tool;
public class ToolGroupCollection : CollectionAbsImpl<ToolGroup>
{
    public ToolGroupCollection(CollectionAbsImpl<ToolGroup> coll) : base(coll) { }
    public ToolGroupCollection() : base()
    {
    }
}
