using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.BasePackage.Contacts.Services;
using Microsoft.UI.Xaml.Controls;
using Windows.Gaming.Input;

namespace EliTool.BasePackage.Services;

public class PageService : ItemMap<ObservableObject, Page>, IPageService
{
    public void AddDependence<TKey, TValue>()
        where TKey : ObservableObject
        where TValue : Page
        => Configure<TKey, TValue>(t => t.FullName!);

    public Type GetPageType(string key) => GetDependenceType(key);
}
