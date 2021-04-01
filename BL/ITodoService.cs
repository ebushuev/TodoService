using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.BL
{
    public interface ITodoService
    {
        public Task<IEnumerable<TodoItemDTO>> ListAsync();
        public Task<TodoItemDTO> GetAsync(long id);
        public Task<TodoItemDTO> UpdateAsync(long id, TodoItemDTO todo);
        public Task InsertAsync(TodoItemDTO todo);


    }
}
