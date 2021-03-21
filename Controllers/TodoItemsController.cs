using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Bll;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _service;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoService service
            , ILogger<TodoItemsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodoItems() => await _service.GetTodos();

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            if (!TodoItemExists(id))
            {
                return NotFound();
            }

            var todoItem = await _service.GetTodo(id);
            return todoItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            if (!TodoItemExists(id))
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateTodo(todoItem);
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                _logger.LogError($"Todo item with {id} has not been found!");
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItem todoItem)
        {
            try
            {
                var todo = await _service.CreateTodo(todoItem);
                return CreatedAtAction(nameof(GetTodoItem), new { id = todo.Id }, todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(CreateTodoItem));
                return BadRequest();
            }           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (!TodoItemExists(id))
            {
                return NotFound();
            }
            
            try
            {
                await _service.DeleteTodo(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(DeleteTodoItem));
                return BadRequest();
            }

            return NoContent();
        }

        private bool TodoItemExists(long id) => _service.TodoItemExists(id);     
    }
}
