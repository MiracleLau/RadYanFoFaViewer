using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using LiteDB;
using RadYanFoFaViewer.Utils;

namespace RadYanFoFaViewer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AvaloniaObject_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property.Name == "ClientSize")
        {
            var height = Height - 180;
            var searchViewControl = this.GetControl<TabItem>("SearchViewTabItem").GetControl<SearchView>("SearchView")
                .GetControl<DataGrid>("SearchResultDataGrid");
            if (searchViewControl is not null) searchViewControl.Height = height;
        }
    }

    private void StyledElement_OnInitialized(object? sender, EventArgs e)
    {
        new Task(() =>
        {
            try
            {
                var updateSetting = Config.GetOrDefaultConfig("UpdateSetting", new BsonDocument
                {
                    ["AutoCheckUpdate"] = true
                });
                if (!updateSetting["AutoCheckUpdate"].AsBoolean) return;
                var updateInfo = Update.AutoCheckUpdate(true);
                if (updateInfo is null) return;
                if (updateInfo.IsNewVersion)
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        var updateWindow = new UpdateWindow();
                        updateWindow.SetUpdateInfo(updateInfo);
                        updateWindow.Topmost = true;
                        updateWindow.Show();
                    });
                }
            }
            catch (Exception ex)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    Utils.MessageBox.NormalMsgBox(msg: ex.Message);
                });
            }
        }).Start();
    }
}