using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITaskRepository
{
    Task<TaskItem> CreateTaskAsync(TaskItem task);
    Task<IEnumerable<TaskItem>> GetRecentTasksAsync(int count);
    Task<bool> CompleteTaskAsync(Guid id);
}