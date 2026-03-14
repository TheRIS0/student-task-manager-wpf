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
        {
            MessageBox.Show(this, "An error occurred. Please close the dialog.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(viewModel.Title))
        {
            MessageBox.Show(this, "Please enter a title.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        DialogResult = true;
    }
}
