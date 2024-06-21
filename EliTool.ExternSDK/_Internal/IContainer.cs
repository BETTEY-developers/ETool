using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExtensionSDK.Common;
using EliTool.ExtensionSDK.Common.Resource;

namespace EliTool.ExtensionSDK._Internal;
public interface IContainer
{
    public void Navigate(string path);
    public ResourceBase QueryResource(UniverseUsingIdentity UUID);
}
