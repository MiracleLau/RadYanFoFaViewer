using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RadYanViewer.ViewModels;

namespace RadYanViewer.Views;

public partial class SearchView : UserControl
{
    public SearchView()
    {
        InitializeComponent();
        DataContext = new SearchViewViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}