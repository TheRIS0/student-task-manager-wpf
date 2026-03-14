# Student Task Manager

A desktop task manager application built with WPF and C#, following a simple MVVM architecture and using Entity Framework Core with SQLite for persistence. Built as a portfolio project demonstrating desktop application development with WPF, MVVM, and EF Core.

## Overview

Student Task Manager lets users create, edit, delete, and track tasks with title, description, optional due date, and completion status. The UI supports filtering by All, Active, or Completed tasks. Data is stored in a local SQLite database file created automatically on first run.

## Key Features

- **Task CRUD** — Add, edit, and delete tasks via a modal dialog
- **Completion toggle** — Mark tasks complete or active from the main list
- **Filtering** — View All tasks, Active (incomplete), or Completed tasks
- **Persistence** — SQLite database with Entity Framework Core; database and tables are created on startup
- **Input validation** — Title required when saving; user-friendly message boxes for errors
- **Strikethrough display** — Completed tasks shown with strikethrough in the list

## Technology Stack

- **.NET 8** (Windows)
- **WPF** — UI and data binding
- **C#** — Nullable reference types, modern language features
- **Entity Framework Core 8** — ORM with SQLite provider
- **SQLite** — Single-file database stored next to the executable

## Architecture Summary

- **MVVM** — Views (XAML) bind to view models; logic lives in view models and services
- **Models** — `TaskItem` (Id, Title, Description, IsCompleted, CreatedAt, DueDate)
- **View models** — `MainViewModel` (task list, filter, commands), `TaskEditViewModel` (edit form state); both use `ViewModelBase` for property change notification
- **Commands** — `RelayCommand` for button actions; no dependency injection
- **Data** — `AppDbContext` with `DbSet<TaskItem>`; connection string points to a local `.db` file
- **Services** — `TaskService` (CRUD and toggle completion), `DatabaseInitializer` (ensures database exists on startup)
- **Flow** — Main window sets a `ShowTaskEditDialog` delegate on `MainViewModel` so add/edit open the task edit window; basic title validation is handled in the task edit window code-behind, while persistence stays in the service layer

## Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (or later)
- Windows (WPF target)

### Build and run

```bash
git clone <repository-url>
cd <repo-folder>
dotnet build StudentTaskManager.sln
dotnet run --project ./StudentTaskManager/StudentTaskManager.csproj
```

Or open `StudentTaskManager.sln` in Visual Studio 2022 and run the project. The SQLite database file (`StudentTaskManager.db`) is created in the application output directory on first run.

## Future Improvements

- **Categories or tags** — Group or label tasks
- **Sorting** — By due date, title, or creation date
- **Search** — Filter tasks by title or description text
- **EF Core migrations** — Replace `EnsureCreated()` with migrations for schema evolution
- **Unit tests** — For `TaskService`, view models, and validation logic
- **Accessibility** — Keyboard navigation and screen-reader support

## License

See the [LICENSE](LICENSE) file in the repository.
