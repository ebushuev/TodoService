using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface IGetAllRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAllAsync();
    }
}
