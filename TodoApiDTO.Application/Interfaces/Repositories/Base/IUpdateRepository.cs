using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface IUpdateRepository<T> where T: class
    {
        Task<T> UpdateAsync(T entity);
    }
}
