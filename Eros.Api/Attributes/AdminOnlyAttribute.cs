using Microsoft.AspNetCore.Mvc;

namespace Eros.Api.Attributes;

public class AdminOnlyAttribute : TypeFilterAttribute
{
    public AdminOnlyAttribute() : base(typeof(AdminOnlyFilter))
    {
    }
}