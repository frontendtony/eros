using Eros.Domain.Interfaces;

namespace Eros.Domain.Aggregates.Users;

public interface IUserWriteRepository
{
    Task<User?> AddAsync(User user, string password);
}
