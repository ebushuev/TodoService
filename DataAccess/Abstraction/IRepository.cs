using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApiDTO.DataAccess.Abstraction
{
    public interface IRepository<T> where T : class
    {
        Task<int> UpdateAsync(T item);

        Task<T> AddAsync(T item);

        Task<int> DeleteAsync(T item);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(long id);
    }
}
