using System.Windows;
using StudentTaskManager.ViewModels;

namespace StudentTaskManager;

/// <summary>
/// Main window of the application.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
