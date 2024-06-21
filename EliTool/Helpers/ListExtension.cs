using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliTool.Helpers;
public static class MyEnumerableExtension
{
    public static TResult MergeItem<TSource, TResult>(this IEnumerable<TSource> enumerable, Action<TResult, TSource> func)
        where TResult : class, new()
    {
        TResult s = new();
        foreach(TSource item in enumerable)
        {
            func(s, item);
        }
        return s;
    }
}
