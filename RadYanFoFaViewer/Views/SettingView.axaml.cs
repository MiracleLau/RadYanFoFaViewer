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
    private readonly SettingViewViewModel _settingViewViewModel;

    public SettingView()
    {
        InitializeComponent();
        _settingViewViewModel = new SettingViewViewModel();
        DataContext = _settingViewViewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SaveConfigButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _settingViewViewModel.IsSaveButtonEnabled = false;
        new Task(() =>
        {
            try
            {
                Config.SetConfig("ApiSetting", new BsonDocument
                {
                    ["ApiEmail"] = _settingViewViewModel.Email,
                    ["ApiKey"] = _settingViewViewModel.ApiKey
                });
                Config.SetConfig("SearchSetting", new BsonDocument
                {
                    ["PerPageSize"] = _settingViewViewModel.SearchPageSize
                });
                Config.SetConfig("UpdateSetting", new BsonDocument
                {
                    ["AutoCheckUpdate"] = _settingViewViewModel.IsAutoCheckUpdate
                });
                Dispatcher.UIThread.Post(() =>
                {
                    _settingViewViewModel.IsSaveButtonEnabled = true;
                    Utils.MessageBox.NormalMsgBox(msg: "保存成功！").Show();
                });
            }
            catch (Exception ex)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    _settingViewViewModel.IsSaveButtonEnabled = true;
                    Utils.MessageBox.NormalMsgBox(msg: $"保存失败：{ex.Message}").Show();
                });
            }
        }).Start();
    }
}