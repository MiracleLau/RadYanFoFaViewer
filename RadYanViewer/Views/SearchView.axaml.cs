using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RadYanFoFaDotNet;
using RadYanViewer.ViewModels;

namespace RadYanViewer.Views;

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
        _searchViewViewModel.SearchResults.Clear();
        _searchViewViewModel.SearchString = "";
    }

    private void SearchButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var client = new FoFaClient("","");
        client.SetGetFields(_searchViewViewModel.ReturnFields);

        var result = client.Search(_searchViewViewModel.SearchString);
        if (result is {Error: false})
        {
            if (result.Results != null)
            {
                var results = result.Results;
                for (var i = 0; i < results.Count; i++)
                {
                    _searchViewViewModel.SearchResults.Add(new ()
                    {
                        Host = results[i][0],
                        IP = results[i][1],
                        Protocol = results[i][2],
                        Domain = results[i][3],
                        Port = results[i][4],
                        Title = Regex.Unescape(results[i][5]),
                        ICP = Regex.Unescape(results[i][6]),
                        Server = results[i][7],
                        OS =results[i][8],
                        CountryName = results[i][9]
                    });
                }
        
            }
        }
        else
        {
            new MessageBox().GetStandWindow("警告","发生错误").Show();
        }
    }
}