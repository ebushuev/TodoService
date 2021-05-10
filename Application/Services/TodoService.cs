using Application.Extensions;
using Application.Models;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));
        }

        public async Task<TodoItemDTO> GetAsync(long id)
        {
            return (await _todoItemRepository.GetAsync(id))
                .ToTodoItemDTO();
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            return (await _todoItemRepository.GetAllAsync())
                .Select(item => item.ToTodoItemDTO());
        }

        public async Task<TodoItemDTO> SaveAsync(TodoItemDTO dto)
        {
            return (await _todoItemRepository.SaveAsync(new TodoItem()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    IsComplete = dto.IsComplete
                }))
                .ToTodoItemDTO();
        }

        public async Task<long> DeleteAsync(long id)
        {
            return await _todoItemRepository.DeleteAsync(id);
        }
    }
}