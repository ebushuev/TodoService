using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Domain.Models;

namespace TodoApiDTO.Domain
{
    /// <summary>
    /// Сервис для работы с задачами
    /// </summary>
    public interface ITodoItemService
    {
        /// <summary>
        /// Обновляет задачу
        /// </summary>
        /// <param name="todoItem">новое значение</param>
        /// <returns></returns>
        Task UpdateTodoItemAsync(TodoItem todoItem);

        /// <summary>
        /// Возвращает весь список задач
        /// </summary>
        /// <returns>Список задач</returns>
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();

        /// <summary>
        /// Возвращает задачу по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Найденная задача</returns>
        Task<TodoItem> GetTodoItemAsync(long id);

        /// <summary>
        /// Создает новую звдвчу
        /// </summary>
        /// <param name="todoItem"></param>
        /// <returns>Созданная задача</returns>
        Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);

        /// <summary>
        /// Удаляет задачу по ID
        /// </summary>
        /// <param name="id">ID задачи</param>
        /// <returns></returns>
        Task DeleteTodoItemAsync(long id);
    }
}
