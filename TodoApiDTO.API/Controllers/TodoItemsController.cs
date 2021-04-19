using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApiDTO.Domain;
using TodoApiDTO.Domain.Models;
using TodoApiDTO.Infrastructure;
using TodoApiDTO.Infrastructure.Models;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с задачами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly ILogger<TodoItemsController> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="todoItemService"></param>
        /// <param name="logger"></param>
        public TodoItemsController(ITodoItemService todoItemService, ILogger<TodoItemsController> logger)
        {
            _todoItemService = todoItemService ?? throw new ArgumentNullException(nameof(todoItemService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Возвращает список задач
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            try
            {
                var items = await _todoItemService.GetTodoItemsAsync();
                return items.Select(x => ItemToDTO(x))
                .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Непредвиденная ошибка сервера: {e.Message}");
                throw e;
            }
        }

        /// <summary>
        /// Возвразщает задачу по ID
        /// </summary>
        /// <param name="id">ИД задачи</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            try
            {
                var todoItem = await _todoItemService.GetTodoItemAsync(id);
                if (todoItem == null)
                {
                    _logger.LogError($"Не найдено значение с ID: {id}");
                    return NotFound();
                }

                return ItemToDTO(todoItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Непредвиденная ошибка сервера: {e.Message}");
                throw e;
            }
        }

        /// <summary>
        /// Обновляет задачу
        /// </summary>
        /// <param name="id">ИД задачи</param>
        /// <param name="todoItemDTO">Новое значение</param>
        /// <returns></returns>
        /// <response code="204">Modified</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            try
            {
                if (id != todoItemDTO.Id)
                {
                    _logger.LogError($"Неверно указаны параметры {nameof(id)}={id} и {nameof(todoItemDTO)}={JsonSerializer.Serialize(todoItemDTO)}");
                    return BadRequest();
                }

                await _todoItemService.UpdateTodoItemAsync(todoItemDTO.GetTodoItem);
            }
            catch (NotFoundException e)
            {
                _logger.LogError(e, e.Message);
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Непредвиденная ошибка сервера: {e.Message}");
                throw e;
            }

            return NoContent();
        }

        /// <summary>
        /// Создает задачу
        /// </summary>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    IsComplete = todoItemDTO.IsComplete,
                    Name = todoItemDTO.Name
                };

                var created = await _todoItemService.CreateTodoItemAsync(todoItem);

                return CreatedAtAction(
                    nameof(GetTodoItem),
                    new { id = created.Id },
                    ItemToDTO(created));
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Непредвиденная ошибка сервера: {e.Message}");
                throw e;
            }
        }

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _todoItemService.DeleteTodoItemAsync(id);
            }
            catch (NotFoundException e)
            {
                _logger.LogError(e, e.Message);
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Непредвиденная ошибка сервера: {e.Message}");
                throw e;
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
    }
}
