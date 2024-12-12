using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EliTool.ExtensionSDK._Internal;

public class CollectionAbsImpl<T> : ICollection<T>, IList<T>
{
    public CollectionAbsImpl(CollectionAbsImpl<T> coll)
    {
        _list = coll;
    }

    public CollectionAbsImpl()
    {
    }

    protected List<T> _list = new();

    public T this[int index] { get => _list[index]; set => _list[index] = value; }

    public int Count => _list.Count;

    public bool IsReadOnly => false;

    public void Add(T item) => _list.Add(item);

    public void Clear() => _list.Clear();

    public bool Contains(T item) => _list.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

    public int IndexOf(T item) => _list.IndexOf(item);

    public void Insert(int index, T item) => _list.Insert(index, item);

    public bool Remove(T item) => _list.Remove(item);

    public void RemoveAt(int index) => _list.RemoveAt(index);

    public void AddRange(CollectionAbsImpl<T> collection)
    {
        _list.AddRange(collection._list);
    }

    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

    public static implicit operator CollectionAbsImpl<T>(T[] array)
        => new()
        {
            _list = new(array)
        };
    public static implicit operator CollectionAbsImpl<T>(List<T> list)
        => new()
        {
            _list = new(list)
        };
    
    public static implicit operator T[](CollectionAbsImpl<T> collection) =>
        collection.ToArray();

    public static implicit operator List<T>(CollectionAbsImpl<T> collection) =>
        collection._list;

    public static CollectionAbsImpl<T> operator |(CollectionAbsImpl<T> left, CollectionAbsImpl<T> right)
    {
        CollectionAbsImpl<T> r = new();
        r.AddRange(left);
        r.AddRange(right);
        return r;
    }
}
