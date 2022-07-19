using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RadYanFoFaViewer.Utils;
using RadYanFoFaViewer.ViewModels;

namespace RadYanFoFaViewer.Views;

public partial class AboutView : UserControl
{
    private readonly AboutViewViewModel _viewModel;

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
        Update.AutoCheckUpdate(
            () => _viewModel.IsCheckButtonEnable = true,
            () => _viewModel.IsCheckButtonEnable = true
        );
    }
}