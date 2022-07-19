using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;

public class UpdateWindowViewModel: ViewModelBase
{
    private string? _version;

    public string Version
    {
        get => _version ?? "";
        set => this.RaiseAndSetIfChanged(ref _version, value);
    }
    
    private string? _updateContent;

    public string UpdateContent
    {
        get => _updateContent ?? "";
        set => this.RaiseAndSetIfChanged(ref _updateContent, value);
    }
}