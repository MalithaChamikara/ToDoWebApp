using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services;

public interface ITaskService
{
    Task<TaskItem> CreateTaskAsync(CreateTaskDto dto);
    Task<IEnumerable<TaskItem>> GetRecentTasksAsync(int count);
    Task<bool> CompleteTaskAsync(Guid id);
}