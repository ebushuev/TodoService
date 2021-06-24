using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface ICreateRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
    }
}
