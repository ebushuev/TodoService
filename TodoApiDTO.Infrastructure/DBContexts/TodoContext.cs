using System;
using Microsoft.EntityFrameworkCore;
using TodoAPIDTO.Domain.Models;

namespace TodoApiDTO.Infrastructure.DBContexts
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<TodoItem>()
                .Property(b => b.Secret)
                .HasMaxLength(255);
        }
    }
}
