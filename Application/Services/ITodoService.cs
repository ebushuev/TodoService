using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ITodoService
    {
        public Task<TodoItemDTO> GetAsync(long id);

        public Task<IEnumerable<TodoItemDTO>> GetAllAsync();

        public Task<TodoItemDTO> SaveAsync(TodoItemDTO dto);

        public Task<long> DeleteAsync(long id);
    }
}