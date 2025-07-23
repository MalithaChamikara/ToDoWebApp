using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    // Constructor
    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    // Create a new task
    public async Task<TaskItem> CreateTaskAsync(CreateTaskDto dto)
    {
        // Check if the title field is empty or not
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ArgumentException("Title is required.", nameof(dto.Title));

        // Create a new task object
        var task = new Models.TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };


        return await _repository.CreateTaskAsync(task);
    }

    // Get Recent Tasks
    public async Task<IEnumerable<TaskItem>> GetRecentTasksAsync(int count)
    {
        if (count <= 0)
            throw new ArgumentException("Count must be positive.", nameof(count));

        return await _repository.GetRecentTasksAsync(count);
    }

    // Complete a task
    public async Task<bool> CompleteTaskAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid task ID.", nameof(id));

        return await _repository.CompleteTaskAsync(id);
    }

    Task<IEnumerable<TaskItem>> ITaskService.GetRecentTasksAsync(int count)
    {
        throw new NotImplementedException();
    }
}