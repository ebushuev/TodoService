using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Infrastructure.Interfaces
{
    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetAsync(long id);
        Task<bool> UpdateAsync(TodoItemDTO todoItemDTO);
        Task CreateAsync(TodoItem todoItem);
        Task<bool> DeleteAsync(long id);
    }
}
