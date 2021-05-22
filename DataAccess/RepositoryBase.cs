using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.DataAccess.Abstraction;

namespace TodoApiDTO.DataAccess
{

    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public RepositoryBase(DbContext context)
        {
            Context = context;
        }

        public void Add(T item)
        {
            Context.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            Context.Set<T>().Attach(item);
            Context.Entry<T>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(T item)
        {
            Context.Set<T>().Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }
    }
}