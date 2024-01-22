using Eros.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Identity;

namespace Eros.Persistence.Data.Users.Repositories;

public class UserReadRepository : IUserReadRepository
{
    private readonly UserManager<User> _userManager;
    public UserReadRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }
}