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
    private ObservableCollection<TaskItem> _filteredTasks = new();
    private int _selectedFilterIndex;
    private TaskItem? _selectedTask;
    private string _welcomeMessage = "Welcome to Student Task Manager";

    private Func<TaskItem?, TaskItem?>? _showTaskEditDialog;

    /// <summary>
    /// Filter options for the task list: All, Active, Completed.
    /// </summary>
    public string[] FilterOptions { get; } = new[] { "All", "Active", "Completed" };

    /// <summary>
    /// Set by the view to show the task edit dialog. Pass null for new task, or existing task to edit. Returns the task to save or null if cancelled.
    /// </summary>
    public Func<TaskItem?, TaskItem?>? ShowTaskEditDialog
    {
        get => _showTaskEditDialog;
        set
        {
            if (SetProperty(ref _showTaskEditDialog, value))
                CommandManager.InvalidateRequerySuggested();
        }
    }

    public MainViewModel()
    {
        LoadTasksCommand = new RelayCommand(LoadTasks);
        AddTaskCommand = new RelayCommand(AddTask, () => ShowTaskEditDialog != null);
        EditTaskCommand = new RelayCommand(EditTask, () => SelectedTask != null && ShowTaskEditDialog != null);
        DeleteTaskCommand = new RelayCommand(DeleteTask, () => SelectedTask != null);
        ToggleCompletionCommand = new RelayCommand(ToggleCompletion, () => SelectedTask != null);

        LoadTasks();
    }

    /// <summary>
    /// All tasks from the database.
    /// </summary>
    public ObservableCollection<TaskItem> Tasks
    {
        get => _tasks;
        set => SetProperty(ref _tasks, value);
    }

    /// <summary>
    /// Tasks visible in the list based on the current filter (All, Active, or Completed).
    /// </summary>
    public ObservableCollection<TaskItem> FilteredTasks
    {
        get => _filteredTasks;
        set => SetProperty(ref _filteredTasks, value);
    }

    /// <summary>
    /// Selected filter index: 0 = All, 1 = Active, 2 = Completed.
    /// </summary>
    public int SelectedFilterIndex
    {
        get => _selectedFilterIndex;
        set
        {
            if (SetProperty(ref _selectedFilterIndex, value))
                RefreshFilteredTasks();
        }
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
        RefreshFilteredTasks();
    }

    private void RefreshFilteredTasks()
    {
        var filtered = _selectedFilterIndex switch
        {
            1 => Tasks.Where(t => !t.IsCompleted).ToList(),
            2 => Tasks.Where(t => t.IsCompleted).ToList(),
            _ => Tasks.ToList()
        };
        FilteredTasks = new ObservableCollection<TaskItem>(filtered);
    }

    private void EnsureShowTaskEditDialogConfigured()
    {
        if (ShowTaskEditDialog == null)
            throw new InvalidOperationException("ShowTaskEditDialog must be set by the view before using Add or Edit.");
    }

    private void AddTask()
    {
        EnsureShowTaskEditDialogConfigured();
        var task = ShowTaskEditDialog!.Invoke(null);
        if (task != null)
        {
            _taskService.Add(task);
            LoadTasks();
        }
    }

    private void EditTask()
    {
        if (SelectedTask == null) return;
        EnsureShowTaskEditDialogConfigured();
        var task = ShowTaskEditDialog!.Invoke(SelectedTask);
        if (task != null)
        {
            _taskService.Update(task);
            LoadTasks();
        }
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
