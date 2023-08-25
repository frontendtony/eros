namespace Application.Interfaces;

public interface IWriteRepository<T> where T : class
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    T Create(T entity);
    T Update(T entity);
    void Delete(T entity);
}