using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    // TODO: Look in to adding back "await" keywords and updating the service layer to be asynchronous
    public class TodoItemsController : ControllerBase
    {
        // TODO: Replace TodoContext dependency with TodoService
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return _todoService.GetAll().ToList();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = _todoService.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // TODO: Get the list of Labels to update correctly off of the TodoItem data
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            // Q: I don't like that this logic is here, maybe discuss a different way to detect that an item was not found, but no server problem occurred
            if (_todoService.Get(id) == null)
            {
                // Could not find the todoitem
                return NotFound();
            }

            if (_todoService.Update(todoItem))
            {
                // TodoItem was updated successfully
                return NoContent();
            }

            // Something went wrong internally with updating the TodoItem
            return StatusCode(StatusCodes.Status500InternalServerError);


            // TODO: Try to load back in the TodoItem, make changes, then save it

            //var item = _context.TodoItems
            //                    .Where(t => t.Id == id)
            //                    .Include(t => t.Labels)
            //                    .FirstOrDefault();
            //var todoItems = _context.TodoItems.ToList();
            //var labels = _context.Labels.ToList();

            //var newLabel = new Label("NewLabel");
            //labels.Add(newLabel);
            //todoItems[0].Labels.Add(newLabel);


            //foreach (var label in todoItem.Labels)
            //{
            //    item.Labels.Add(label);
            //}

            //ICollection<Label> labels = item.Labels;
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            if (_todoService.Add(todoItem))
            {
                // TodoItem was added successfully
                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
            }

            // Something went wrong internally with adding the TodoItem
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = _todoService.Remove(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            // Return the item that was deleted
            return todoItem;
        }
    }
}
