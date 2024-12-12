using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EliTool.Controls.Primitives;
public sealed partial class FormatTextBoxViewModel : ObservableRecipient
{
    [ObservableProperty]
    private ObservableCollection<ItemsPresenter> _displayItems = new();
}
