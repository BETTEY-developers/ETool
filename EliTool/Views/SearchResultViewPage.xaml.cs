﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using EliTool.Contracts.Services;
using EliTool.Helpers;
using EliTool.Models;
using EliTool.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace EliTool.Views;

public class SearchItem : PageInfoDataItem
{
    public string Title
    {
        get; set;
    }
    public string Subtitle
    {
        get; set;
    }
    public ExternSDK.Media.Image Image
    {
        get; set;
    }
    public Type ClickType
    {
        get; set;
    }
    public Type PageType
    {
        get; set;
    }

    public static SearchItem FromInfoData(PageInfoDataItem controlInfoDataItem) => new()
    {
        Title = controlInfoDataItem.Title,
        Image = controlInfoDataItem.Image,
        ClickType = controlInfoDataItem.PageViewModel,
        PageType = controlInfoDataItem.Page,
        Subtitle = controlInfoDataItem.Subtitle
    };
}

public class SearchGroup
{
    public ObservableCollection<SearchItem> Items { get; set; } = new ObservableCollection<SearchItem>();

    public string Title { get; set; }

    public string Id { get; set; }

    public void Add(SearchItem item) => Items.Add(item);
    public void AddRange(SearchItem[] items) => items.ToList().ForEach(x => Items.Add(x));

    public ObservableCollection<SearchItem> Geter() => Items;
}

public sealed partial class SearchResultViewPage : Page
{
    public SearchResultViewViewModel ViewModel
    {
        get;
    }

    public static SearchResultViewPage Instance
    {
        private set;
        get;
    }

    public SearchResultViewPage()
    {
        ViewModel = App.GetService<SearchResultViewViewModel>();
        
        InitializeComponent();
        Instance = this;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ShellPage.Instance.NavigationViewControl.Header = null;
        ViewModel.Groups = (ObservableCollection<SearchGroup>)e.Parameter;
        base.OnNavigatedTo(e);
    }

    public ObservableCollection<SearchItem> GetControlInfo(string id)
    {
        ObservableCollection < SearchItem > result = new();
        if (id == "all")
        {
            ViewModel.Groups.ToList().ForEach(x => 
                x.Geter().ToList().ForEach(ix => result.Add(ix)));
        }
        else
        {
            foreach(var group in ViewModel.Groups)
            {
                if (group.Id == id)
                    group.Geter().ToList().ForEach(result.Add);
            }
        }
        return result;
    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        App.GetService<INavigationService>().NavigateTo((e.ClickedItem as SearchItem).ClickType);
    }

    private void GridViewControl_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        try
        {
            (GridViewControl as GridView).ItemsSource = GetControlInfo((NavigationControl.SelectedItem as SearchGroup).Id);
        }
        catch { }
    }

    public void ReAddAllItem()
    {
        if (ViewModel.Groups.Count > 0 && ViewModel.Groups[0].Id != "all")
        {
            ViewModel.Groups.Insert(0, new()
            {
                Id = "all",
                Title = "All".GetLocalized("SearchResultViewPage")
            });
        }
    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ReAddAllItem();
    }
}
