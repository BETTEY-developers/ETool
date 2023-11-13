using System.Collections;
using System.Collections.Generic;
using ETool.ExternSDK.Model;

namespace EliTool.ExternSDK;

public class SettingCollection : ICollection<SettingItem>, IList<SettingItem>
{
    private List<SettingItem> _list = new();

    public SettingItem this[int index] { get => _list[index]; set => _list[index] = value; }

    public int Count => _list.Count;

    public bool IsReadOnly => false;

    public void Add(SettingItem item) => _list.Add(item);

    public void Clear() => _list.Clear();

    public bool Contains(SettingItem item) => _list.Contains(item);

    public void CopyTo(SettingItem[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

    public IEnumerator<SettingItem> GetEnumerator() => _list.GetEnumerator();

    public int IndexOf(SettingItem item) => _list.IndexOf(item);

    public void Insert(int index, SettingItem item) => _list.Insert(index, item);

    public bool Remove(SettingItem item) => _list.Remove(item);

    public void RemoveAt(int index) => _list.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
}
