using EstateManager.Entities;

namespace EstateManager.Interfaces;

public interface IUserReadRepository
{
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> CheckPassword(User user, string password);
}
