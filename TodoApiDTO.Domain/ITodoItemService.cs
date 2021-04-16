using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Domain.Models;

namespace TodoApiDTO.Domain
{
    public interface ITodoItemService
    {
        Task UpdateTodoItemAsync(TodoItem todoItem);

        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();

        Task<TodoItem> GetTodoItemAsync(long id);

        Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);

        Task DeleteTodoItemAsync(long id);
    }
}
