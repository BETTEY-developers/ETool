using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace EliTool.ExternSDK.Common.Resource;

public enum ResourceType
{
    Binary,
    Text,
    Number
}

public class ResourceBase : IDisposable, IEquatable<ResourceBase>
{
    protected ExternBase m_parent;

    protected string m_relativePath;

    protected string m_absolutePath;

    private UniverseUsingIdentity m_universeUsingIdentity;

    private bool m_unused;

    protected string m_name;

    protected ResourceType m_resourceType;

    public ExternBase Parent => m_parent;

    public string RelativePath => m_relativePath;

    public UniverseUsingIdentity UniverseUsingIdentity => m_universeUsingIdentity;

    public string Name => m_name;

    public bool Unused => m_unused;

    protected string AbsolutePath => m_absolutePath;

    internal void SetUUID(UniverseUsingIdentity uuid) => m_universeUsingIdentity = uuid;

    internal void SetUnused() => m_unused = true;

    public void Dispose() => ExternBase.InternalSystem.RegisterResource(this);

    public bool Equals(ResourceBase other)
    {
        return AbsolutePath == other.AbsolutePath &&
               RelativePath == other.RelativePath &&
               Unused == other.Unused;
    }

    protected string ToAbsolute(string path)
    {
        return ApplicationData.Current.LocalFolder.Path + "Externs\\Unpackage\\" + m_parent.Name + "\\" + m_parent.Name + path;
    }

    protected ResourceBase()
    {
        ExternBase.InternalSystem.RegisterResource(this);
        m_parent = ExternBase.Main;
    }

    ~ResourceBase()
    {
        if (!Unused)
            ExternBase.InternalSystem.RegisterResource(this);
    }

    public ResourceBase FromUUID(UniverseUsingIdentity uuid)
    {
        return ExternBase.InternalSystem.QureyResourceGlobal(uuid);
    }
}
