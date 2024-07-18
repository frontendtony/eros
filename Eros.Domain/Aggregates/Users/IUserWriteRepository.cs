namespace Eros.Domain.Aggregates.Users;

public interface IUserWriteRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<User?> AddAsync(User user, string password, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);
}
