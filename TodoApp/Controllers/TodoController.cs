using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TodoApp.Models;

namespace TodoApp.Controllers
{
    /// <summary>
    /// Controller for the Todo items
    /// </summary>
    [Produces("application/json")]
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        /// <summary>
        /// Controller for the Todo items
        /// </summary>
        /// <param name="context"></param>
        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                foreach (int index in Enumerable.Range(1, 10))
                {
                    _context.TodoItems.Add(new TodoItem { Name = $"Item {index}" });
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// List all Todo items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoItem>), 200)]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        /// <summary>
        /// Get the Todo item with the provided id
        /// </summary>
        /// <param name="id">Id of Todo item</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTodo")]
        [ProducesResponseType(typeof(TodoItem), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public IActionResult Get(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            return new ObjectResult(item);
        }

        /// <summary>
        /// Create a new Todo item
        /// </summary>
        /// <param name="item">Todo Item object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TodoItem), 201)]
        [ProducesResponseType(typeof(void), 400)]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null) return BadRequest();
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        /// <summary>
        /// Update a Todo Item
        /// </summary>
        /// <param name="id">Id of item to update</param>
        /// <param name="item">Todo item object with updated data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(void), 400)]
        public IActionResult Update(int id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id) return BadRequest();
            var todo = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (todo == null) return NotFound();

            todo.IsCompleted = item.IsCompleted;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            return  new NoContentResult();
        }
        
        /// <summary>
        /// Delete a todo item
        /// </summary>
        /// <param name="id">Id of todo item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(void), 404)]
        public IActionResult Delete(int id)
        {
            var todo = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (todo == null) return NotFound();

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
