namespace Eros.Domain.Aggregates.Estates.Repositories;

public interface IEstateWriteRepository
{
    Task<Estate> AddAsync(Estate estate, CancellationToken cancellationToken);
    Task<Estate> UpdateAsync(Estate estate, CancellationToken cancellationToken);
    Task DeleteAsync(Estate estate, CancellationToken cancellationToken);
}
