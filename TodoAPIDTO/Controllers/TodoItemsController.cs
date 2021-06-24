using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Application.Interfaces.Services;
using TodoApiDTO.Application.Interfaces.Services.Models;

namespace TodoApi.Controllers
{
    /// <summary>
    /// CRUD resource for todo items
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Get list of all todo items
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<TodoItemDTO>), 200)]
        public async Task<IActionResult> GetTodoItems()
        {
            IList<TodoItemEntity> todoItems = await _todoItemService.GetAllAsync();
            return Ok(todoItems.Select(i => ConvertToResponseModel(i)));
        }

        /// <summary>
        /// Get todo item
        /// </summary>
        /// <param name="id">Identificator</param>
        /// <returns></returns>
        [HttpGet("{id}"), ProducesResponseType(typeof(TodoItemDTO), 200)]
        public async Task<IActionResult> GetTodoItem([FromRoute]long id)
        {
            TodoItemEntity todoItem = await _todoItemService.GetAsync(id);
            return Ok(ConvertToResponseModel(todoItem));
        }

        /// <summary>
        /// Update todo Item
        /// </summary>
        /// <param name="id">Identificator</param>
        /// <param name="todoItemDTO">payload</param>
        /// <returns></returns>
        [HttpPut("{id}"), ProducesResponseType(typeof(TodoItemDTO), 200)]
        public async Task<IActionResult> UpdateTodoItem([FromRoute] long id, [FromBody] TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
                return BadRequest();

            var todoItemRequest = new TodoItemEntity
            {
                IsComplete = todoItemDTO.IsComplete,
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name
            };
            TodoItemEntity todoItemResponse = await _todoItemService.UpdateAsync(todoItemRequest);
            TodoItemDTO response = ConvertToResponseModel(todoItemResponse);
            return Ok(response);
        }

        /// <summary>
        /// Creates todo item
        /// </summary>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(typeof(TodoItemDTO), 200)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItemEntity = new TodoItemEntity
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            TodoItemEntity createTodoItemResponse =
                await _todoItemService.CreateAsync(todoItemEntity);
            TodoItemDTO response = ConvertToResponseModel(createTodoItemResponse);
            return Ok(response);
        }

        /// <summary>
        /// Deletes todo item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem([FromRoute] long id)
        {
            await _todoItemService.DeleteAsync(id);
            return NoContent();
        }

        private TodoItemDTO ConvertToResponseModel(TodoItemEntity todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };       
    }
}
