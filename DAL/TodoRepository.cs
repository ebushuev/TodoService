using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.BL;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;


namespace TodoApiDTO.DAL
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItemDTO>> ListAsync()
        {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };

        public async Task<TodoItemDTO> GetAsync(long id)
        {
            var result = await _context.TodoItems.FindAsync(id);
            return result != null? ItemToDTO(result) : null;            
        }

        public async Task<TodoItemDTO> SaveAsync(long id, TodoItemDTO todo)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                todoItem.Name = todo.Name;
                todoItem.IsComplete = todo.IsComplete;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
                {
                    todoItem = null;
                }
            }
            return todoItem != null ? ItemToDTO(todoItem) : null;        
        }

        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);

        public async Task<TodoItemDTO> InsertAsync(TodoItemDTO todo)
        {          
            var todoItem = new TodoItem
            {
                IsComplete = todo.IsComplete,
                Name = todo.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return (ItemToDTO(todoItem));
        }

        public async Task DeleteAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
            }
            await _context.SaveChangesAsync();

        }
    }
}
