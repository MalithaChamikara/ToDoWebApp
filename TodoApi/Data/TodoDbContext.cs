using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public class TodoDbContext : DbContext
{
    // Constructor to initialize the DbContext
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }

    // Define the DbSet for the Task entity which represents the tasks table in the database
    public DbSet<TaskItem> Tasks { get; set; }


}