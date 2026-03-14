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
        return context.Tasks.ToList();
    }

    /// <summary>
    /// Adds a new task. Sets CreatedAt to UTC now if not already set, then saves.
    /// </summary>
    public void Add(TaskItem task)
    {
        if (task.CreatedAt == default)
            task.CreatedAt = DateTime.UtcNow;

        using var context = new AppDbContext();
        context.Tasks.Add(task);
        context.SaveChanges();
    }

    /// <summary>
    /// Updates an existing task. The task must have a valid Id.
    /// </summary>
    public void Update(TaskItem task)
    {
        using var context = new AppDbContext();
        context.Tasks.Update(task);
        context.SaveChanges();
    }

    /// <summary>
    /// Deletes the task with the given Id. Does nothing if no task is found.
    /// </summary>
    public void Delete(int id)
    {
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
        using var context = new AppDbContext();
        var task = context.Tasks.Find(id);
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            context.SaveChanges();
        }
    }
}
