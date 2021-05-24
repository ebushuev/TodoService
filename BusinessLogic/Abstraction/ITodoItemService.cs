using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.DataAccess.Abstraction
{
    public interface ITodoItemService
    {
        Task<int> UpdateAsync(long id, TodoItemDTO todoItemDTO);

        Task<TodoItemDTO> AddAsync(TodoItemDTO todoItemDTO);

        Task<int> DeleteAsync(long id);

        Task<IEnumerable<TodoItemDTO>> GetAllAsync();

        Task<TodoItemDTO> GetAsync(long id);
    }
}
