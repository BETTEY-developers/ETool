using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.BasePackage.Contracts.Services;

namespace EliTool.BasePackage.Services;

public class ItemMap<TKeyBase, TValueBase> : IItemMap<TKeyBase, TValueBase>
{
    private Dictionary<string, Type> _items = new();

    public ItemMap()
    {
    }

    public Type GetDependenceType(string key) => _items[key];

    public virtual void Configure<TKey, TValue>(Func<Type, string> keyselector)
        where TKey : class, TKeyBase
        where TValue : class, TValueBase
        => _items.Add(keyselector(typeof(TKey)), typeof(TValue));

    public void Configure(Type Key, Type Value, Func<Type, string> keyselector) => _items.Add(keyselector(Key), Value);
}

public class ItemMap : IItemMap
{
    private Dictionary<string, Type> _items = new();

    public ItemMap()
    {
    }

    public Type GetDependenceType(string key) => _items[key];

    public virtual void Configure<TKey, TValue>(Func<Type, string> keyselector)
        where TKey : class
        where TValue : class
        => _items.Add(keyselector(typeof(TKey)), typeof(TValue));

    public void Configure(Type Key, Type Value, Func<Type, string> keyselector) => _items.Add(keyselector(Key), Value);
}