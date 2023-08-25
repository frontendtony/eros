using EstateManager.Commands;
using EstateManager.Entities;
using EstateManager.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EstateManager.Repositories;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly UserManager<User> _userManager;

    public UserWriteRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> CreateUserAsync(SignupCommand command)
    {
        var newUser = new User(command);

        var identityResult = await _userManager.CreateAsync(newUser, command.Password);

        if (!identityResult.Succeeded)
        {
            return null;
        }

        return newUser;
    }
}
