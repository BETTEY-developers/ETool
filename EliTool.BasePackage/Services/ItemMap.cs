using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.BasePackage.Contacts.Services;

namespace EliTool.BasePackage.Services;

public class ItemMap<TKeyBase, TValueBase> : IItemMap<TKeyBase, TValueBase>
{
    private Dictionary<string, Type> _items = new();

    public ItemMap()
    {
    }

    public Type GetDependenceType(string key) => _items[key];

    public void Configure<TKey, TValue>(Func<Type, string> keyselector)
        where TKey : class, TKeyBase
        where TValue : class, TValueBase
        => _items.Add(keyselector(typeof(TKey)), typeof(TValue));
}
