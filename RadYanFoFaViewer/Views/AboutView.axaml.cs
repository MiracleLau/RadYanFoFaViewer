using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using RadYanFoFaViewer.Utils;
using RadYanFoFaViewer.ViewModels;

namespace RadYanFoFaViewer.Views;

public partial class AboutView : UserControl
{
    private AboutViewViewModel _viewModel;
    public AboutView()
    {
        InitializeComponent();
        _viewModel = new AboutViewViewModel
        {
            IsCheckButtonEnable = true
        };
        DataContext = _viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void CheckUpdateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _viewModel.IsCheckButtonEnable = false;
        new Task(() =>
        {
            try
            {
                var updateInfo = Update.AutoCheckUpdate();
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
                Dispatcher.UIThread.Post(() =>
                {
                    _viewModel.IsCheckButtonEnable = true;
                });
            }catch(Exception ex)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    Utils.MessageBox.NormalMsgBox(msg: ex.Message);
                    _viewModel.IsCheckButtonEnable = true;
                });
            }
        }).Start();
    }
}