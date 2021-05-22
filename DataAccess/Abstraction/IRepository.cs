using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApiDTO.DataAccess.Abstraction
{
    public interface IRepository<T> where T : class
    {
        void Update(T item);

        void Add(T item);

        void Delete(T item);

        IEnumerable<T> GetAll();

        T Get(int id);
    }
}
