using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RadYanFoFaViewer.Models;
using RadYanFoFaViewer.Utils;
using RadYanFoFaViewer.ViewModels;

namespace RadYanFoFaViewer.Views;

public partial class UpdateWindow : Window
{
    private readonly string _releasesUrl = "https://github.com/MiracleLau/RadYanFoFaViewer/releases";
    private readonly UpdateWindowViewModel _viewModel;

    public UpdateWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        _viewModel = new UpdateWindowViewModel();
        DataContext = _viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void SetUpdateInfo(UpdateInfo updateInfo)
    {
        _viewModel.Version = $"版本：{updateInfo.Version ?? "未知"}";
        _viewModel.UpdateContent = updateInfo.UpdateContent ?? "无";
    }

    private void GoToDownloadPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        OpenBrowser.Open(_releasesUrl);
        Close();
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}