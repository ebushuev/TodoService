using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Application.Interfaces.Repositories.Base;
using TodoAPIDTO.Domain.Models;

namespace TodoApiDTO.Application.Interfaces.Repositories
{
    public interface ITodoItemRepository : ICrudRepository<TodoItem, long>
    {
    }
}
