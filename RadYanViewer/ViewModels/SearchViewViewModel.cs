using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RadYanViewer.Models;
using ReactiveUI;

namespace RadYanViewer.ViewModels;

public class SearchViewViewModel: ViewModelBase
{
    private string _searchString;
    public readonly List<string> ReturnFields;
    public string SearchString
    {
        get => _searchString; 
        set => this.RaiseAndSetIfChanged(ref _searchString,value);
    }

    private bool _isNotFullData;
    public bool IsNotFullData
    {
        get => _isNotFullData; 
        set => this.RaiseAndSetIfChanged(ref _isNotFullData, value);
    }

    public ObservableCollection<SearchResult> SearchResults { get; set; }

    public SearchViewViewModel()
    {
        _isNotFullData = true;
        SearchResults = new ObservableCollection<SearchResult>();
        ReturnFields = new List<string>
        {
            "host",
            "ip",
            "protocol",
            "domain",
            "port",
            "title",
            "icp",
            "server",
            "os",
            "country_name"
        };
    }
}