using System;
using System.Collections.ObjectModel;
using RadYanViewer.Models;
using ReactiveUI;

namespace RadYanViewer.ViewModels;

public class SearchViewViewModel: ViewModelBase
{
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

    public ObservableCollection<SearchResult> SearchResults { get; }

    public SearchViewViewModel()
    {
        _isNotFullData = true;
        SearchResults = new ObservableCollection<SearchResult>();
    }
    
    public void SearchButton_Clicked()
    {
        SearchResults.Add(new SearchResult
        {
            Name = "小燕燕"
        });
    }

    public void ResetButton_Clicked()
    {
        SearchResults.Clear();
        SearchString = "";
    }
}