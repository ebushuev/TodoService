
using TodoApi.Models;
using TodoApiDTO.DataAccess.Abstraction;

namespace TodoApiDTO.DataAccess.Implementation
{
    public class TodoItemRepository<T> : RepositoryBase<T>, IRepository<T> where T : TodoItem
    {
        public TodoItemRepository(TodoContext context) : base(context)
        {
        }
    }
}
