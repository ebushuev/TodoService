using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface IGetAllRepository<T> where T: class
    {
        Task<IList<T>> GetAllAsync();
    }
}
