using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.DataAccess
{
    public class TodoContext : DbContext, IDbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}