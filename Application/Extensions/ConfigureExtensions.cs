using Api.Middlewares;

namespace Application.Extensions;

public static class ConfigureExtensions
{
    public static void ConfigureExceptionHandlers(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}