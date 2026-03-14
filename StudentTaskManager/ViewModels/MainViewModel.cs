namespace StudentTaskManager.ViewModels;

/// <summary>
/// View model for the main window.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private string _welcomeMessage = "Welcome to Student Task Manager";

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set
        {
            if (_welcomeMessage != value)
            {
                _welcomeMessage = value;
                OnPropertyChanged();
            }
        }
    }
}
