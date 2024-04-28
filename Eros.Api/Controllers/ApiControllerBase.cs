using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private IHttpContextAccessor? _httpContextAccessor;
    private ISender? _mediator;

    private IHttpContextAccessor? HttpContextAccessor =>
        _httpContextAccessor ??= HttpContext?.RequestServices.GetRequiredService<IHttpContextAccessor>();

    protected ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected Guid UserId => GetUserId();
    protected string UserEmail => GetUserEmail();
    protected bool IsAdmin => IsAdminUser();

    private Guid GetUserId()
    {
        if (HttpContextAccessor?.HttpContext?.User is null) throw new InvalidDataException("HttpContext is null.");

        var userId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? throw new InvalidDataException("Invalid user id.");

        return Guid.Parse(userId);
    }

    private string GetUserEmail()
    {
        if (HttpContextAccessor?.HttpContext?.User is null) throw new InvalidDataException("HttpContext is null.");

        return HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)
               ?? throw new InvalidDataException("Invalid user email.");
    }

    private bool IsAdminUser()
    {
        if (HttpContextAccessor?.HttpContext?.User is null) throw new InvalidDataException("HttpContext is null.");

        return HttpContextAccessor.HttpContext.User.FindFirstValue("IsAdmin") == "True";
    }
}
