namespace Eros.Domain.Aggregates.Users;

public interface IUserReadRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> CheckPassword(User user, string password);
}
