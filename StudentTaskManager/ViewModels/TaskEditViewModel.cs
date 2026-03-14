using StudentTaskManager.Models;

namespace StudentTaskManager.ViewModels;

/// <summary>
/// View model for the task edit window. Holds editable fields for creating or editing a task.
/// </summary>
public class TaskEditViewModel : ViewModelBase
{
    private int _id;
    private string _title = "New task";
    private string _description = string.Empty;
    private DateTime? _dueDate;
    private bool _isCompleted;
    private DateTime _createdAt;

    public TaskEditViewModel(TaskItem? existing)
    {
        if (existing != null)
        {
            _id = existing.Id;
            _title = existing.Title;
            _description = existing.Description;
            _dueDate = existing.DueDate;
            _isCompleted = existing.IsCompleted;
            _createdAt = existing.CreatedAt;
        }
    }

    /// <summary>
    /// Task Id. Zero for a new task.
    /// </summary>
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public DateTime? DueDate
    {
        get => _dueDate;
        set => SetProperty(ref _dueDate, value);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value);
    }

    /// <summary>
    /// Returns a TaskItem with the current property values. Used after the user confirms the dialog.
    /// </summary>
    public TaskItem GetTask()
    {
        string title = string.IsNullOrWhiteSpace(_title) ? "New task" : _title;
        return new TaskItem
        {
            Id = _id,
            Title = title,
            Description = _description,
            DueDate = _dueDate,
            IsCompleted = _isCompleted,
            CreatedAt = _createdAt
        };
    }
}
