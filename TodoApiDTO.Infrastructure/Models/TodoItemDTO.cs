using System.Text.Json.Serialization;
using TodoApiDTO.Domain.Models;

namespace TodoApiDTO.Infrastructure.Models
{
    #region snippet
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        [JsonIgnore]
        public TodoItem GetTodoItem =>
            new TodoItem
            {
                Id = Id,
                IsComplete = IsComplete,
                Name = Name
            };
    }
    #endregion
}
