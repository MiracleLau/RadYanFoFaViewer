using RadYanFoFaViewer.Utils;
using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;

public class AboutViewViewModel: ViewModelBase
{
    public string Version => $"RadYanFoFaViewer  {Update.Version}";

    private bool _isCheckButtonEnable;

    public bool IsCheckButtonEnable
    {
        get => _isCheckButtonEnable;
        set => this.RaiseAndSetIfChanged(ref _isCheckButtonEnable, value);
    }
}