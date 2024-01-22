namespace Eros.Domain.Interfaces;

public interface IWriteRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    T Update(T entity);
    void Delete(T entity);
}