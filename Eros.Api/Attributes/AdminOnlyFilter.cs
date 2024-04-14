using System.Security.Claims;
using Eros.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eros.Api.Attributes;

public class AdminOnlyFilter(IHttpContextAccessor httpContextAccessor) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (httpContextAccessor?.HttpContext?.User is null)
        {
            throw new InvalidDataException("HttpContext is null.");
        }

        if (httpContextAccessor.HttpContext.User.FindFirstValue("IsAdmin") == "True") return;

        throw new ForbiddenException("Unauthorized.");
    }
}