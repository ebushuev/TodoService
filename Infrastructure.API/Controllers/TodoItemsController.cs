using Application.Models;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoService _todoService;

        public TodoItemsController(
            ILogger<TodoItemsController> logger,
            ITodoService todoService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItemsAsync()
        {
            return Ok(await _todoService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItemAsync(long id)
        {
            TodoItemDTO result;
            try
            {
                result = await _todoService.GetAsync(id);
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _todoService.SaveAsync(todoItemDTO);
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            TodoItemDTO result;
            try
            {
                result = await _todoService.SaveAsync(todoItemDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(
                "CreateTodoItem",
                new { id = result.Id },
                result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemAsync(long id)
        {
            try
            {
                await _todoService.DeleteAsync(id);
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }
    }
}