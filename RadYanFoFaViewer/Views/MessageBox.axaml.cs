using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace RadYanFoFaViewer.Views;

public partial class MessageBox : Window
{
    public MessageBox()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    public MessageBox GetStandWindow(string title = "消息提示", string msg = "")
    {
        Title = title;
        this.FindControl<TextBlock>("MessageContent").Text = msg;
        return this;
    }
}