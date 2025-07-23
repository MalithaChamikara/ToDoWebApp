using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    // Dependency injection for the task service
    private readonly ITaskService _service;

    // Constructor to inject the task service
    public TasksController(ITaskService service)
    {
        _service = service;
    }

    // POST /api/tasks
    // Endpoint to create a new task
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
    {
        try
        {
            var task = await _service.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(GetRecentTasks), new { id = task.Id }, task);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET /api/tasks
    // Endpoint to get recent tasks
    [HttpGet]
    public async Task<IEnumerable<TaskItem>> GetRecentTasks()
    {
        return await _service.GetRecentTasksAsync(5);
    }

    // PUT /api/tasks/{id}/complete
    // Endpoint to mark a task as complete
    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteTask(Guid id)
    {
        try
        {
            var success = await _service.CompleteTaskAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}