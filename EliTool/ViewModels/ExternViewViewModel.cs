using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Contracts.Services;
using EliTool.Models;

namespace EliTool.ViewModels;

public partial class ExternViewViewModel : ObservableRecipient
{
    private ObservableCollection<Extern> externInfos = new ObservableCollection<Extern>();

    public ObservableCollection<Extern> ExternInfos
    {
        get => externInfos;
        set
        {
            externInfos = value;
            OnPropertyChanged(nameof(ExternInfos));
        }
    }

    public ExternViewViewModel()
    {
        (App.GetService<IExternService>().Externs??new List<Extern>()).ForEach(ExternInfos.Add);
    }
}
