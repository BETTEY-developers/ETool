using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.ExternSDK.Common.Resource;

namespace EliTool.ExternSDK.Common;

internal class _InternalSystem
{
    internal class _InternalSystem_Factory
    {
        public static _InternalSystem Get_InternalSystem()
        {
            return new(UniverseUsingIdentity.GeneraterUUID());
        }
    }

    public UniverseUsingIdentity SystemUniverseUsingIdentity { get; set; }

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

    private _InternalSystem(UniverseUsingIdentity universeUsingIdentity)
    {
        SystemUniverseUsingIdentity = universeUsingIdentity;
    }
}


