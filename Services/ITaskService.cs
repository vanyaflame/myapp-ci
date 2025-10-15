using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync() 
            => await _context.Tasks.OrderByDescending(t => t.CreatedDate).ToListAsync();

        public async Task<TaskItem?> GetTaskByIdAsync(int id) 
            => await _context.Tasks.FindAsync(id);

        public async Task AddTaskAsync(TaskItem task)
        {
            task.CreatedDate = DateTime.UtcNow;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}