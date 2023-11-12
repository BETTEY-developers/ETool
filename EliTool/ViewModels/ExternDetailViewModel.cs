using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Models;

namespace EliTool.ViewModels;

public partial class ExternDetailViewModel : ObservableRecipient
{
    public ExternDetailViewModel()
    {
    }

    [ObservableProperty]
    ExternInfo displayExtern = new ExternInfo();
}
