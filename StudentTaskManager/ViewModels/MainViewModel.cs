using System.Collections.ObjectModel;
using System.Windows.Input;
using StudentTaskManager.Commands;
using StudentTaskManager.Models;
using StudentTaskManager.Services;

namespace StudentTaskManager.ViewModels;

/// <summary>
/// View model for the main window. Manages the task list and commands for CRUD and completion toggle.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly TaskService _taskService = new();
    private ObservableCollection<TaskItem> _tasks = new();
    private TaskItem? _selectedTask;
    private string _welcomeMessage = "Welcome to Student Task Manager";

    public MainViewModel()
    {
        LoadTasksCommand = new RelayCommand(LoadTasks);
        AddTaskCommand = new RelayCommand(AddTask);
        EditTaskCommand = new RelayCommand(EditTask, () => SelectedTask != null);
        DeleteTaskCommand = new RelayCommand(DeleteTask, () => SelectedTask != null);
        ToggleCompletionCommand = new RelayCommand(ToggleCompletion, () => SelectedTask != null);

        LoadTasks();
    }

    /// <summary>
    /// All tasks from the database. Bound to the task list in the view.
    /// </summary>
    public ObservableCollection<TaskItem> Tasks
    {
        get => _tasks;
        set => SetProperty(ref _tasks, value);
    }

    /// <summary>
    /// Welcome text. Shown until the task list UI is fully in place.
    /// </summary>
    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set => SetProperty(ref _welcomeMessage, value);
    }

    /// <summary>
    /// The task currently selected in the list. Used by Edit, Delete, and ToggleCompletion.
    /// </summary>
    public TaskItem? SelectedTask
    {
        get => _selectedTask;
        set
        {
            if (SetProperty(ref _selectedTask, value))
                CommandManager.InvalidateRequerySuggested();
        }
    }

    public ICommand LoadTasksCommand { get; }
    public ICommand AddTaskCommand { get; }
    public ICommand EditTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand ToggleCompletionCommand { get; }

    private void LoadTasks()
    {
        var list = _taskService.GetAll();
        Tasks = new ObservableCollection<TaskItem>(list);
    }

    private void AddTask()
    {
        var task = new TaskItem
        {
            Title = "New task",
            Description = string.Empty,
            IsCompleted = false,
            DueDate = null
        };
        _taskService.Add(task);
        LoadTasks();
    }

    private void EditTask()
    {
        if (SelectedTask == null) return;
        _taskService.Update(SelectedTask);
        LoadTasks();
    }

    private void DeleteTask()
    {
        if (SelectedTask == null) return;
        _taskService.Delete(SelectedTask.Id);
        SelectedTask = null;
        LoadTasks();
    }

    private void ToggleCompletion()
    {
        if (SelectedTask == null) return;
        _taskService.ToggleCompletion(SelectedTask.Id);
        LoadTasks();
    }
}
