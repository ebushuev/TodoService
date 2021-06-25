using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Services
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
