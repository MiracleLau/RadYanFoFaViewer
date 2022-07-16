using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using LiteDB;
using RadYanFoFaViewer.Utils;
using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;

public class SettingViewViewModel : ViewModelBase
{
    private string _apiKey;
    private string _email;

    private bool _isSaveButtonEnabled = true;

    private int _searchPageSize;

    public SettingViewViewModel()
    {
        new Task(() =>
        {
            var apiSetting = Config.GetOrDefaultConfig("ApiSetting", new BsonDocument
            {
                ["ApiEmail"] = "",
                ["ApiKey"] = ""
            });
            var searchSetting = Config.GetOrDefaultConfig("SearchSetting", new BsonDocument
            {
                ["PerPageSize"] = 20
            });
            Dispatcher.UIThread.Post(() =>
            {
                Email = apiSetting["ApiEmail"].AsString;
                ApiKey = apiSetting["ApiKey"].AsString;
                SearchPageSize = searchSetting["PerPageSize"].AsInt32;
            });
        }).Start();
    }

    public string Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    public string ApiKey
    {
        get => _apiKey;
        set => this.RaiseAndSetIfChanged(ref _apiKey, value);
    }

    public int SearchPageSize
    {
        get => _searchPageSize;
        set => this.RaiseAndSetIfChanged(ref _searchPageSize, value);
    }

    public bool IsSaveButtonEnabled
    {
        get => _isSaveButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isSaveButtonEnabled, value);
    }
}