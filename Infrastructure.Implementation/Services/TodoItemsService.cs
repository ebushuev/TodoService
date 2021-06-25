using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Infrastructure.Interfaces.Services;
using Infrastructure.Interfaces.DataAccess;

namespace Infrastructure.Implementation.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly IDbContext _context;

        public TodoItemsService(IDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var item = await GetAsync(id);
            if (item != null)
            {
                _context.TodoItems
                    .Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync() =>
            await _context.TodoItems
                .AsNoTracking()
                .ToListAsync();

        public async Task<TodoItem> GetAsync(long id) =>
            await _context.TodoItems
                .FindAsync(id);

        public async Task<bool> UpdateAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = await GetAsync(todoItemDTO.Id);
            if (todoItem == null)
            {
                return false;
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(todoItemDTO.Id))
            {
                return false;
            }
            return true;
        }
        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);
    }
}
