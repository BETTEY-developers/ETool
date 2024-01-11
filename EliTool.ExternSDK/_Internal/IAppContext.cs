using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExternSDK.Common;
using EliTool.ExternSDK.Common.Resource;

namespace EliTool.ExternSDK._Internal;
public interface IAppContext
{
    public void Navigate(string path);
    public ResourceBase QueryResource(UniverseUsingIdentity UUID);
}
