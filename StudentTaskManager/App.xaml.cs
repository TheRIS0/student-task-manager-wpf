using System.Windows;
using StudentTaskManager.Services;

namespace StudentTaskManager;

/// <summary>
/// Application entry point.
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        DatabaseInitializer.Initialize();
        base.OnStartup(e);
    }
}
