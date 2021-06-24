using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface ICreateRepository<T> where T: class
    {
        Task<T> CreateAsync(T entity);
    }
}
