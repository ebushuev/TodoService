using Entities.Models;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService todoItemsService;

        public TodoItemsController(ITodoItemsService todoItemsService)
        {
            this.todoItemsService = todoItemsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var allItems = await todoItemsService.GetAllAsync();

            return Ok(allItems.Select(x => TodoItemDTO.ItemToDTO(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {

            var todoItem = await todoItemsService.GetAsync(id); 

            if (todoItem == null)
            {
                return NotFound();
            }

            return TodoItemDTO.ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            if (await todoItemsService.UpdateAsync(todoItemDTO)) return NoContent();

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            await todoItemsService.CreateAsync(todoItem);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                TodoItemDTO.ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {

            if (await todoItemsService.DeleteAsync(id)) return NoContent();

            return NotFound();
        }
    }
}
