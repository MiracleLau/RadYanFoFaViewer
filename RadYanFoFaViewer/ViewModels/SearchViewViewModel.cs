using System.Collections.Generic;
using System.Collections.ObjectModel;
using RadYanFoFaViewer.Models;
using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;

public class SearchViewViewModel : ViewModelBase
{
    public readonly List<string> ReturnFields;
    private bool _isLoading;

    private bool _isNotFirstPage;

    private bool _isNotFullData;

    private bool _isNotLastPage;

    private bool _isSearchButtonEnabled;

    private int _nowPage;

    private int _searchPage;

    private string _searchString;

    private int _totalPage;

    private string _totalPageString;

    private int _totalSize;

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

    public bool IsLoading
    {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }

    public string SearchString
    {
        get => _searchString;
        set => this.RaiseAndSetIfChanged(ref _searchString, value);
    }

    public bool IsNotFullData
    {
        get => _isNotFullData;
        set => this.RaiseAndSetIfChanged(ref _isNotFullData, value);
    }

    public ObservableCollection<SearchResult> SearchResults { get; set; }

    public bool IsSearchButtonEnabled
    {
        get => _isSearchButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isSearchButtonEnabled, value);
    }

    public int TotalPage
    {
        get => _totalPage;
        set => this.RaiseAndSetIfChanged(ref _totalPage, value);
    }

    public string TotalPageString
    {
        get => _totalPageString;
        set => this.RaiseAndSetIfChanged(ref _totalPageString, value);
    }

    public int TotalSize
    {
        get => _totalSize;
        set => this.RaiseAndSetIfChanged(ref _totalSize, value);
    }

    public int NowPage
    {
        get => _nowPage;
        set => this.RaiseAndSetIfChanged(ref _nowPage, value);
    }

    public bool IsNotFirstPage
    {
        get => _isNotFirstPage;
        set => this.RaiseAndSetIfChanged(ref _isNotFirstPage, value);
    }

    public bool IsNotLastPage
    {
        get => _isNotLastPage;
        set => this.RaiseAndSetIfChanged(ref _isNotLastPage, value);
    }

    public int SearchPage
    {
        get => _searchPage;
        set => this.RaiseAndSetIfChanged(ref _searchPage, value);
    }
}