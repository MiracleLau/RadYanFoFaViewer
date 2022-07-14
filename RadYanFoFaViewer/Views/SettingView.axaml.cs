using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using LiteDB;
using RadYanFoFaViewer.Utils;
using RadYanFoFaViewer.ViewModels;

namespace RadYanFoFaViewer.Views;

public partial class SettingView : UserControl
{
    private SettingViewViewModel _viewModel;
    private Config _config;
    public SettingView()
    {
        InitializeComponent();
        _viewModel = new SettingViewViewModel();
        DataContext = _viewModel;
        _config = new Config();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SaveConfigButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _viewModel.IsSaveButtonEnabled = false;
        new Task(() =>
        {
            _config.SetConfig("ApiSetting", new BsonDocument
            {
                ["ApiEmail"] = _viewModel.Email,
                ["ApiKey"] = _viewModel.ApiKey
            });
            _config.SetConfig("SearchSetting", new BsonDocument
            {
                ["PerPageSize"] = _viewModel.SearchPageSize
            });
            Dispatcher.UIThread.Post(() =>
            {
                _viewModel.IsSaveButtonEnabled = true;
                new MessageBox().GetStandWindow(msg: "保存成功！").Show();
            });
        }).Start();
        
    }
}