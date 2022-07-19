using RadYanFoFaViewer.Utils;
using ReactiveUI;

namespace RadYanFoFaViewer.ViewModels;

public class AboutViewViewModel : ViewModelBase
{
    private bool _isCheckButtonEnable;
    public string Version => $"RadYanFoFaViewer  {Update.Version}";

    public bool IsCheckButtonEnable
    {
        get => _isCheckButtonEnable;
        set => this.RaiseAndSetIfChanged(ref _isCheckButtonEnable, value);
    }
}