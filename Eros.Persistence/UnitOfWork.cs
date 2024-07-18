using Eros.Domain;

namespace Eros.Persistence;

public class UnitOfWork(ErosDbContext dbContext) : IUnitOfWork
{
  public void Dispose()
  {
    dbContext.Dispose();
    GC.SuppressFinalize(this);
  }

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return await dbContext.SaveChangesAsync(cancellationToken);
  }
}
