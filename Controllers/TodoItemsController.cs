using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.BL;
using Microsoft.Extensions.Logging;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepository _service;
        private readonly ILogger<TodoItemsController> _log;

        public TodoItemsController(ITodoRepository service, ILogger<TodoItemsController> logger)
        {
            _service = service;
            _log = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {

            var result = await _service.ListAsync();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _service.GetAsync(id);

            if (todoItem == null)
            {
                _log.LogInformation($"Todo item {id} is missing!");
                return NotFound();
            }
            return todoItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                _log.LogInformation($"Todo item {id} is missing!");
                return BadRequest();
            }
            var todoItem = await _service.SaveAsync(id, todoItemDTO);

            if (todoItem == null)
            {
                _log.LogInformation($"Todo item {id} is missing!");
                return NotFound();
            }
            return Ok(todoItem);
        }

        

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _service.InsertAsync(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                todoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }




    }
}
