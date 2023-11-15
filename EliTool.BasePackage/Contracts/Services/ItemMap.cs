using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliTool.BasePackage.Contracts.Services;

public interface IItemMap<TKeyBase, TValueBase>
{
    public void Configure<TKey, TValue>(Func<Type, string> keyselector)
        where TKey : class, TKeyBase
        where TValue : class, TValueBase;
    public void Configure(Type Key, Type Value, Func<Type, string> keyselector);

    public Type GetDependenceType(string key);
}

public interface IItemMap
{
    public void Configure<TKey, TValue>(Func<Type, string> keyselector)
        where TKey : class
        where TValue : class;

    public void Configure(Type Key, Type Value, Func<Type, string> keyselector);

    public Type GetDependenceType(string key);
}
