using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.VisualTree;
using RadYanFoFaViewer.Utils;
using RadYanFoFaViewer.ViewModels;

namespace RadYanFoFaViewer.Views;

public partial class SearchView : UserControl
{
    private readonly SearchViewViewModel _searchViewViewModel;

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
        _searchViewViewModel.TotalPage = 0;
        _searchViewViewModel.TotalSize = 0;
        _searchViewViewModel.NowPage = 1;
        _searchViewViewModel.SearchPage = 1;
        _searchViewViewModel.TotalPageString = "";
        _searchViewViewModel.IsNotFirstPage = false;
        _searchViewViewModel.IsNotLastPage = false;
    }

    private void SearchButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _searchViewViewModel.IsLoading = true;
        _searchViewViewModel.IsSearchButtonEnabled = false;
        _searchViewViewModel.SearchResults.Clear();
        new Task(() =>
        {
            try
            {
                var results = Search.DoSearch(
                    _searchViewViewModel.SearchString,
                    _searchViewViewModel.ReturnFields,
                    _searchViewViewModel.SearchPage,
                    _searchViewViewModel.IsNotFullData);
                if (results is not null)
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        _searchViewViewModel.IsLoading = false;
                        _searchViewViewModel.IsSearchButtonEnabled = true;
                        _searchViewViewModel.NowPage = results.Page;
                        _searchViewViewModel.TotalSize = results.Size;
                        _searchViewViewModel.TotalPage = results.TotalPage;
                        if (results.Page == results.TotalPage)
                        {
                            _searchViewViewModel.IsNotFirstPage = true;
                            _searchViewViewModel.IsNotLastPage = false;
                        }
                        else if (results.Page == 1)
                        {
                            _searchViewViewModel.IsNotLastPage = true;
                            _searchViewViewModel.IsNotFirstPage = false;
                        }
                        else
                        {
                            _searchViewViewModel.IsNotFirstPage = true;
                            _searchViewViewModel.IsNotLastPage = true;
                        }

                        _searchViewViewModel.TotalPageString =
                            $"共{results.Size}条数据，第{results.Page}/{results.TotalPage}页";
                        results.Results?.ForEach(x => _searchViewViewModel.SearchResults.Add(x));
                    });
                }
                else
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        _searchViewViewModel.IsLoading = false;
                        _searchViewViewModel.IsSearchButtonEnabled = true;
                    });
                }
            }
            catch (Exception ex)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    _searchViewViewModel.IsLoading = false;
                    _searchViewViewModel.IsSearchButtonEnabled = true;
                    Utils.MessageBox.NormalMsgBox("发生错误", ex.Message).Show();
                });
            }
        }).Start();
    }

    private void SearchStringTextBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        _searchViewViewModel.IsSearchButtonEnabled = !string.IsNullOrEmpty(_searchViewViewModel.SearchString);
    }

    private void FirstPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _searchViewViewModel.SearchPage = 1;
        SearchButton_OnClick(sender, e);
    }

    private void PrePageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _searchViewViewModel.SearchPage = _searchViewViewModel.NowPage - 1;
        SearchButton_OnClick(sender, e);
    }

    private void NextPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _searchViewViewModel.SearchPage = _searchViewViewModel.NowPage + 1;
        SearchButton_OnClick(sender, e);
    }

    private void LastPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _searchViewViewModel.SearchPage = _searchViewViewModel.TotalPage;
        SearchButton_OnClick(sender, e);
    }
}