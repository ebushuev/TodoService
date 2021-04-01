using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.BL
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItemDTO>> ListAsync();
        Task<TodoItemDTO> GetAsync(long id);
        Task<TodoItemDTO> SaveAsync(long id, TodoItemDTO todo);
        Task<TodoItemDTO> InsertAsync(TodoItemDTO todo);
        Task DeleteAsync(long id);
    }
}
