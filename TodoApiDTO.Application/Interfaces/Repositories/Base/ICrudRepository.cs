namespace TodoApiDTO.Application.Interfaces.Repositories.Base
{
    public interface ICrudRepository<T, P> : ICreateRepository<T>, IUpdateRepository<T>,
                        IDeleteRepository<T>, IGetAllRepository<T>, IGetRepository<T, P> where T:class
    {
    }
}
