using StudentTaskManager.Data;

namespace StudentTaskManager.Services;

/// <summary>
/// Ensures the SQLite database and tables exist when the application starts.
/// </summary>
public static class DatabaseInitializer
{
    /// <summary>
    /// Creates the database file and tables if they do not already exist.
    /// Call this once at application startup.
    /// </summary>
    public static void Initialize()
    {
        using var context = new AppDbContext();
        context.Database.EnsureCreated();
    }
}
