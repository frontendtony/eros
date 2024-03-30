using Eros.Domain.Interfaces;

namespace Eros.Domain.Aggregates.Users;

public interface IUserReadRepository : IReadRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> CheckPassword(User user, string password);
}
