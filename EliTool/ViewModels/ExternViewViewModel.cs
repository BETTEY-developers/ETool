using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Contracts.Services;
using EliTool.Models;

namespace EliTool.ViewModels;

public partial class ExternViewViewModel : ObservableRecipient
{
    private ObservableCollection<ExternInfo> externInfos = new ObservableCollection<ExternInfo>();

    public ObservableCollection<ExternInfo> ExternInfos
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
        App.GetService<IExternService>().Externs.ForEach(ExternInfos.Add);
    }
}
