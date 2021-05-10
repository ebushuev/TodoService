using Application.Models;
using Domain.Entities;

namespace Application.Extensions
{
    public static class TodoItemExtensions
    {
        public static TodoItemDTO ToTodoItemDTO(this TodoItem todoItem) => new TodoItemDTO
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete
        };
    }
}