using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories;

public class TaskRepository : ITaskRepository
{
    // Private field for DbContext 
    private readonly TodoDbContext _context;

    // Constructor which initializes _context field
    public TaskRepository(TodoDbContext context)
    {
        _context = context;
    }

    // Create task 
    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    // Get recent tasks
    public async Task<IEnumerable<TaskItem>> GetRecentTasksAsync(int count)
    {
        return await _context.Tasks
            .Where(t => !t.IsCompleted)
            .OrderByDescending(t => t.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    // Complete a task 
    public async Task<bool> CompleteTaskAsync(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;
        task.IsCompleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

}