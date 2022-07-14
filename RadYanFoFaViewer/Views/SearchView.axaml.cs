using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using LiteDB;
using RadYanFoFaDotNet;
using RadYanFoFaViewer.Utils;
using RadYanFoFaViewer.ViewModels;

namespace RadYanFoFaViewer.Views;

public partial class SearchView : UserControl
{
    // Todo: 解决配置无法即时生效的问题
    private SearchViewViewModel _searchViewViewModel;
    private FoFaClient _foFaClient;
    private Config _config;
    public SearchView()
    {
        InitializeComponent();
        _searchViewViewModel = new SearchViewViewModel();
        DataContext = _searchViewViewModel;
        _config = new Config();
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
            var apiSetting = (BsonDocument?) _config.GetConfig("ApiSetting");
            var apiEmail = "";
            var apiKey = "";
            var pageSize = 20;
            if (apiSetting is not null)
            {
                apiEmail = apiSetting["ApiEmail"].AsString;
                apiKey = apiSetting["ApiKey"].AsString;
            }

            var searchSetting = _config.GetConfig("SearchSetting");
            if (searchSetting is not null)
            {
                Console.WriteLine(searchSetting["PerPageSize"].AsInt32);
                pageSize = searchSetting["PerPageSize"].AsInt32;
            }
            _foFaClient = new FoFaClient(apiEmail, apiKey);
            _foFaClient.SetGetFields(_searchViewViewModel.ReturnFields);
            new Task(() =>
            {
                var result = _foFaClient.Search(_searchViewViewModel.SearchString,size:pageSize,isFullData:!_searchViewViewModel.IsNotFullData);
                if (result is {Error: false})
                {
                    if (result.Results != null)
                    {
                        var results = result.Results;
                        Dispatcher.UIThread.Post(() =>
                        {
                            var num = 0;
                            foreach (var r in results)
                            {
                                num++;
                                _searchViewViewModel.SearchResults.Add(new()
                                {
                                    Id = num,
                                    Host = r[0],
                                    IP = r[1],
                                    Protocol = r[2],
                                    Domain = r[3],
                                    Port = r[4],
                                    Title = Regex.Unescape(r[5]),
                                    ICP = Regex.Unescape(r[6]),
                                    Server = r[7],
                                    OS = r[8],
                                    CountryName = r[9]
                                });
                            }
                        });
                    }
                }
                else
                {
                    Dispatcher.UIThread.Post(() =>
                        new MessageBox().GetStandWindow("无法获取数据", "无法获取数据，可能是搜索语句不正确或者Api Key未正确设置。").Show()
                    );
                }

                Dispatcher.UIThread.Post(() =>
                {
                    _searchViewViewModel.IsLoading = false;
                    _searchViewViewModel.IsSearchButtonEnabled = true;
                });
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