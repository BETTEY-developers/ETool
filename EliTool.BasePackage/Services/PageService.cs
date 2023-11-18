using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.BasePackage.Contracts.Services;
using Microsoft.UI.Xaml.Controls;
using Windows.Gaming.Input;

namespace EliTool.BasePackage.Services;

public class PageService : ItemMap<ObservableRecipient, Page>, IPageService
{
    public void AddDependence<TKey, TValue>()
        where TKey : ObservableRecipient
        where TValue : Page
        => Configure<TKey, TValue>(t => t.FullName!);
    public void AddDependence(Type ViewModel, Type Page)
    {
        Configure(ViewModel, Page, t => t.FullName!);
    }

    public Type GetPageType(string key) => GetDependenceType(key);
}
