using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface IDeleteRepository<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entity);
    }
}
