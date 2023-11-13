using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliTool.BasePackage.Services;
using Microsoft.UI.Xaml.Automation.Provider;

namespace SimpleExtern.Common;

public class ServiceRegister : ItemMap
{
    private readonly Dictionary<Type, object> _serviceLongLifetime = new();

    public void Configure<TKey, TValue>()
        where TKey : class
        where TValue : class
        => base.Configure<TKey, TValue>(x => x.FullName!);

    public TKey GetServiceToObject<TKey>()
    {
        var t = GetDependenceType(typeof(TKey).FullName!);
        return (TKey)(!_serviceLongLifetime.ContainsKey(t)? 
            _serviceLongLifetime[t] = (TKey)Activator.CreateInstance(t) : 
            _serviceLongLifetime[t]);
    }
}
