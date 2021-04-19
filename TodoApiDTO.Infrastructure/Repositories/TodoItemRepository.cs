using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.Domain;
using TodoApiDTO.Domain.Models;
using TodoApiDTO.Infrastructure.Models;

namespace TodoApiDTO.Infrastructure.Repositories
{
    ///<inheritdoc/>
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _todoContext;

        public TodoItemRepository(TodoContext todoContext)
        {
            _todoContext = todoContext ?? throw new ArgumentNullException(nameof(todoContext));
        }

        ///<inheritdoc/>
        public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
        {
            _todoContext.TodoItems.Add(todoItem);
            await _todoContext.SaveChangesAsync();
            return todoItem;
        }

        ///<inheritdoc/>
        public async Task DeleteTodoItemAsync(long id)
        {
            var todoItem = await _todoContext.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new NotFoundException($"Не найден объект с ID={id} для удаления");
            }

            _todoContext.TodoItems.Remove(todoItem);
            await _todoContext.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await _todoContext.TodoItems.FindAsync(id);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _todoContext.TodoItems.ToListAsync();
        }

        ///<inheritdoc/>
        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            var todoItemOld = await _todoContext.TodoItems.FindAsync(todoItem.Id);

            if (todoItem == null)
            {
                throw new NotFoundException($"Не найден объект с ID={todoItem.Id} для обновления");
            }

            todoItemOld.Name = todoItem.Name;
            todoItemOld.IsComplete = todoItem.IsComplete;

            try
            {
                await _todoContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) when (!TodoItemExists(todoItem.Id))
            {
                throw new NotFoundException($"Не удалось обновить объект с ID={todoItem.Id}", e);
            }
        }

        private bool TodoItemExists(long id) =>
             _todoContext.TodoItems.Any(e => e.Id == id);
    }
}
