using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using LiteDB;
using RadYanFoFaViewer.Utils;
using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;


public class SettingViewViewModel: ViewModelBase
{
    private Config _config;
    private string _email;

    public string Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }
    
    private string _apiKey;

    public string ApiKey
    {
        get => _apiKey;
        set => this.RaiseAndSetIfChanged(ref _apiKey, value);
    }

    private int _searchPageSize;

    public int SearchPageSize
    {
        get => _searchPageSize;
        set => this.RaiseAndSetIfChanged(ref _searchPageSize, value);
    }

    private bool _isSaveButtonEnabled = true;

    public bool IsSaveButtonEnabled
    {
        get => _isSaveButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isSaveButtonEnabled, value);
    }

    public SettingViewViewModel()
    {
        _config = new Config();
        new Task(() =>
        {
            var apiSetting = _config.GetOrDefaultConfig("ApiSetting", new BsonDocument
            {
                ["ApiEmail"] = "",
                ["ApiKey"] = "",
            });
            var searchSetting = _config.GetOrDefaultConfig("SearchSetting", new BsonDocument
            {
                ["PerPageSize"] = 20
            });
            Dispatcher.UIThread.Post(() =>
            {
                Email = apiSetting["ApiEmail"].AsString;
                ApiKey = apiSetting["ApiKey"].AsString;
                SearchPageSize = searchSetting["PerPageSize"].AsInt32;
                Console.WriteLine(SearchPageSize);
            });
        }).Start();
    }
    
}