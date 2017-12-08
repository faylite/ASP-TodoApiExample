using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    /// <summary>
    /// Item schema for todo items
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Id of the Todo item
        /// </summary>
        /// <returns></returns>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Name of the todo item
        /// </summary>
        /// <returns></returns>
        [Required]
        [DefaultValue("Todo item")]
        public string Name { get; set; }

        /// <summary>
        /// Completion status of the task
        /// </summary>
        /// <returns></returns>
        [Required]
        [DefaultValue(false)]
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