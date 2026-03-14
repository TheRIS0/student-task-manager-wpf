using System.Windows;

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
        DialogResult = true;
    }
}
