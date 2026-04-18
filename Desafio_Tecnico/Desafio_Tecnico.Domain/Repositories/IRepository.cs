namespace Desafio_Tecnico.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
    }
}
