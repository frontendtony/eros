namespace Eros.Domain.Aggregates.Estates;

public interface IEstateUserWriteRepository
{
    Task AddAsync(EstateUser estateUser, CancellationToken cancellationToken = default);
}