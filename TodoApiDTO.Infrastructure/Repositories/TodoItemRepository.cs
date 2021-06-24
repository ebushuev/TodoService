using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Application.Interfaces.Repositories;
using TodoApiDTO.Infrastructure.DBContexts;
using TodoAPIDTO.Domain.Models;

namespace TodoApiDTO.Infrastructure.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _todoContext;
        public TodoItemRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task<IList<TodoItem>> GetAllAsync()
        {
            return await _todoContext.TodoItems.ToListAsync();
        }

        public Task<TodoItem> GetAsync(long id)
        {
            return _todoContext.TodoItems.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _todoContext.Update(entity);
            await _todoContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TodoItem> CreateAsync(TodoItem entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _todoContext.TodoItems.AddAsync(entity);
            await _todoContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TodoItem entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
                
            _todoContext.TodoItems.Remove(entity);
            await _todoContext.SaveChangesAsync();
        }
    }
}
