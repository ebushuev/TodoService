using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Application.Interfaces.Services.Models;

namespace TodoApiDTO.Application.Interfaces.Services
{
    public interface ITodoItemService
    {
        Task<TodoItemEntity> CreateAsync(TodoItemEntity entity);
        Task<TodoItemEntity> UpdateAsync(TodoItemEntity entity);
        Task DeleteAsync(long id);
        Task<TodoItemEntity> GetAsync(long id);
        Task<IList<TodoItemEntity>> GetAllAsync();
    }
}
