using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Domain.Models;

namespace TodoApiDTO.Domain
{
    ///<inheritdoc/>
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));
        }

        ///<inheritdoc/>
        public async Task DeleteTodoItemAsync(long id)
        {
            await _todoItemRepository.DeleteTodoItemAsync(id);
        }

        ///<inheritdoc/>
        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await _todoItemRepository.GetTodoItemAsync(id);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _todoItemRepository.GetTodoItemsAsync();
        }

        ///<inheritdoc/>
        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            await _todoItemRepository.UpdateTodoItemAsync(todoItem);
        }

        ///<inheritdoc/>
        public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
        {
            return await _todoItemRepository.CreateTodoItemAsync(todoItem);
        }
    }
}
