using System;
using Microsoft.EntityFrameworkCore;
using TodoAPIDTO.Domain.Models;

namespace TodoApiDTO.Infrastructure.DBContexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
