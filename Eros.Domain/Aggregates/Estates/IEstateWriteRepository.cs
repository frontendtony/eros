namespace Eros.Domain.Aggregates.Estates;

public interface IEstateWriteRepository
{
    Task<Estate> AddAsync(Estate estate, CancellationToken cancellationToken);
    Estate UpdateAsync(Estate estate, CancellationToken cancellationToken);
    Task DeleteAsync(Estate estate, CancellationToken cancellationToken);
}
