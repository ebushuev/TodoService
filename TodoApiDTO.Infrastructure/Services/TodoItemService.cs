using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.Application.Interfaces.Repositories;
using TodoApiDTO.Application.Interfaces.Services;
using TodoApiDTO.Application.Interfaces.Services.Models;
using TodoAPIDTO.Domain.Exceptions;
using TodoAPIDTO.Domain.Models;

namespace TodoApiDTO.Infrastructure.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<TodoItemEntity> CreateAsync(TodoItemEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id != default)
                throw new ArgumentException($"{nameof(entity.Id)} property should not be non-zero integer", nameof(entity));

            ValidateTodoItem(entity);
            var dbEntity = new TodoItem
            {
                Id = default,
                IsComplete = entity.IsComplete,
                Name = entity.Name
            };

            await _todoItemRepository.CreateAsync(dbEntity);
            entity.Id = dbEntity.Id;
            return entity;
        }

        public async Task<TodoItemEntity> UpdateAsync(TodoItemEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id <= 0)
                throw new ArgumentException($"{nameof(entity.Id)} property should be positive integer", nameof(entity));

            ValidateTodoItem(entity);
            TodoItem todoItemFromDb = await GetAsyncWithRawModel(entity.Id);
            todoItemFromDb.Name = entity.Name;
            todoItemFromDb.IsComplete = entity.IsComplete;
            await _todoItemRepository.UpdateAsync(todoItemFromDb);
            return entity;

        }

        public async Task DeleteAsync(long id)
        {
            TodoItem todoItemFromDb = await GetAsyncWithRawModel(id);
            await _todoItemRepository.DeleteAsync(todoItemFromDb);
        }

        public async Task<TodoItemEntity> GetAsync(long id)
        {
            TodoItem todoItemFromDb = await GetAsyncWithRawModel(id);
            return ConvertToDTO(todoItemFromDb);
        }

        public async Task<IList<TodoItemEntity>> GetAllAsync()
        {
            IList<TodoItem> todoItems = await _todoItemRepository.GetAllAsync();
            return todoItems.Select(i => ConvertToDTO(i)).ToList();
        }

        private async Task<TodoItem> GetAsyncWithRawModel(long id)
        {
            TodoItem todoItemFromDb = await _todoItemRepository.GetAsync(id);
            string className = typeof(TodoItem).Name;
            if (todoItemFromDb == null)
                throw new EntityNotFoundException(className, id);

            return todoItemFromDb;
        }

        private void ValidateTodoItem(TodoItemEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var validationErrors = new List<string>();
            if (entity.Name == null)
                validationErrors.Add($"{nameof(entity.Name)} property should not be null");

            // можно было использовать string.IsNullOrEmpty или string.IsNUllOrWhitespace
            // но я решил обработать случаи c null и "пустотой" отдельно
            if (entity.Name.Trim() == string.Empty)
                validationErrors.Add($"{nameof(entity.Name)} property should not be emtpy or whitespace");

            if (validationErrors.Count > 0)
                throw new EntityNotValidException(validationErrors);
        }

        private TodoItemEntity ConvertToDTO(TodoItem todoItem)
        {
            if (todoItem == null)
                throw new ArgumentNullException(nameof(todoItem));

            return new TodoItemEntity
            {
                Id = todoItem.Id,
                IsComplete = todoItem.IsComplete,
                Name = todoItem.Name
            };
        }
    }
}
