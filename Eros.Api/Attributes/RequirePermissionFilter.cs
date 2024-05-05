using System.Security.Claims;
using Eros.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Eros.Api.Attributes;

public class RequirePermissionAttribute : TypeFilterAttribute
{
    public RequirePermissionAttribute(string permission) : base(typeof(RequirePermissionFilter))
    {
        Arguments = new object[] { permission };
    }
}

internal class RequirePermissionFilter(string permission) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var estateId = context.HttpContext.Request.Headers["EstateId"].ToString();

        if (string.IsNullOrWhiteSpace(estateId))
        {
            throw new BadRequestException("EstateId is required.");
        }

        var permissions = GetEstatePermissions(context.HttpContext.User, estateId);

        if (permissions.Contains(permission))
        {
            return;
        }

        throw new ForbiddenException("Unauthorized. You don't have the required permission.");
    }

    private static List<string> GetEstatePermissions(ClaimsPrincipal user, string estateId)
    {
        // Check if the "EstateRoles" claim exists
        var estateRolesClaim = user.FindFirst("EstateRoles");
        if (estateRolesClaim == null)
        {
            return []; // No permissions found
        }

        // Parse the JSON string within the claim
        var estateRoles = JsonConvert.DeserializeObject<Dictionary<string, EstateRole>>(estateRolesClaim.Value);

        if (estateRoles == null)
        {
            return []; // No permissions found
        }

        // Check if the specified estate id exists in the dictionary
        return !estateRoles.TryGetValue(estateId, out var value) ? []
            : value.Permissions;
    }
}

internal sealed record EstateRole
{
    public List<string> Permissions { get; } = [];
}
