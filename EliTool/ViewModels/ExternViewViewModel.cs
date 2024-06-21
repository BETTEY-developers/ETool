using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Contracts.Services;
using EliTool.Models;

namespace EliTool.ViewModels;

public partial class ExternViewViewModel : ObservableRecipient
{
    private ObservableCollection<Extension> externInfos = new ObservableCollection<Extension>();

    public ObservableCollection<Extension> ExternInfos
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
        (App.GetService<IExtensionService>().Extensions??new List<Extension>()).ForEach(ExternInfos.Add);
    }
}
