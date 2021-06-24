using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface IGetRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> GetAsync(TKey id);
    }
}
