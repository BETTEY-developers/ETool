using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExternSDK._Internal;
using EliTool.ExternSDK.Common.Resource;

namespace EliTool.ExternSDK.Common;

internal class _InternalSystem
{
    internal class _InternalSystem_Factory
    {
        public static _InternalSystem Get_InternalSystem(IAppContext appContext)
        {
            return new(UniverseUsingIdentity.GeneraterUUID(), appContext);
        }
    }

    public UniverseUsingIdentity SystemUniverseUsingIdentity { get; set; }

    private IAppContext AppContext
    {
        get; set; 
    }

    Dictionary<UniverseUsingIdentity, ResourceBase> m_resourceMapping = new();

    internal void RegisterResource(ResourceBase resource)
    {
        resource.SetUUID(UniverseUsingIdentity.GeneraterUUID());

        m_resourceMapping.Add(resource.UniverseUsingIdentity, resource);
    }

    internal void UnregisterResource(ResourceBase resource)
    {
        m_resourceMapping.Remove(resource.UniverseUsingIdentity);

        resource.UniverseUsingIdentity.SetUnused();
        resource.SetUnused();
    }

    internal ResourceBase? QueryResourceLocal(UniverseUsingIdentity universeUsingIdentity)
    {
        if (m_resourceMapping.TryGetValue(universeUsingIdentity, out ResourceBase resource))
        {
            return resource;
        }
        return null;
    }

    internal ResourceBase? QureyResourceGlobal(UniverseUsingIdentity universeUsingIdentity)
    {
        return AppContext.QueryResource(universeUsingIdentity);
    }

    private _InternalSystem(UniverseUsingIdentity universeUsingIdentity, IAppContext appContext)
    {
        SystemUniverseUsingIdentity = universeUsingIdentity;
        AppContext = appContext;
    }
}


