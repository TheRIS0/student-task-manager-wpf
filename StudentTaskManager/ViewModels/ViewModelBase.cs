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

    /// <summary>
    /// Sets the backing field and raises PropertyChanged if the value changed.
    /// Use this in property setters to keep view models consistent.
    /// </summary>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
