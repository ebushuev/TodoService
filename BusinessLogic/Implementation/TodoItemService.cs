using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DataAccess.Implementation;
using TodoApiDTO.Models;

namespace TodoApiDTO.DataAccess.Abstraction
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async  Task<int> UpdateAsync(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _todoItemRepository.GetAsync(id);
            if (todoItem == null)
            {
                throw new Exception("Not Found");
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;
                       
            return await _todoItemRepository.UpdateAsync(todoItem);           
        }

        public async Task<TodoItemDTO> AddAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            return MapHelper.ItemToDTO(await _todoItemRepository.AddAsync(todoItem));      
        }

        public async Task<int> DeleteAsync(long id)
        {
            var todoItem = await _todoItemRepository.GetAsync(id);
            if (todoItem == null)
            {
                throw new Exception("Not Found");
            }

            return await _todoItemRepository.DeleteAsync(todoItem);
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            var todoItems = await _todoItemRepository.GetAllAsync();
            return todoItems.Select(x => MapHelper.ItemToDTO(x));
        }

        public async Task<TodoItemDTO> GetAsync(long id)
        {
            var todoItem = await _todoItemRepository.GetAsync(id);
            return MapHelper.ItemToDTO(todoItem);
        }
    }
}
