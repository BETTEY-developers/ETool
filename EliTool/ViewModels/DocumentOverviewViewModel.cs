using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EliTool.Contracts.Services;
using EliTool.Views;
using EliTool.Views.Document;

namespace EliTool.ViewModels;

public enum Documents
{
    OfflineTool,
    PartialOfflineTool,
    OnlineTool
}

public partial class DocumentOverviewViewModel : ObservableRecipient
{
    private Dictionary<Documents, Type> DocumentTypes = new()
    {
        [Documents.OfflineTool] = typeof(OfflineToolsDocumentPage),
        [Documents.PartialOfflineTool] = typeof(OnOffLineToolsDocumentPage),
        [Documents.OnlineTool] = typeof(OnlineToolsDocumentPage)
    };
    private Dictionary<Documents, Type> DocumentViewModelTypes = new()
    {
        [Documents.OfflineTool] = typeof(OfflineToolsDocumentViewModel),
        [Documents.PartialOfflineTool] = typeof(OnOffLineToolsDocumentViewModel),
        [Documents.OnlineTool] = typeof(OnlineToolsDocumentViewModel)
    };

    public DocumentOverviewViewModel()
    {
    }

    [RelayCommand]
    public void NavigationToDocument(int type)
    {
        App.GetService<INavigationService>().NavigateTo(DocumentViewModelTypes[(Documents)type].FullName);
    }
}
