using Eros.Domain;
using Eros.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Identity;

namespace Eros.Persistence.Data.Users.Repositories;

public class UserWriteRepository(UserManager<User> userManager, IUnitOfWork unitOfWork) : IUserWriteRepository
{
    public IUnitOfWork UnitOfWork => unitOfWork;

    public async Task<User?> AddAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        var identityResult = await userManager.CreateAsync(user, password);

        return !identityResult.Succeeded ? null : user;
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        await userManager.UpdateAsync(user);

        return user;
    }
}
