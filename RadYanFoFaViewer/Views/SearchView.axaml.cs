using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using DynamicData;
using LiteDB;
using RadYanFoFaDotNet;
using RadYanFoFaViewer.Utils;
using RadYanFoFaViewer.ViewModels;

namespace RadYanFoFaViewer.Views;

public partial class SearchView : UserControl
{
    private SearchViewViewModel _searchViewViewModel;
    public SearchView()
    {
        InitializeComponent();
        _searchViewViewModel = new SearchViewViewModel();
        DataContext = _searchViewViewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ResetButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _searchViewViewModel.IsSearchButtonEnabled = false;
        _searchViewViewModel.SearchResults.Clear();
        _searchViewViewModel.SearchString = "";
    }

    private void SearchButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _searchViewViewModel.IsLoading = true;
        _searchViewViewModel.IsSearchButtonEnabled = false;
        _searchViewViewModel.SearchResults.Clear();
        try
        {
            new Task(() =>
            {
                var results = Search.DoSearch(
                    _searchViewViewModel.SearchString, 
                    _searchViewViewModel.ReturnFields,
                    1,
                    _searchViewViewModel.IsNotFullData);
                if (results is not null)
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        _searchViewViewModel.IsLoading = false;
                        _searchViewViewModel.IsSearchButtonEnabled = true;
                        results.ForEach(x=> _searchViewViewModel.SearchResults.Add(x));
                    });
                }
            }).Start();
        }
        catch (Exception ex)
        {
            _searchViewViewModel.IsLoading = false;
            _searchViewViewModel.IsSearchButtonEnabled = true;
            new MessageBox().GetStandWindow("警告", "发生错误：" + ex.Message).Show();
        }
    }

    private void SearchStringTextBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        _searchViewViewModel.IsSearchButtonEnabled = !string.IsNullOrEmpty(_searchViewViewModel.SearchString);
    }
}