using Eros.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Identity;

namespace Eros.Persistence.Data.Users.Repositories;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly UserManager<User> _userManager;

    public UserWriteRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> AddAsync(User user, string password)
    {
        var identityResult = await _userManager.CreateAsync(user, password);

        if (!identityResult.Succeeded)
        {
            return null;
        }

        return user;
    }
}
