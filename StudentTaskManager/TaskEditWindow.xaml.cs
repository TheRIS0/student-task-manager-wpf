using System.Windows;
using StudentTaskManager.ViewModels;

namespace StudentTaskManager;

/// <summary>
/// Window for creating or editing a task. DataContext is TaskEditViewModel.
/// </summary>
public partial class TaskEditWindow : Window
{
    public TaskEditWindow()
    {
        InitializeComponent();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is not TaskEditViewModel viewModel)
            throw new InvalidOperationException("TaskEditWindow DataContext must be a TaskEditViewModel.");

        if (string.IsNullOrWhiteSpace(viewModel.Title))
        {
            MessageBox.Show("Please enter a title.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        DialogResult = true;
    }
}
