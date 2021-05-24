
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DataAccess.Abstraction;
using System.Linq;

namespace TodoApiDTO.DataAccess.Implementation
{
    public class TodoItemRepository : RepositoryBase<TodoItem>, ITodoItemRepository
    {
        private readonly TodoContext _context;
        public TodoItemRepository(TodoContext context) : base(context)
        {
            _context = context;
        }

        private bool TodoItemExists(long id) =>
            _context.TodoItems.Any(e => e.Id == id);        

        public async override Task<int> UpdateAsync(TodoItem item)
        {
            try
            {
                return await base.UpdateAsync(item);
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(item.Id))
            {
                // need to log DbUpdateConcurrencyException or to insert it to new exception below to avoid dissappearing it's info
                throw new Exception("Not Found");
            }       
        }
    }
}