using System.Collections.Generic;
using System.Collections.ObjectModel;
using RadYanFoFaViewer.Models;
using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;

public class SearchViewViewModel : ViewModelBase
{
    public readonly List<string> ReturnFields;
    
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
        SearchPage = 1;
    }
    
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }

    private string? _searchString;
    public string SearchString
    {
        get => _searchString ?? "";
        set => this.RaiseAndSetIfChanged(ref _searchString, value);
    }
    
    private bool _isNotFullData;
    public bool IsNotFullData
    {
        get => _isNotFullData;
        set => this.RaiseAndSetIfChanged(ref _isNotFullData, value);
    }
    
    private bool _isSearchButtonEnabled;
    public bool IsSearchButtonEnabled
    {
        get => _isSearchButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isSearchButtonEnabled, value);
    }

    private int _totalPage;
    public int TotalPage
    {
        get => _totalPage;
        set => this.RaiseAndSetIfChanged(ref _totalPage, value);
    }

    private string? _totalPageString;
    public string TotalPageString
    {
        get => _totalPageString ?? "";
        set => this.RaiseAndSetIfChanged(ref _totalPageString, value);
    }

    private int _totalSize;
    public int TotalSize
    {
        get => _totalSize;
        set => this.RaiseAndSetIfChanged(ref _totalSize, value);
    }

    private int _nowPage;
    public int NowPage
    {
        get => _nowPage;
        set => this.RaiseAndSetIfChanged(ref _nowPage, value);
    }

    private bool _isNotFirstPage;
    public bool IsNotFirstPage
    {
        get => _isNotFirstPage;
        set => this.RaiseAndSetIfChanged(ref _isNotFirstPage, value);
    }

    private bool _isNotLastPage;
    public bool IsNotLastPage
    {
        get => _isNotLastPage;
        set => this.RaiseAndSetIfChanged(ref _isNotLastPage, value);
    }

    private int _searchPage;
    public int SearchPage
    {
        get => _searchPage;
        set => this.RaiseAndSetIfChanged(ref _searchPage, value);
    }
}