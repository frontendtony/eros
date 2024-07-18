namespace Eros.Domain;

public interface IUnitOfWork : IDisposable
{
  public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
