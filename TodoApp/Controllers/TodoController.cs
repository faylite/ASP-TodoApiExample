using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

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

        // GET: api/Todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        // GET: api/Todo/5
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult Get(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            return new ObjectResult(item);
        }
        
        // POST: api/Todo
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null) return BadRequest();
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
        
        // PUT: api/Todo/5
        [HttpPut("{id}")]
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
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
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
