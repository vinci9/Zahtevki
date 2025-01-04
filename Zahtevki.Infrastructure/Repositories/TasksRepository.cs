using Microsoft.EntityFrameworkCore;
using Zahtevki.Core.Entities;
using Zahtevki.Core.Interfaces;
using Zahtevki.Infrastructure.Data;

namespace Zahtevki.Infrastructure.Repositories
{
    public class TasksRepository(AppDbContext dbContext) : ITasksRepository
    {
        public async Task<IEnumerable<TasksEntity>> GetAllTasks()
        {
            return await dbContext.Tasks.ToListAsync();
        }

        public async Task<TasksEntity> GetTaskByIdAsync(Guid id)
        {
            var task = await dbContext.Tasks.SingleOrDefaultAsync(x => x.Id == id);
            
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} was not found.");
            }

            return task;
        }

        public async Task<TasksEntity> AddTaskAsync(TasksEntity entity)
        {
            entity.Id = Guid.NewGuid();

            dbContext.Tasks.Add(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TasksEntity> UpdateTaskAsync(Guid id, TasksEntity entity)
        {
            var task = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} was not found.");
            }

            task.Title = entity.Title;
            task.Description = entity.Description;
            task.IsCompleted = entity.IsCompleted;
            task.DueDate = entity.DueDate;
            task.Priority = entity.Priority;
            task.UpdatedDate = DateTime.Now;

            await dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} was not found.");
            }

            dbContext.Tasks.Remove(task);

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}