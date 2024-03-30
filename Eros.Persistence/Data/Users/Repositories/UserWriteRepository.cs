using Eros.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Identity;

namespace Eros.Persistence.Data.Users.Repositories;

public class UserWriteRepository(UserManager<User> userManager) : IUserWriteRepository
{
    public async Task<User?> AddAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        var identityResult = await userManager.CreateAsync(user, password);

        return !identityResult.Succeeded ? null : user;
    }
}
