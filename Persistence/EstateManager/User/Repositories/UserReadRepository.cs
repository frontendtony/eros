using EstateManager.Entities;
using EstateManager.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EstateManager.Repositories;

public class UserReadRepository : IUserReadRepository
{
    private readonly UserManager<User> _userManager;

    public UserReadRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }
}
