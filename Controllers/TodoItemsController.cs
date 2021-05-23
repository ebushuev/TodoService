using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DataAccess;
using TodoApiDTO.DataAccess.Abstraction;

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
        public async Task<ActionResult> GetTodoItems()
        {
            var items = await _todoItemService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemService.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                await _todoItemService.UpdateAsync(id, todoItemDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return ProcessExceptionMessage(ex.Message);
            }  
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            try
            {
                var todoItem =  await _todoItemService.AddAsync(todoItemDTO);
                return CreatedAtAction(
                    nameof(GetTodoItem),
                    todoItem
                    );
            }
            catch (Exception ex)
            {                
                return BadRequest();        
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _todoItemService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return ProcessExceptionMessage(ex.Message);
            }
        }

        private IActionResult ProcessExceptionMessage(string exceptionMessage)
        {
            if (exceptionMessage == "NotFound")
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}