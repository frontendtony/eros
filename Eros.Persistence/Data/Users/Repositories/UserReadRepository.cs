using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Users.Repositories;

public class UserReadRepository(
    UserManager<User> userManager,
    ErosDbContext dbContext
) : IUserReadRepository
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<List<Estate>> GetEstatesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var estates = await dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Estates)
            .ToListAsync(cancellationToken);

        return estates;
    }
}
