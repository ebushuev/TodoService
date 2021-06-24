using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface IUpdateRepository<TEntity> where TEntity : class
    {
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
