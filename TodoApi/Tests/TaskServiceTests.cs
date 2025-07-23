using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TodoApi.Tests
{
    public class TaskServiceTests
    {
        private TodoDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new TodoDbContext(options);
        }

        [Fact]
        public async Task CreateTaskAsync_ValidDto_ReturnsCreatedTask()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new TaskRepository(context);
            var service = new TaskService(repository);
            var dto = new CreateTaskDto { Title = "Test Task", Description = "Test Description" };

            // Act
            var task = await service.CreateTaskAsync(dto);

            // Assert
            var savedTask = await context.Tasks.FindAsync(task.Id);
            Assert.NotNull(savedTask);
            Assert.Equal("Test Task", savedTask.Title);
            Assert.Equal("Test Description", savedTask.Description);
            Assert.False(savedTask.IsCompleted);
        }

        [Fact]
        public async Task CreateTaskAsync_EmptyTitle_ThrowsArgumentException()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new TaskRepository(context);
            var service = new TaskService(repository);
            var dto = new CreateTaskDto { Title = "", Description = "Test Description" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTaskAsync(dto));
        }

        [Fact]
        public async Task GetRecentTasksAsync_ReturnsFiveRecentNonCompletedTasks()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new TaskRepository(context);
            var service = new TaskService(repository);
            for (int i = 1; i <= 7; i++)
            {
                context.Tasks.Add(new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = $"Task {i}",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-i),
                    IsCompleted = i == 7
                });
            }
            await context.SaveChangesAsync();

            // Act
            var tasks = await service.GetRecentTasksAsync(5);

            // Assert
            Assert.Equal(5, tasks.Count());
            Assert.All(tasks, t => Assert.False(t.IsCompleted));
            Assert.True(tasks.First().CreatedAt >= tasks.Last().CreatedAt);
        }

        [Fact]
        public async Task CompleteTaskAsync_ValidId_MarksTaskAsCompleted()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new TaskRepository(context);
            var service = new TaskService(repository);
            var task = new TaskItem { Id = Guid.NewGuid(), Title = "Test", IsCompleted = false };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            // Act
            var result = await service.CompleteTaskAsync(task.Id);

            // Assert
            Assert.True(result);
            var updatedTask = await context.Tasks.FindAsync(task.Id);
            Assert.True(updatedTask.IsCompleted);
        }

        [Fact]
        public async Task CompleteTaskAsync_InvalidId_ReturnsFalse()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new TaskRepository(context);
            var service = new TaskService(repository);

            // Act
            var result = await service.CompleteTaskAsync(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }
    }
}