using System;
using Avalonia;
using Avalonia.Controls;
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
    private void WindowBase_OnActivated(object? sender, EventArgs e)
    {
        Update.AutoCheckUpdate(() => { }, () => { }, true);
    }
}