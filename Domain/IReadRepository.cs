namespace Eros.Domain.Interfaces;

public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(string id);
}