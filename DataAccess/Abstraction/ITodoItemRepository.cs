using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.DataAccess.Abstraction
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
    }
}
