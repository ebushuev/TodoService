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

        public async Task<T> AddAsync(T item)
        {
            Context.Set<T>().Add(item);
            await Context.SaveChangesAsync();
            return item;
        }

        public virtual async Task<int> UpdateAsync(T item)
        {
            Context.Set<T>().Attach(item);
            Context.Entry<T>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T item)
        {
            Context.Set<T>().Remove(item);
            return await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(long id)
        {
            return await Context.Set<T>().FindAsync(id);
        }
    }
}