using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentTaskManager.ViewModels;

/// <summary>
/// Base class for view models. Provides INotifyPropertyChanged for data binding.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
    }
}
