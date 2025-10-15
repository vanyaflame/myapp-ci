using SimpleTaskApp.Models;

namespace SimpleTaskApp.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
    }

    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public TaskService()
        {
            // Добавляем тестовые данные
            _tasks.Add(new TaskItem { 
                Id = _nextId++, 
                Title = "Изучить ASP.NET Core", 
                Description = "Освоить основы веб-разработки на .NET",
                IsCompleted = true,
                CreatedDate = DateTime.UtcNow.AddDays(-2)
            });
            _tasks.Add(new TaskItem { 
                Id = _nextId++, 
                Title = "Создать простое приложение", 
                Description = "Разработать задачник",
                IsCompleted = false,
                CreatedDate = DateTime.UtcNow.AddDays(-1)
            });
            _tasks.Add(new TaskItem { 
                Id = _nextId++, 
                Title = "Протестировать функциональность", 
                IsCompleted = false,
                CreatedDate = DateTime.UtcNow
            });
        }

        public Task<List<TaskItem>> GetAllTasksAsync() 
            => Task.FromResult(_tasks.OrderByDescending(t => t.CreatedDate).ToList());

        public Task<TaskItem?> GetTaskByIdAsync(int id) 
            => Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));

        public Task AddTaskAsync(TaskItem task)
        {
            task.Id = _nextId++;
            task.CreatedDate = DateTime.UtcNow;
            _tasks.Add(task);
            return Task.CompletedTask;
        }

        public Task UpdateTaskAsync(TaskItem task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.IsCompleted = task.IsCompleted;
                // Сохраняем оригинальную дату создания
                task.CreatedDate = existingTask.CreatedDate;
            }
            return Task.CompletedTask;
        }

        public Task DeleteTaskAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
            return Task.CompletedTask;
        }
    }
}