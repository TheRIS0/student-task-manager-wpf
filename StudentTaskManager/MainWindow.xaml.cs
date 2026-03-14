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
        var viewModel = new MainViewModel();
        viewModel.ShowTaskEditDialog = (existing) =>
        {
            var editViewModel = new TaskEditViewModel(existing);
            var window = new TaskEditWindow
            {
                Owner = this,
                DataContext = editViewModel
            };
            return window.ShowDialog() == true ? editViewModel.GetTask() : null;
        };
        DataContext = viewModel;
    }
}
