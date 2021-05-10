using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITodoItemRepository
    {
        public Task<TodoItem> GetAsync(long id);

        public Task<IEnumerable<TodoItem>> GetAllAsync();

        public Task<TodoItem> SaveAsync(TodoItem item);

        public Task<long> DeleteAsync(long id);
    }
}