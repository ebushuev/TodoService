using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Domain.Models;

namespace TodoApiDTO.Domain
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));
        }

        public async Task DeleteTodoItemAsync(long id)
        {
            await _todoItemRepository.DeleteTodoItemAsync(id);
        }

        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await _todoItemRepository.GetTodoItemAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _todoItemRepository.GetTodoItemsAsync();
        }

        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            await _todoItemRepository.UpdateTodoItemAsync(todoItem);
        }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
        {
            return await _todoItemRepository.CreateTodoItemAsync(todoItem);
        }
    }
}
