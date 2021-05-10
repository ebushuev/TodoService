using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public TodoItemRepository(TodoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TodoItem> GetAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new TodoItemNotFoundException();
            }

            return todoItem;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> SaveAsync(TodoItem item)
        {
            var todoItem = await _context.TodoItems.FindAsync(item.Id);
            if (todoItem == null)
            {
                todoItem = new TodoItem
                {
                    IsComplete = item.IsComplete,
                    Name = item.Name
                };

                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                todoItem.Name = item.Name;
                todoItem.IsComplete = item.IsComplete;
                await _context.SaveChangesAsync();
            }

            return todoItem;
        }

        public async Task<long> DeleteAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new TodoItemNotFoundException();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return todoItem.Id;
        }
    }
}