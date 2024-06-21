using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExtensionSDK._Internal;
using EliTool.ExtensionSDK.Common.Resource;

namespace EliTool.ExtensionSDK.Common;

internal class _InternalSystem
{
    internal class _InternalSystem_Factory
    {
        public static _InternalSystem Get_InternalSystem(IContainer container)
        {
            return new(UniverseUsingIdentity.GeneraterUUID(), container);
        }
    }

    public UniverseUsingIdentity SystemUniverseUsingIdentity { get; set; }

    private IContainer Container
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
        return Container.QueryResource(universeUsingIdentity);
    }

    internal void Navigate(string path) => Container.Navigate(path);

    private _InternalSystem(UniverseUsingIdentity universeUsingIdentity, IContainer container)
    {
        SystemUniverseUsingIdentity = universeUsingIdentity;
        Container = container;
    }
}


