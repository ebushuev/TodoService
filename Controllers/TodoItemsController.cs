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
        public async Task<IActionResult> GetTodoItem(long id)
        {
            try
            {
                var item = await _todoItemService.GetAsync(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return ProcessExceptionMessage(ex.Message);
            }
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
                // there was a problem with CreatedAtAction:
                // https://github.com/microsoft/aspnet-api-versioning/issues/558
                var routeValues = new { id = todoItemDTO.Id, version = "1.0" };
                return CreatedAtAction(
                    nameof(GetTodoItem),
                    routeValues,
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
            if (exceptionMessage == "Not Found")
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