using Zahtevki.Core.Entities;
using Zahtevki.Core.Interfaces;
using Zahtevki.Infrastructure.Data;
using Zahtevki.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Zahtevki.UnitTests
{
    public class CRUDUnitTests : IDisposable
    {
        private readonly ITasksRepository _taskRepository;
        private readonly AppDbContext _dbContext;

        public CRUDUnitTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(options);
            _taskRepository = new TasksRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Fact]
        public async Task AddTaskTest()
        {
            var task = new TasksEntity
            {
                Title = "Test Task",
                Description = "Test Description",
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = 1
            };

            var result = await _taskRepository.AddTaskAsync(task);

            Assert.NotNull(result);
            Assert.Equal(task.Title, result.Title);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task GetTaskByIdTest()
        {
            var task = await _taskRepository.AddTaskAsync(new TasksEntity
            {
                Title = "Test Task",
                Description = "Test Description",
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = 1
            });

            var result = await _taskRepository.GetTaskByIdAsync(task.Id);

            Assert.NotNull(result);
            Assert.Equal(task.Title, result.Title);
        }

        [Fact]
        public async Task DeleteTaskTest()
        {
            var task = await _taskRepository.AddTaskAsync(new TasksEntity
            {
                Title = "Test Task",
                Description = "Test Description",
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = 1
            });

            var result = await _taskRepository.DeleteTaskAsync(task.Id);

            Assert.True(result);

            var deletedTask = await _dbContext.Tasks.FindAsync(task.Id);
            Assert.Null(deletedTask);
        }

        [Fact]
        public async Task DeleteTaskTestException()
        {
            var nonExistentId = Guid.NewGuid();

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _taskRepository.DeleteTaskAsync(nonExistentId));
        }

        [Fact]
        public async Task UpdateTaskTest()
        {
            var task = await _taskRepository.AddTaskAsync(new TasksEntity
            {
                Title = "Test Task",
                Description = "Test Description",
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = 1
            });

            var updatedTask = new TasksEntity
            {
                Title = "Updated Task",
                Description = "Updated Description",
                DueDate = DateTime.UtcNow.AddDays(2),
                Priority = 2
            };

            var result = await _taskRepository.UpdateTaskAsync(task.Id, updatedTask);

            Assert.NotNull(result);
            Assert.Equal("Updated Task", result.Title);
            Assert.Equal("Updated Description", result.Description);
            Assert.Equal(updatedTask.DueDate, result.DueDate);
            Assert.Equal(updatedTask.Priority, result.Priority);
        }

        [Fact]
        public async Task UpdateTaskTestException()
        {
            var nonExistentId = Guid.NewGuid();
            var updatedTask = new TasksEntity
            {
                Title = "Updated Task",
                Description = "Updated Description",
                DueDate = DateTime.UtcNow.AddDays(2),
                Priority = 2
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _taskRepository.UpdateTaskAsync(nonExistentId, updatedTask));
        }

        [Fact]
        public async Task GetAllTasksTest()
        {
            var task1 = new TasksEntity
            {
                Title = "Test Task 1",
                Description = "Test Description 1",
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = 1
            };
            var task2 = new TasksEntity
            {
                Title = "Test Task 2",
                Description = "Test Description 2",
                DueDate = DateTime.UtcNow.AddDays(2),
                Priority = 2
            };

            await _taskRepository.AddTaskAsync(task1);
            await _taskRepository.AddTaskAsync(task2);

            var result = await _taskRepository.GetAllTasks();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            Assert.Collection(result, t => Assert.Equal("Test Task 1", t.Title), t => Assert.Equal("Test Task 2", t.Title));
        }
    }
}