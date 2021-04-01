using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.BL
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repo;
        public TodoService(ITodoRepository repo)
        {
            _repo = repo;   
        }
        public Task<IEnumerable<TodoItemDTO>> ListAsync()
        {
            return _repo.ListAsync();

        }
        public Task<TodoItemDTO> GetAsync(long id)
        {
            return _repo.GetAsync(id);
        }

        public Task<TodoItemDTO> UpdateAsync(long id, TodoItemDTO todo)
        {
            
            return _repo.SaveAsync(id, todo);
        }

        public Task InsertAsync(TodoItemDTO todo)
        {
            return _repo.InsertAsync(todo);
        }
    }
}
