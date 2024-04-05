using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Attributes;

public class ForbidAdminAttribute : TypeFilterAttribute
{
    public ForbidAdminAttribute() : base(typeof(ForbidAdminFilter))
    {
    }
}