using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    /// <summary>
    /// Item schema for todo items
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Id of the TodoItem
        /// </summary>
        /// <returns></returns>
        public long Id { get; set; }

        /// <summary>
        /// Name of the todo task
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// Completion status of the task
        /// </summary>
        /// <returns></returns>
        public bool IsCompleted { get; set; }
    }

    /// <summary>
    /// Database context for the todo items
    /// </summary>
    public class TodoContext : DbContext
    {
        /// <summary>
        /// Database context for the todo items
        /// </summary>
        /// <param name="options">Context options</param>
        /// <returns></returns>
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        /// <summary>
        /// Instances of todo in the database
        /// </summary>
        /// <returns></returns>
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}