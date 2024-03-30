namespace Eros.Domain.Aggregates.Estates;

public interface IEstateReadRepository
{
    Task<Estate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Estate?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Estate>> GetAllAsync(CancellationToken cancellationToken = default);
}
