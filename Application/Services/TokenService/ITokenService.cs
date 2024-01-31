
using Eros.Domain.Aggregates.Users;

namespace Eros.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
