using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface IDeleteRepository<T> where T: class
    {
        Task DeleteAsync(T entity);
    }
}
