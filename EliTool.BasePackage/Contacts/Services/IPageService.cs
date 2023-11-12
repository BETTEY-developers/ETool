using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.BasePackage.Services;
using Microsoft.UI.Xaml.Controls;

namespace EliTool.BasePackage.Contacts.Services;
public interface IPageService
{
    public Type GetPageType(string key);
}
