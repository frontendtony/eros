using System.Security.Claims;
using EstateManager.Constants;
using Microsoft.AspNetCore.Mvc;

namespace EstateManager.Handlers;

public class EstateManagerBaseHandler : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EstateManagerBaseHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected Guid GetUserId()
    {
        var id = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out var userId))
        {
            throw new UnauthorizedException("Unauthorized");
        }

        return userId;
    }

    protected bool IsSelf(Guid userId)
    {
        return userId == GetUserId();
    }

    protected bool IsAdmin()
    {
        var role = HttpContext.User.FindFirst(CustomClaimTypes.IsAdmin)?.Value;

        if (string.IsNullOrWhiteSpace(role) || !bool.TryParse(role, out var result))
        {
            throw new UnauthorizedException("Unauthorized");
        }

        return result;
    }

    protected bool IsAdminOrSelf(Guid userId)
    {
        return IsAdmin() || IsSelf(userId);
    }
}
