using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Contracts.Services;
using EliTool.Models;

namespace EliTool.ViewModels;

public partial class ExternViewViewModel : ObservableRecipient
{
    private ObservableCollection<ExternManifestInfo> externInfos = new ObservableCollection<ExternManifestInfo>();

    public ObservableCollection<ExternManifestInfo> ExternInfos
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
        App.GetService<IExternService>().Externs.ForEach(e =>
        {
            externInfos.Add(e.Manifest);
        });
    }
}
