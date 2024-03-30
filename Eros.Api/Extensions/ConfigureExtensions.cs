using Eros.Api.Middlewares;

namespace Eros.Api.Extensions;

public static class ConfigureExtensions
{
    public static void ConfigureExceptionHandlers(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}