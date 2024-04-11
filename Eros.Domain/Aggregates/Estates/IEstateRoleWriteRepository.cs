namespace Eros.Domain.Aggregates.Estates;

public interface IEstateRoleWriteRepository
{
    Task AddAsync(EstateRole estateRole, CancellationToken cancellationToken = default);
}