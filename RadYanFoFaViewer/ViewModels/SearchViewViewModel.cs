using System.Collections.Generic;
using System.Collections.ObjectModel;
using RadYanFoFaViewer.Models;
using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;

public class SearchViewViewModel: ViewModelBase
{
    public readonly List<string> ReturnFields;
    private bool _isLoading;

    public bool IsLoading
    {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }
    
    private string _searchString;
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

    private bool _isSearchButtonEnabled;

    public bool IsSearchButtonEnabled
    {
        get => _isSearchButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isSearchButtonEnabled, value);
    }

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