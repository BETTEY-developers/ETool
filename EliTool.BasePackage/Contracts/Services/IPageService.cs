using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.BasePackage.Services;
using Microsoft.UI.Xaml.Controls;

namespace EliTool.BasePackage.Contracts.Services;
public interface IPageService : IItemMap<ObservableRecipient, Page>
{
    public Type GetPageType(string key);
    public void AddDependence<TKey, TValue>()
        where TKey : ObservableRecipient
        where TValue : Page;
    public void AddDependence(Type ViewModel, Type Page);
}
