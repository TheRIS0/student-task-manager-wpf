using Microsoft.EntityFrameworkCore;
using StudentTaskManager.Data;
using StudentTaskManager.Models;

namespace StudentTaskManager.Services;

/// <summary>
/// Handles CRUD operations for tasks using the application database.
/// Each method uses its own context and disposes it when done.
/// </summary>
public class TaskService
{
    /// <summary>
    /// Returns all tasks from the database, in no specific order.
    /// </summary>
    public List<TaskItem> GetAll()
    {
        using var context = new AppDbContext();
        return context.Tasks.AsNoTracking().ToList();
    }

    /// <summary>
    /// Adds a new task. Sets CreatedAt to UTC now if not already set, then saves.
    /// The task must have Id equal to 0 (new entity).
    /// </summary>
    public void Add(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        if (task.Id != 0)
            throw new ArgumentException("New task must have Id equal to 0.", nameof(task));

        if (task.CreatedAt == default)
            task.CreatedAt = DateTime.UtcNow;

        using var context = new AppDbContext();
        context.Tasks.Add(task);
        context.SaveChanges();
    }

    /// <summary>
    /// Updates an existing task. Loads the task by Id and updates Title, Description, IsCompleted, and DueDate. CreatedAt is left unchanged.
    /// Does nothing if no task with the given Id is found.
    /// </summary>
    public void Update(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        if (task.Id <= 0)
            throw new ArgumentException("Task must have an Id greater than 0.", nameof(task));

        using var context = new AppDbContext();
        var existing = context.Tasks.Find(task.Id);
        if (existing == null)
            return;

        existing.Title = task.Title;
        existing.Description = task.Description;
        existing.IsCompleted = task.IsCompleted;
        existing.DueDate = task.DueDate;
        context.SaveChanges();
    }

    /// <summary>
    /// Deletes the task with the given Id. Does nothing if no task is found.
    /// </summary>
    public void Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id must be greater than 0.", nameof(id));

        using var context = new AppDbContext();
        var task = context.Tasks.Find(id);
        if (task != null)
        {
            context.Tasks.Remove(task);
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Toggles the IsCompleted flag for the task with the given Id.
    /// Does nothing if no task is found.
    /// </summary>
    public void ToggleCompletion(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id must be greater than 0.", nameof(id));

        using var context = new AppDbContext();
        var task = context.Tasks.Find(id);
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            context.SaveChanges();
        }
    }
}
