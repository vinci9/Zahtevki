using Zahtevki.Core.Entities;

namespace Zahtevki.Core.Interfaces
{
    public interface ITasksRepository
    {
        Task<IEnumerable<TasksEntity>> GetAllTasks();
        Task<TasksEntity> GetTaskByIdAsync(Guid id);
        Task<TasksEntity> AddTaskAsync(TasksEntity entity);
        Task<TasksEntity> UpdateTaskAsync(Guid id, TasksEntity entity);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}