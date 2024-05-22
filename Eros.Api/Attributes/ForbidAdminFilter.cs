using System.Security.Claims;
using Eros.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eros.Api.Attributes;

public class ForbidAdminFilter(IHttpContextAccessor httpContextAccessor) : IAuthorizationFilter
{
  public void OnAuthorization(AuthorizationFilterContext context)
  {
    if (httpContextAccessor?.HttpContext?.User == null) return;
    var isAdminClaim = httpContextAccessor.HttpContext.User.FindFirstValue("IsAdmin");

    if (isAdminClaim is null or "False") return;

    throw new UnauthorizedException("An admin is not allowed to perform this action.");
  }
}
