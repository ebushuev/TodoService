using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Core;
using TodoApiDTO.Infrastructure;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var items = (await _todoItemService.GetTodoItems()).Object;
            var res = new List<TodoItemDTO>(items.Count());

            foreach (var item in items)
            {
                res.Add(ItemToDTO(item));
            }

            return res;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var res = (await _todoItemService.GetTodoItem(id)).Object;

            return ItemToDTO(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var item = DTOToItem(todoItemDTO);
            var res = await _todoItemService.UpdateTodoItem(id, item);

            if (res.StatusCode == Result<TodoItem>.BAD_REQUEST)
            {
                return BadRequest();
            }

            if (res.StatusCode == Result<TodoItem>.NOT_FOUND)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            var res = await _todoItemService.CreateTodoItem(todoItem);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var res = await _todoItemService.DeleteTodoItem(id);

            if (res.StatusCode == Result<TodoItem>.NOT_FOUND)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        private static TodoItem DTOToItem(TodoItemDTO todoItem) =>
            new TodoItem
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete,
                Secret = "some secret"
            };
    }
}
