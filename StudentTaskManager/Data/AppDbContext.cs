using System.IO;
using Microsoft.EntityFrameworkCore;
using StudentTaskManager.Models;

namespace StudentTaskManager.Data;

/// <summary>
/// Entity Framework Core database context for the task manager.
/// The SQLite database file is stored next to the application executable.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        // Database file lives in the same folder as the application.
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StudentTaskManager.db");
        string connectionString = $"Data Source={path}";

        optionsBuilder.UseSqlite(connectionString);
    }
}
