using System;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface IGetRepository<T, P> where T: class
    {
        Task<T> GetAsync(P id);
    }
}
