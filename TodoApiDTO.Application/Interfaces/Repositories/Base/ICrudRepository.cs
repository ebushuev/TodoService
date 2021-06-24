namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface ICrudRepository<TEntity, TKey> : ICreateRepository<TEntity>, IUpdateRepository<TEntity>,
                        IDeleteRepository<TEntity>, IGetAllRepository<TEntity>, IGetRepository<TEntity, TKey> where TEntity : class
    {
    }
}
