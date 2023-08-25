using EstateManager.Entities;

namespace EstateManager.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
