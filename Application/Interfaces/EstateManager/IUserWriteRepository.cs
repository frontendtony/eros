using EstateManager.Commands;
using EstateManager.Entities;
using EstateManager.Models;

namespace EstateManager.Interfaces;

public interface IUserWriteRepository
{
    Task<User?> CreateUserAsync(SignupCommand command);
}
