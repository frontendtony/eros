using Eros.Domain.Aggregates.Roles;

namespace Eros.Domain.Aggregates.Estates;

public interface IEstateReadRepository
{
    Task<Estate?> GetByIdAsync(Guid id);
    Task<IEnumerable<Estate>> GetAllAsync();
}
