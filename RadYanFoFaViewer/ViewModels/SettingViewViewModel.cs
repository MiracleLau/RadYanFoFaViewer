using System.Threading.Tasks;
using Avalonia.Threading;
using LiteDB;
using RadYanFoFaViewer.Utils;
using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;

public class SettingViewViewModel : ViewModelBase
{
    private string? _apiKey;
    private string? _email;

    private bool _isAutoCheckUpdate;

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
                ["PerPageSize"] = 100
            });
            var updateSetting = Config.GetOrDefaultConfig("UpdateSetting", new BsonDocument
            {
                ["AutoCheckUpdate"] = true
            });
            Dispatcher.UIThread.Post(() =>
            {
                Email = apiSetting["ApiEmail"].AsString;
                ApiKey = apiSetting["ApiKey"].AsString;
                SearchPageSize = searchSetting["PerPageSize"].AsInt32;
                IsAutoCheckUpdate = updateSetting["AutoCheckUpdate"].AsBoolean;
            });
        }).Start();
    }

    public string Email
    {
        get => _email ?? "";
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    public string ApiKey
    {
        get => _apiKey ?? "";
        set => this.RaiseAndSetIfChanged(ref _apiKey, value);
    }

    public int SearchPageSize
    {
        get => _searchPageSize == 0 ? 100 : _searchPageSize;
        set => this.RaiseAndSetIfChanged(ref _searchPageSize, value);
    }

    public bool IsSaveButtonEnabled
    {
        get => _isSaveButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isSaveButtonEnabled, value);
    }

    public bool IsAutoCheckUpdate
    {
        get => _isAutoCheckUpdate;
        set => this.RaiseAndSetIfChanged(ref _isAutoCheckUpdate, value);
    }
}